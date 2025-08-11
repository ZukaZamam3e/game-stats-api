using FluentValidation;
using GameStats.API.Abstract;
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
        services.AddSwaggerGen();
        services.AddProblemDetails();
        services.RegisterEndpointsFromAssemblyContaining<IApiMarker>();
        services.AddGameStatsDb(configuration);
        services.AddValidatorsFromAssemblyContaining<IApiMarker>();

        return services;
    }
}
