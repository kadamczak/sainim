using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sainim.WPF.Helpers;

namespace sainim.WPF.HostBuilders
{
    public static class AddHelpersHostBuilderExtension
    {
        public static IHostBuilder AddHelpers(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<MessageBoxHelpers>();
            });

            return hostBuilder;
        }
    }
}