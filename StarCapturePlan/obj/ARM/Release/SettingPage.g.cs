﻿

#pragma checksum "G:\c# project\StarCapturePlan\StarCapturePlan\SettingPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D262CBC8FCFA6FB4DBFF999BDF39EA26"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StarCapturePlan
{
    partial class SettingPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 23 "..\..\..\SettingPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ToggleSwitch)(target)).Toggled += this.NetworkButton_Toggled;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 24 "..\..\..\SettingPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ToggleSwitch)(target)).Toggled += this.WriteButton_Toggled;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 25 "..\..\..\SettingPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ToggleSwitch)(target)).Toggled += this.AnimotionButton_Toggled;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 26 "..\..\..\SettingPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ToggleSwitch)(target)).Toggled += this.MessagenButton_Toggled;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 37 "..\..\..\SettingPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.CollectionPage_Click;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 43 "..\..\..\SettingPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SettingsAppBarButton_Click;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 47 "..\..\..\SettingPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.HelpAppBarButton_Click;
                 #line default
                 #line hidden
                break;
            case 8:
                #line 50 "..\..\..\SettingPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Home_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


