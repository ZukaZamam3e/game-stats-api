using FastEndpoints;
using FluentValidation;

namespace GameStats.API.Features.Match.Shared
{
    public class CreateMatchValidator : Validator<CreateMatchRequest>
    {
        public CreateMatchValidator()
        {
            RuleFor(x => x.GameId)
                .GreaterThan(0)
                .WithMessage("Game ID must be greater than 0.");

            RuleFor(x => x.MatchName)
                .NotEmpty()
                .WithMessage("Match name cannot be empty.")
                .MaximumLength(100)
                .WithMessage("Match name cannot exceed 100 characters.");

            RuleFor(x => x.OldMatchId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Old match ID must be 0 or greater.");

            RuleFor(x => x.TypeCd)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Type code must be 0 or greater.");

            RuleFor(x => x.MapId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Map ID must be 0 or greater.");

            When(x => x.TotalTime.HasValue, () =>
            {
                RuleFor(x => x.TotalTime.Value)
                    .GreaterThan(0)
                    .WithMessage("Total time must be greater than 0 when specified.");
            });
        }
    }

    public class UpdateMatchValidator : Validator<UpdateMatchRequest>
    {
        public UpdateMatchValidator()
        {
            RuleFor(x => x.MatchId)
                .GreaterThan(0)
                .WithMessage("Match ID must be greater than 0.");

            RuleFor(x => x.GameId)
                .GreaterThan(0)
                .WithMessage("Game ID must be greater than 0.");

            RuleFor(x => x.MatchName)
                .NotEmpty()
                .WithMessage("Match name cannot be empty.")
                .MaximumLength(100)
                .WithMessage("Match name cannot exceed 100 characters.");

            RuleFor(x => x.OldMatchId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Old match ID must be 0 or greater.");

            RuleFor(x => x.TypeCd)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Type code must be 0 or greater.");

            RuleFor(x => x.MapId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Map ID must be 0 or greater.");

            When(x => x.TotalTime.HasValue, () =>
            {
                RuleFor(x => x.TotalTime.Value)
                    .GreaterThan(0)
                    .WithMessage("Total time must be greater than 0 when specified.");
            });
        }
    }

    public class DeleteMatchValidator : Validator<DeleteMatchRequest>
    {
        public DeleteMatchValidator()
        {
            RuleFor(x => x.MatchId)
                .GreaterThan(0)
                .WithMessage("Match ID must be greater than 0.");
        }
    }
}
