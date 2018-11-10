using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Ninject;

namespace Launcher
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IKernel _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow?.Show();
        }

        private void ConfigureContainer()
        {
            _container = new StandardKernel();
            
        }

        private void ComposeObjects()
        {
            Current.MainWindow = _container.Get<MainWindow>();
            Current.MainWindow.Title = "Launcher";
            Current.MainWindow.Icon = new BitmapImage(new Uri("launcher.ico", UriKind.Relative));
        }
    }
}
