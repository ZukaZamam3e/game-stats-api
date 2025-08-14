using FastEndpoints;
using FastEndpoints.Swagger;
using GameStats.API.Common;
using GameStats.API.SetUp;

namespace GameStats.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddProblemDetails();
        services.AddGameStatsDb(configuration);
        services.AddFastEndpoints()
            .SwaggerDocument();

        return services;
    }
}
