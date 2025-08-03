using GameStats.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GameStats.API.SetUp;

public static class GameStatsSetup
{
    public static IServiceCollection AddGameStatsDI(this IServiceCollection services, ConfigurationManager configuration)
    {
        string? connectionString = configuration.GetConnectionString("GameStatsConnection");
        services.AddDbContext<GameStatsDbContext>(m => m.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), m => m.MigrationsHistoryTable("__GS_EFMigrationsHistory")), ServiceLifetime.Transient);

        return services;
    }
}