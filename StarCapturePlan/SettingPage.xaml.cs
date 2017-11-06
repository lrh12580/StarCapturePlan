using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace StarCapturePlan
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingPage : Page
    {

        Windows.Storage.ApplicationDataContainer _localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
       
        //GetStatistics getstatistics = new GetStatistics();
        public SettingPage()
        {
            this.InitializeComponent();
            
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            CheckSetting();ReadSetting();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            SaveSetting();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            e.Handled = true;
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        public static bool GetWriteButtonValue()
        {
            SettingPage page1 = new SettingPage();
            var ison = page1.WriteButton.IsOn;
            return ison;
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

        private void ReadSetting()
        {
            NetworkButton.IsOn = (bool)_localSettings.Values["isNetwork"];
            WriteButton.IsOn = (bool)_localSettings.Values["isWrite"];
            AnimotionButton.IsOn = (bool)_localSettings.Values["isAnimotion"];
            MessageButton.IsOn = (bool)_localSettings.Values["isMessage"];
        }

        public void SaveSetting()
        {
            _localSettings.Values["isNetwork"] = NetworkButton.IsOn;
            _localSettings.Values["isWrite"] = WriteButton.IsOn;
            _localSettings.Values["isAnimotion"] = AnimotionButton.IsOn;
            _localSettings.Values["isMessage"] = AnimotionButton.IsOn;
        }

        private void NetworkButton_Toggled(object sender, RoutedEventArgs e)
        {

        }

        private void AnimotionButton_Toggled(object sender, RoutedEventArgs e)
        {

        }

        private void WriteButton_Toggled(object sender, RoutedEventArgs e)
        {

        }

        private void CollectionPage_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof(CollectionPage));
        }

        private void SettingsAppBarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HelpAppBarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void MessagenButton_Toggled(object sender, RoutedEventArgs e)
        {

        }
    }
}
