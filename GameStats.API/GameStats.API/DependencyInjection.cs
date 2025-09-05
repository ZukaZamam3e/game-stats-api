using FastEndpoints;
using FastEndpoints.Swagger;
using GameStats.API.Common;
using GameStats.Data.Context;
using GameStats.Store;
using GameStats.Store.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStats.API;

public static class DependencyInjection
{
    public static IServiceCollection AddGameStatsDb(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("GameStatsConnection");
        services.AddDbContext<GameStatsDbContext>(m => m.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), m => m.MigrationsHistoryTable("__GS_EFMigrationsHistory")), ServiceLifetime.Transient);

        services.AddTransient<IGameStore, GameStore>();
        services.AddTransient<IMapStore, MapStore>();
        services.AddTransient<IPlayerStore, PlayerStore>();

        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddProblemDetails();
        services.AddGameStatsDb(configuration);
        services.AddFastEndpoints()
            .SwaggerDocument(o => o.AutoTagPathSegmentIndex = 2);

        return services;
    }
}
