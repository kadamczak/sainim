using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sainim.Models;

namespace sainim.WPF.HostBuilders
{
    public static class AddServicesHostBuilderExtension
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<OriginalImageFactory>();
                services.AddSingleton<FrameRenderer>();
            });

            return hostBuilder;
        }
    }
}