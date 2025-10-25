using System;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;

namespace editor_ui.Controls.Editor
{
    public sealed partial class EditorControl : UserControl
    {
        public EditorControl()
        {
            InitializeComponent();
            this.Loaded += EditorControl_Loaded;
            Wrapper.ActualThemeChanged += Wrapper_ActualThemeChanged;
        }

        private async void EditorControl_Loaded(object sender, RoutedEventArgs e)
        {
            await WebView.EnsureCoreWebView2Async();
            WebView.NavigationCompleted += WebView_NavigationCompleted;

            var core_wv2 = WebView.CoreWebView2;
            if (core_wv2 != null)
            {
                core_wv2.SetVirtualHostNameToFolderMapping(
                    "editor.app", "assets/EditorWeb",
                    Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow);

                WebView.Source = new Uri(@"https://editor.app/index.html");
            }
        }

        private async void WebView_NavigationCompleted(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            if (args.IsSuccess)
            {
                await SendThemeToWebView();
            }
        }

        private async void Wrapper_ActualThemeChanged(FrameworkElement sender, object args)
        {
            if (WebView.CoreWebView2 != null)
            {
                await SendThemeToWebView();
            }
        }

        private async Task SendThemeToWebView()
        {
            string theme = Wrapper.ActualTheme.ToString().ToLower();

            var core_wv2 = WebView.CoreWebView2;
            if (core_wv2 != null)
            {
                await core_wv2.ExecuteScriptAsync($"setEditorTheme('{theme}')");
            }
        }
    }
}
