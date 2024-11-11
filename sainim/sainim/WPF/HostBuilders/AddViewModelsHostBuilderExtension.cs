using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sainim.WPF.ViewModels;

namespace sainim.WPF.HostBuilders
{
    public static class AddViewModelsHostBuilderExtension
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<AnimationPreviewViewModel>();
                services.AddSingleton<ContentBarViewModel>();
                services.AddSingleton<TimelineBarViewModel>();
                services.AddSingleton<PlayBarViewModel>();
                services.AddSingleton<MenuBarViewModel>();
                services.AddSingleton<AnimationTimelineViewModel>();

                services.AddSingleton<MainViewModel>();
            });

            return hostBuilder;
        }
    }
}