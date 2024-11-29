using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sainim.WPF.Commands.SettingsCommands;
using sainim.WPF.HostBuilders;
using sainim.WPF.ViewModels;
using System.Windows;

namespace sainim
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .AddViewModels()
                .AddStores()
                .AddServices()
                .AddHelpers()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });

                    services.AddSingleton<IConfiguration>(context.Configuration);
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            LoadStringResources();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }

        private void LoadStringResources()
        {
            var configuration = _host.Services.GetRequiredService<IConfiguration>();
            string lastUsedLanguage = configuration["AppSettings:AppLanguage"];

            LoadStringResourcesCommand loadStringResourcesCommand = new LoadStringResourcesCommand(lastUsedLanguage);
            loadStringResourcesCommand.Execute();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}