using GameStats.API.Features.Game;
using GameStats.Data.Context;
using GameStats.Store;
using GameStats.Store.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameStats.API.SetUp;

public static class GameStatsSetup
{
    public static IServiceCollection AddGameStatsDb(this IServiceCollection services, ConfigurationManager configuration)
    {
        string? connectionString = configuration.GetConnectionString("GameStatsConnection");
        services.AddDbContext<GameStatsDbContext>(m => m.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), m => m.MigrationsHistoryTable("__GS_EFMigrationsHistory")), ServiceLifetime.Transient);

        services.AddTransient<IGameStore, GameStore>();

        return services;
    }
}