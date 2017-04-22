using Microsoft.Extensions.DependencyInjection;

namespace Net.Chdk.Watchers.Card
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCardWatcher(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<ICardWatcher, CardWatcher>();
        }
    }
}
