using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using Windows.UI.Core;
using Windows.UI.Xaml.Shapes;

using Windows.Media.Capture;            //For MediaCapture
using Windows.Media.MediaProperties;    //For Encoding Image in JPEG format
using Windows.Storage;                  //For storing Capture Image in App storage or in Picture Library
using Windows.UI.Xaml.Media.Imaging;
using Windows.Devices.Sensors;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;    //For BitmapImage. for showing image on screen we need BitmapImage format.
 
// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace StarCapturePlan
{

    public sealed partial class MainPage : Page
    {
        Gyrometer gyrometer;
        GyrometerReading gyrometerReading;

        Geolocator geolocator;//location

        public static ObservableCollection<int> numberCollection = new ObservableCollection<int>();
        //取当前年月日时分秒 
        DateTime currentTime;

        //int number1 = 25;
        Image[] image = new Image[25];
        Grid[] grids = new Grid[25];

        Windows.Storage.ApplicationDataContainer _localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        double weidu;
        double jingdu;

        int number=0, number1 = 0;

        String currentX, currentY, currentZ;
        private DispatcherTimer timer;


        static bool Animotion = false, Message = false, shining = false;
        //Declare MediaCapture object globally
        //Windows.Media.Capture.MediaCapture captureManager;
        //mysqlite MySqlite;

        StarClient starclient;

        public MainPage()
        {
            this.InitializeComponent();
            
            this.NavigationCacheMode = NavigationCacheMode.Required;

            //InitilizeImage();

            CheckSetting();
         
            initialize();

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler<Object>(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(5000);
            //timer.Interval = new TimeSpan(0, 0, 0, 1); 
            
            timer.Start();
            
            camera();


            if ((bool)_localSettings.Values["isMessage"])
                toastText02_Click();
        }

        async void initialize()
        {
             //-----------------------------------------------------------------------------陀螺仪部分
            string errMessage = "";
            try
            {
                //获取默认的陀螺仪对象
                gyrometer = Gyrometer.GetDefault();
                if (gyrometer == null)
                {
                    await new MessageDialog("不支持陀螺仪").ShowAsync();
                    return;
                }
                //设置读取数据的时间间隔
                gyrometer.ReportInterval = 1000;
                gyrometer.ReadingChanged += gyrometer_ReadingChanged;
                gyrometerReading = gyrometer.GetCurrentReading();
                
            }
            
            catch (Exception err)
            {
                errMessage = err.Message;
            }
            
            if (errMessage != "")
                await new MessageDialog(errMessage).ShowAsync();

            //-------------------------------------------------------------
            GetLocation();
            ShowData();
            starclient = new StarClient(GetTime()[0], GetTime()[1], GetTime()[2], GetTime()[3], weidu, jingdu,
                Double.Parse(currentX), Double.Parse(currentY), Double.Parse(currentZ));
            number = starclient.planetnumber();

            //MySqlite = new mysqlite();
            //MySqlite.creatTable();

            
            
        }

        public void timer_Tick(object sender, object e)
        {
                    
            //(sender as DispatcherTimer).Tick -= timer_Tick;

            //(sender as DispatcherTimer).Stop();

            drawGrid.Children.Clear();
            ShowData();
            starclient = new StarClient(GetTime()[0], GetTime()[1], GetTime()[2], GetTime()[3], weidu, jingdu,
                Double.Parse(currentX), Double.Parse(currentY), Double.Parse(currentZ));
            number = starclient.planetnumber();
             textblock.Text = "";
            if (number1 != number)
            {
               
                textblock1.Text = "";
                Draw(number);
            }
            number1 = number;
 
        }


        private void toastText02_Click()
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            XmlNodeList elements = toastXml.GetElementsByTagName("text");
            elements[0].AppendChild(toastXml.CreateTextNode("SCP新消息"));
            elements[1].AppendChild(toastXml.CreateTextNode("【木星】由巨蟹座运行至狮子座，前半夜可见"));
            ToastNotification toast = new ToastNotification(toastXml);
            toast.Activated += toast_Activated;
            toast.Dismissed += toast_Dismissed;
            toast.Failed += toast_Failed;
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        //Toast 通知弹出失败的事件
        async void toast_Failed(ToastNotification sender, ToastFailedEventArgs args)
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                //info.Text = "Toast通知消失：" + args.ErrorCode.ToString();
            });
        }

        async void toast_Dismissed(ToastNotification sender, ToastDismissedEventArgs args)
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
               // info.Text = "Toast通知消失：" + args.Reason.ToString();
            });
        }

        //Toast通知激活的事件，当通知弹出时，点击通知会触发该事件
        async void toast_Activated(ToastNotification sender, object args)
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                //info.Text = "Toast通知激活";
            });
        }



        async void camera()
        {
            
            Windows.Media.Capture.MediaCapture captureManager;

            captureManager = new MediaCapture();        //Define MediaCapture object

            await captureManager.InitializeAsync();     //Initialize MediaCapture and 
            captureManager.SetPreviewRotation(VideoRotation.Clockwise90Degrees);
            capturePreview.Source = captureManager;     //Start preiving on CaptureElement
            await captureManager.StartPreviewAsync();   //Start camera capturing 
            SimpleOrientationSensor sensor = SimpleOrientationSensor.GetDefault();
            sensor.OrientationChanged += (s, arg) =>
            {
                switch (arg.Orientation)
                {
                    case SimpleOrientation.Rotated90DegreesCounterclockwise:
                        captureManager.SetPreviewRotation(VideoRotation.None);
                        break;
                    case SimpleOrientation.Rotated180DegreesCounterclockwise:
                    case SimpleOrientation.Rotated270DegreesCounterclockwise:
                        captureManager.SetPreviewRotation(VideoRotation.Clockwise180Degrees);
                        break;
                    default:
                        captureManager.SetPreviewRotation(VideoRotation.Clockwise90Degrees);
                        break;
                }
            };


            captureManager = null;
        }

        private void CheckSetting()
        {
            if (!_localSettings.Values.ContainsKey("isNetwork"))
                _localSettings.Values["isNetwork"] = false;
            if (!_localSettings.Values.ContainsKey("isWrite"))
                _localSettings.Values["isWrite"] = true;
            if (!_localSettings.Values.ContainsKey("isAnimotion"))
                _localSettings.Values["isAnimotion"] = true;
            if (!_localSettings.Values.ContainsKey("isMessage"))
                _localSettings.Values["isMessage"] = true;
        }

        //private void InitilizeImage()
        //{
        //    for (int i = 1; i <= 25; i++)
        //    {
        //        ImageBrush ib = new ImageBrush();
        //        ib.ImageSource = new BitmapImage(new Uri("ms-appx:/Assets/" + i + ".png", UriKind.RelativeOrAbsolute));
        //        Grid _grid = new Grid();
        //        _grid.Background = ib;
        //        _grid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        //        BackGroundGrid.Children.Add(_grid);
        //        grids[i - 1] = _grid;
        //    }
        //    grids[0].Visibility = Windows.UI.Xaml.Visibility.Visible;
        //}

        //private void slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        //{
        //    grids[now].Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        //    now = (int)slider.Value / 4;
        //    grids[now].Visibility = Visibility.Visible;
        //}

        private void SettingsAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingPage));
        }

        //private void Clear_Click(object sender, RoutedEventArgs e)
        //{
        //    drawGrid.Children.Clear();
        //    textblock.Text = "";
        //    textblock1.Text = "";
        //}

        private void SetStar(double a, double b,double c,double d)
        {
            var image = new Image();
            image.Source = new BitmapImage(new Uri("ms-appx:/Assets/star.png"));
            image.Margin = new Thickness(a, b, c, d);
            //image.SetValue(Image.MarginProperty, new Thickness(a,b,c,d));
            drawGrid.Children.Add(image);
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    camera();
        //}
        //private void SetLine(double a, double b, double c, double d)
        //{
        //    Line line = new Line() { X1=a,Y1=b,X2=c,Y2=d};
        //    Canvas.Children.Add(line);
        //}

        //private void knock_Click(object sender, RoutedEventArgs e)
        //{

        //    var starclient = new StarClient(GetTime()[0], GetTime()[1], GetTime()[2], GetTime()[3], weidu, jingdu, ToDegrees((float)gyrometerReading.AngularVelocityX), ToDegrees((float)gyrometerReading.AngularVelocityY), ToDegrees((float)gyrometerReading.AngularVelocityZ));
        //    number = starclient.planetnumber();
        //    Draw(number);
        //}

        private float ToDegrees(float radians)
        {
            return (float)(radians * 180 / Math.PI);
        }

        async void gyrometer_ReadingChanged(Gyrometer sender, GyrometerReadingChangedEventArgs args)
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                gyrometerReading = args.Reading;
            });
        }

        public int[] GetTime()
        {
            int[] temp = new int[4];
            currentTime = DateTime.Now;
            int mm = currentTime.Month;
            int dd = currentTime.Day;
            int hh = currentTime.Hour;
            int min = currentTime.Minute;
            temp[0] = mm;
            temp[1] = dd;
            temp[2] = hh;
            temp[3] = min;
            return temp;
        }
        
        public async void GetLocation()
        {
            try
            {
                geolocator = new Geolocator();
                Geoposition pos = await geolocator.GetGeopositionAsync();
                this.weidu = pos.Coordinate.Point.Position.Latitude;
                this.jingdu = pos.Coordinate.Point.Position.Longitude;
                double accuracy = pos.Coordinate.Accuracy;
            }
            catch (System.UnauthorizedAccessException)
            {
            }
            catch (TaskCanceledException)
            {
            }
        }

        void ShowData()
        {
            currentX = gyrometerReading.AngularVelocityX.ToString("0.000");
            currentY = gyrometerReading.AngularVelocityY.ToString("0.000");
            currentZ = gyrometerReading.AngularVelocityZ.ToString("0.000");
        }


        private void HelpAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HelpPage));
            //timer.Stop();
        }

        private void CollectionPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CollectionPage), numberCollection);//)
            //timer.Stop();
        }

        private void Collect_Click(object sender, RoutedEventArgs e)
        {
           
            if(!numberCollection.Contains(number))
                numberCollection.Add(number);
            
            //MySqlite.insert(number);
        }


        private void drawGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Details), number);
            //timer.Stop();
        }
        
        async void Draw(int number)
        {
            //textblock.Text = null;
            /*
             if((bool)_localSettings.Values["isAnimotion"]) //有donghua    
             */
            //starImage = null;
            var starImage = new Image();
            starImage.HorizontalAlignment = HorizontalAlignment.Center;
            starImage.VerticalAlignment = VerticalAlignment.Center; ;
            starImage.Width = 300;
            starImage.Height = 300;


            switch (number)
            {
                case 0:
                    starImage.Source = new BitmapImage(new Uri("ms-appx:/Assets/maind0.png"));
                    drawGrid.Children.Add(starImage);
                    textblock.Text = "Mercury 水星";
                    break;
                case 1:
                    starImage.Source = new BitmapImage(new Uri("ms-appx:/Assets/maind1.png"));
                    drawGrid.Children.Add(starImage);
                    textblock.Text = "Venus 金星";
                    break;
                case 2:
                    starImage.Source = new BitmapImage(new Uri("ms-appx:/Assets/maind2.png"));
                    drawGrid.Children.Add(starImage);
                    textblock.Text = "Mars 火星";
                    break;
                case 3:
                    starImage.Source = new BitmapImage(new Uri("ms-appx:/Assets/maind3.png"));
                    drawGrid.Children.Add(starImage);
                    textblock.Text = "Jupiter 木星";
                    break;
                case 4:
                    starImage.Source = new BitmapImage(new Uri("ms-appx:/Assets/maind4.png"));
                    drawGrid.Children.Add(starImage);
                    textblock.Text = "Saturn 土星";
                    break;
                case 5:
                    starImage.Source = new BitmapImage(new Uri("ms-appx:/Assets/maind5.png"));
                    drawGrid.Children.Add(starImage);
                    textblock.Text = "Uranus 天王星";
                    break;
                case 6:
                    starImage.Source = new BitmapImage(new Uri("ms-appx:/Assets/maind6.png"));
                    drawGrid.Children.Add(starImage);
                    textblock.Text = "Neptune 海王星";
                    break;
                case 7:
                    SetStar(236, 10, 129, 364); SetStar(248, 80, 117, 294); SetStar(355, 50, 10, 324); SetStar(273, 238, 92, 136);
                    SetStar(236, 341, 129, 33); SetStar(295, 312, 70, 62); SetStar(330, 341, 35, 33); SetStar(365, 325, 0, 49);
                    SetStar(119, 23, 246, 351); SetStar(133, 66, 232, 308); SetStar(53, 80, 312, 294); SetStar(100, 205, 265, 169);
                    SetStar(133, 245, 232, 129); SetStar(40, 268, 325, 106); SetStar(79, 341, 286, 33); SetStar(133, 312, 232, 62);
                    textblock.Text = "Gemini 双子座";
                    break;
                case 8:
                    SetStar(272, 24, 93, 350); SetStar(82, 24, 283, 350); SetStar(96, 45, 269, 329); SetStar(286, 103, 79, 271);
                    SetStar(82, 103, 283, 271); SetStar(200, 143, 165, 231); SetStar(200, 210, 165, 164); SetStar(149, 264, 216, 110);
                    SetStar(128, 325, 237, 49); SetStar(240, 245, 125, 129); SetStar(272, 309, 93, 65);
                    textblock.Text = "Taurus 金牛座";
                    break;
                case 9:
                    SetStar(327, 154, 38, 189); SetStar(313, 114, 52, 229); SetStar(200, 52, 165, 291); SetStar(25, 220, 340, 123);
                    textblock.Text = "Aries 白羊座";
                    break;
                case 10:
                    SetStar(63, 49, 302, 315);await Task.Delay(100);  SetStar(42, 106, 323, 258);await Task.Delay(100);  SetStar(82, 135, 283, 229);await Task.Delay(100);  SetStar(98, 225, 267, 139);
                    await Task.Delay(100); SetStar(113, 292, 252, 72);await Task.Delay(100); SetStar(165, 334, 200, 30);await Task.Delay(100);  SetStar(200, 299, 165, 65); await Task.Delay(100); SetStar(219, 264, 146, 100);
                    await Task.Delay(100);  SetStar(234, 224, 131, 140);await Task.Delay(100);  SetStar(263, 150, 103, 214); await Task.Delay(100); SetStar(291, 92, 74, 273); await Task.Delay(100); SetStar(325, 73, 40, 291);
                    await Task.Delay(100);  SetStar(362, 77, 4, 287);await Task.Delay(100);  SetStar(360, 106, 6, 259);await Task.Delay(100);  SetStar(347, 126, 18, 239); await Task.Delay(100); SetStar(310, 125, 55, 240);
                    textblock.Text = "Pisces 双鱼座";
                    break;
                case 11:
                    SetStar(165, 36, 200, 328); SetStar(165, 59, 200, 305); SetStar(130, 76, 235, 288); SetStar(78, 120, 287, 244);
                    SetStar(26, 200, 339, 164); SetStar(78, 320, 287, 44); SetStar(98, 260, 267, 104); SetStar(130, 200, 235, 164);
                    SetStar(118, 140, 247, 224); SetStar(200, 94, 165, 270); SetStar(240, 59, 125, 305); SetStar(240, 140, 125, 224);
                    SetStar(266, 200, 99, 164); SetStar(306, 94, 59, 270); SetStar(365, 140, 0, 224);
                    textblock.Text = "Aquarius 水瓶座";
                    break;
                case 12:
                    SetStar(343, 37, 22, 327); SetStar(322, 101, 43, 263); SetStar(179, 140, 186, 224); SetStar(113, 122, 252, 242);
                    SetStar(55, 140, 310, 224); SetStar(15, 122, 350, 242); SetStar(39, 223, 326, 141); SetStar(75, 283, 290, 81);
                    SetStar(200, 342, 165, 22); SetStar(240, 308, 125, 56);
                    textblock.Text = "Capricornus 摩羯座";
                    break;
                case 13:
                    SetStar(186, 37, 179, 327); SetStar(96, 136, 269, 228); SetStar(36, 200, 329, 164); SetStar(36, 296, 329, 68);
                    SetStar(96, 316, 269, 48); SetStar(297, 316, 68, 48); SetStar(257, 150, 108, 214); SetStar(355, 116, 10, 248);
                    textblock.Text = "Sagittarius 射手座";
                    break;
                case 14:
                    SetStar(248, 34, 117, 330); SetStar(180, 10, 185, 354); SetStar(123, 34, 242, 330); SetStar(180, 90, 185, 274);
                    SetStar(180, 130, 185, 234); SetStar(226, 170, 139, 194); SetStar(263, 239, 102, 125); SetStar(340, 256, 25, 108);
                    SetStar(365, 306, 0, 58); SetStar(340, 346, 25, 18); SetStar(263, 326, 102, 38); SetStar(263, 311, 102, 53);
                    textblock.Text = "Scorpio 天蝎座";
                    break;
                case 15:
                    SetStar(200, 56, 165, 308); SetStar(89, 118, 276, 246); SetStar(306, 141, 59, 223); SetStar(134, 200, 231, 164);
                    SetStar(151, 303, 214, 61); SetStar(134, 200, 231, 164); SetStar(306, 282, 59, 82);
                    textblock.Text = "Libra 天秤座";
                    break;
                case 16:
                    SetStar(311, 96, 54, 268); SetStar(355, 136, 10, 228); SetStar(338, 200, 27, 164); SetStar(258, 151, 107, 213);
                    SetStar(200, 215, 165, 149); SetStar(178, 136, 187, 228); SetStar(165, 40, 200, 324); SetStar(93, 178, 272, 186);
                    SetStar(10, 230, 355, 134); SetStar(32, 315, 333, 49); SetStar(143, 335, 222, 29); SetStar(93, 254, 272, 110);
                    textblock.Text = "Virgo 处女座";
                    break;
                case 17:
                    SetStar(266, 36, 99, 338); SetStar(214, 24, 151, 350); SetStar(165, 98, 200, 276); SetStar(180, 151, 185, 223);
                    SetStar(266, 205, 99, 169); SetStar(293, 288, 72, 86); SetStar(82, 288, 283, 86); SetStar(24, 328, 341, 46); SetStar(68, 231, 297, 143);
                    textblock.Text = "Leo 狮子座";
                    break;
                case 18:
                    SetStar(165, 10, 200, 364); SetStar(183, 142, 182, 232); SetStar(165, 230, 200, 144); SetStar(51, 297, 314, 77);
                    SetStar(333, 189, 32, 185); SetStar(333, 341, 32, 33);
                    textblock.Text = "Scorpio 巨蟹座";
                    break;
            }
            //if (Animotion)
            //    storyboard2.Begin();
            //}

            textblock1.Text = "Click the Star for more.";
            
            if ((bool)_localSettings.Values["isAnimotion"])
            storyboard2.Begin();//这一句放在每个case里面会闪退

            if ((bool)_localSettings.Values["isWrite"])
                storyboard.Begin();

        }


    }        

}
