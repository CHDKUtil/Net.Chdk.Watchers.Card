using Microsoft.Extensions.DependencyInjection;
using System;

namespace Net.Chdk.Watchers.Volume
{
    public static class ServiceCollectionExtensions
    {
        [Obsolete]
        public static IServiceCollection AddVolumeWatcher(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<IVolumeWatcher, VolumeWatcher>();
        }
    }
}
