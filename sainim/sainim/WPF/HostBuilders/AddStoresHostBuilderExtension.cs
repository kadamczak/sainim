using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sainim.WPF.Stores;

namespace sainim.WPF.HostBuilders
{
    public static class AddStoresHostBuilderExtension
    {
        public static IHostBuilder AddStores(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<OriginalImageStore>();
            });

            return hostBuilder;
        }
    }
}