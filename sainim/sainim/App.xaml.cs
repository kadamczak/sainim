using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sainim.WPF.HostBuilders;
using sainim.WPF.ViewModels;
using System.Windows;

namespace sainim
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddViewModels()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            //mainWindow.Show();

            base.OnStartup(e);
        }
    }
}