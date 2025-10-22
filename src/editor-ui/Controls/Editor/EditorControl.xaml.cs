using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Web.WebView2.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace editor_ui.Controls.Editor
{
    public sealed partial class EditorControl : UserControl
    {
        public EditorControl()
        {
            InitializeComponent();
            this.Loaded += EditorControl_Loaded;
        }

        private async void EditorControl_Loaded(object sender, RoutedEventArgs e)
        {
            await WebView.EnsureCoreWebView2Async();
            var core_wv2 = WebView.CoreWebView2;
            if (core_wv2 != null)
            {
                core_wv2.SetVirtualHostNameToFolderMapping(
                    "editor.app", "assets/EditorWeb",
                    Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow);

                WebView.Source = new Uri(@"https://editor.app/index.html");
            }
        }
    }
}
