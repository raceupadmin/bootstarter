using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using bootstarter.ViewModels;
using bootstarter.Views;

namespace bootstarter
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new mainWnd()
                {
                    DataContext = new mainVM(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
