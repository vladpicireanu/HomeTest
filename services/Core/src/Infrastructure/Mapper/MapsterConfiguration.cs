using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Mapper
{
    public static class MapsterConfiguration
    {
        public static IServiceCollection AddMapster(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}