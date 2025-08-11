using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace GameStats.API.Common;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    private readonly IValidator<T> _validator;

    public ValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var obj = context.Arguments.FirstOrDefault(x => x?.GetType() == typeof(T)) as T;

        if (obj is null)
        {
            return Results.BadRequest(); // Or handle as appropriate
        }

        var validationResult = await _validator.ValidateAsync(obj);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        return await next(context); // Proceed to the next filter or endpoint handler
    }
}