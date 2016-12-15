namespace SparkPostDotNet.Core
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceExtensions
    {
        public static IServiceCollection AddSparkPost(this IServiceCollection services)
        {
            return services.AddSingleton<SparkPostClient>();
                //.AddTransient<SparkPostClient>();
        }
    }
}
