using StarCapturePlan.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

 //“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace StarCapturePlan
{
    public sealed partial class CollectionPage : Page
    {

        private ObservableCollection<int> numberCollection = new ObservableCollection<int>();

        private ObservableCollection<ThePlanets> theplanetsCollection = new ObservableCollection<ThePlanets>();
       
        private ThePlanets[] theplanetsarray = new ThePlanets[19];

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        //ThePlanets tp = new ThePlanets{ ImagePath1 = "/Assets/d0.jpg", name = "水星 Mercury" };
        //public bool IsLoading = false;
        //private object o = new object();

        int number;
        public CollectionPage()
        {
            this.InitializeComponent();
            //listview.ContainerContentChanging += listView_ContainerContentChanging;
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;


            setarray();

            //for (int i = 0; i < mysqlite.numberCollection.Count; i++)
            //{
            //    theplanetsCollection.Add(theplanetsarray[int.Parse(mysqlite.numberCollection[i])]);
            //}
            
            listview.ItemsSource = theplanetsCollection;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            numberCollection = (ObservableCollection<int>)e.Parameter;

            setarray();

            for (int i = 0; i < numberCollection.Count; i++)
            {
                theplanetsCollection.Add(theplanetsarray[numberCollection[i]]);
            }

            listview.ItemsSource = theplanetsCollection;
        }

        void setarray()
        {
            theplanetsarray[0] = new ThePlanets { ImagePath1 = "/Assets/d0.jpg", name = "水星 Mercury", number = 0 ,onesentence="最靠近太阳的行星"};//水星
            theplanetsarray[1] = new ThePlanets { ImagePath1 = "/Assets/d1.jpg", name = "金星 Venus", number = 1, onesentence="夜空中最亮的行星" };//金星
            theplanetsarray[2] = new ThePlanets { ImagePath1 = "/Assets/d2.jpg", name = "火星 Mars", number = 2, onesentence="地球的兄弟星" };//火星
            theplanetsarray[3] = new ThePlanets { ImagePath1 = "/Assets/d3.jpg", name = "木星 Jupiter", number = 3, onesentence="当之无愧的Jupiter" };//木星
            theplanetsarray[4] = new ThePlanets { ImagePath1 = "/Assets/d4.jpg", name = "土星 Saturn", number = 4, onesentence="独一无二的壮丽星环" };//土星
            theplanetsarray[5] = new ThePlanets { ImagePath1 = "/Assets/d5.jpg", name = "天王星 Uranus", number = 5, onesentence="躺倒的行星" };//天王星
            theplanetsarray[6] = new ThePlanets { ImagePath1 = "/Assets/d6.jpg", name = "海王星 Neptune", number = 6, onesentence="笔尖下的第八颗行星" };//海王星
            theplanetsarray[7] = new ThePlanets { ImagePath1 = "/Assets/d7.jpg", name = "双子座 Gemini", number = 7, onesentence="卡斯托和普尔尤克斯" };//双子座
            theplanetsarray[8] = new ThePlanets { ImagePath1 = "/Assets/d8.jpg", name = "金牛座 Taurus", number = 8, onesentence="两星团加一星云" };//金牛座
            theplanetsarray[9] = new ThePlanets { ImagePath1 = "/Assets/d9.jpg", name = "白羊座 Aries", number = 9, onesentence="小而暗的白羊星座" };//白羊座
            theplanetsarray[10] = new ThePlanets { ImagePath1 = "/Assets/d10.jpg", name = "双鱼座 Pisces", number = 10, onesentence="化身双鱼的母与子" };//双鱼座
            theplanetsarray[11] = new ThePlanets { ImagePath1 = "/Assets/d11.jpg", name = "水瓶座 Aquarius", number = 11, onesentence="大而暗的宝瓶座" };//水瓶座
            theplanetsarray[12] = new ThePlanets { ImagePath1 = "/Assets/d12.jpg", name = "摩羯座 Capricornus", number = 12, onesentence="“神仙之门”倒三角" };//摩羯座
            theplanetsarray[13] = new ThePlanets { ImagePath1 = "/Assets/d13.jpg", name = "射手座 Sagittarius", number = 13, onesentence="来自银河深处的射手座" };//射手座
            theplanetsarray[14] = new ThePlanets { ImagePath1 = "/Assets/d14.jpg", name = "天蝎座 Scorpio", number = 14, onesentence="“天蝎之心”心宿二" };//天蝎座
            theplanetsarray[15] = new ThePlanets { ImagePath1 = "/Assets/d15.jpg", name = "天秤座 Libra", number = 15, onesentence="群星里的一杆称" };//天秤座
            theplanetsarray[16] = new ThePlanets { ImagePath1 = "/Assets/d16.jpg", name = "处女座 Virgo", number = 16, onesentence="最大的黄道带星座" };//处女座
            theplanetsarray[17] = new ThePlanets { ImagePath1 = "/Assets/d17.jpg", name = "狮子座 Leo", number = 17, onesentence="每年11月的流星雨" };//狮子座
            theplanetsarray[18] = new ThePlanets { ImagePath1 = "/Assets/d18.jpg", name = "巨蟹座 Cancer", number = 18, onesentence="暗淡的“鬼星团" };//巨蟹座
        }

        //void listView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        //{
        //    lock (o)
        //    {
        //        if (!IsLoading)
        //        {
        //            if (args.ItemIndex == listview.Items.Count - 1)
        //            {
        //                IsLoading = true;
        //                Task.Factory.StartNew(async () =>
        //                {
        //                    await Task.Delay(3000);
        //                    await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
        //                    {
        //                        IsLoading = false;
        //                    });
        //                });
        //            }
        //        }
        //    }
        //}

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        private void listview_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (listview.SelectedItem != null)
            {
                ThePlanets theplanet = listview.SelectedItem as ThePlanets;
                number = theplanet.number;
            }
            Frame.Navigate(typeof(Details), number);
        }

        private void HelpAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //numberCollection.Clear();
        }

        private void SettingsAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingPage));
        }

        private void CollectionPage_Click(object sender, RoutedEventArgs e)
        {
        //    Frame.Navigate(typeof(CollectionPage));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

    }
}
