﻿

#pragma checksum "C:\Users\YYY\Desktop\StarCapturePlan (2)\StarCapturePlan\StarCapturePlan\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8E80D616D17E2596615BB26555DF6B4E"
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
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 85 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.drawGrid_Tapped;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 111 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.CollectionPage_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 116 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SettingsAppBarButton_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 121 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.HelpAppBarButton_Click;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 127 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Collect_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


