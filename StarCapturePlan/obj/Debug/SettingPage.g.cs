﻿

#pragma checksum "C:\Users\蕴盈\Documents\Visual Studio 2013\Projects\StarCapturePlan\StarCapturePlan\SettingPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "041FB5454D04732B2C0323145EFE6C4B"
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
                #line 22 "..\..\SettingPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ToggleSwitch)(target)).Toggled += this.NetworkButton_Toggled;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 23 "..\..\SettingPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ToggleSwitch)(target)).Toggled += this.WriteButton_Toggled;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 24 "..\..\SettingPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ToggleSwitch)(target)).Toggled += this.AnimotionButton_Toggled;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


