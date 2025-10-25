using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using System.Threading.Tasks;
using Windows.Graphics;

namespace editor_ui
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.ExtendsContentIntoTitleBar = true;
            this.AppWindow.IsShownInSwitchers = false;

            OverlappedPresenter presenter = OverlappedPresenter.Create();
            presenter.IsMaximizable = false;
            presenter.IsResizable = false;
            presenter.IsMinimizable = false;
            presenter.IsAlwaysOnTop = true;

            AppWindow.Resize(new SizeInt32(550, 450));
            AppWindow.SetPresenter(presenter);
        }
    }
}
