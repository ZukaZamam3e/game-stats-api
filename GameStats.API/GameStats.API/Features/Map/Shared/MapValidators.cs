using FastEndpoints;
using FluentValidation;

namespace GameStats.API.Features.Map.Shared
{
    public class CreateMapRequestValidator : Validator<CreateMapRequest>
    {
        public CreateMapRequestValidator()
        {
            RuleFor(x => x.MapName)
                .NotEmpty().WithMessage("Map name is required.")
                .MaximumLength(100).WithMessage("Map name must be 100 characters or fewer.");

            RuleFor(x => x.GameId)
                .GreaterThan(0).WithMessage("GameId must be a positive integer.");
        }
    }

    public class UpdateMapRequestValidator : Validator<UpdateMapRequest>
    {
        public UpdateMapRequestValidator()
        {
            RuleFor(x => x.MapId)
                .GreaterThan(0).WithMessage("MapId must be a positive integer.");

            RuleFor(x => x.MapName)
                .NotEmpty().WithMessage("Map name is required.")
                .MaximumLength(100).WithMessage("Map name must be 100 characters or fewer.");

            RuleFor(x => x.GameId)
                .GreaterThan(0).WithMessage("GameId must be a positive integer.");
        }
    }

    public class DeleteMapRequestValidator : Validator<DeleteMapRequest>
    {
        public DeleteMapRequestValidator()
        {
            RuleFor(x => x.MapId)
                .GreaterThan(0).WithMessage("MapId must be greater than 0.");
        }
    }
}