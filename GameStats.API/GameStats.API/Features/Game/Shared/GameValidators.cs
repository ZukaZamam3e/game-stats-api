using FastEndpoints;
using FluentValidation;

namespace GameStats.API.Features.Game.Shared
{
    public class CreateGameValidator : Validator<CreateGameRequest>
    {
        public CreateGameValidator()
        {
            RuleFor(x => x.GameName)
                .NotEmpty()
                .WithMessage("Game name cannot be empty.")
                .MaximumLength(100)
                .WithMessage("Game name cannot exceed 100 characters.");
        }
    }

    public class UpdateGameValidator : Validator<UpdateGameRequest>
    {
        public UpdateGameValidator()
        {
            RuleFor(x => x.GameId)
                .GreaterThan(0)
                .WithMessage("Game ID must be greater than 0.");

            RuleFor(x => x.GameName)
                .NotEmpty()
                .WithMessage("Game name cannot be empty.")
                .MaximumLength(100)
                .WithMessage("Game name cannot exceed 100 characters.");
        }
    }

    public class DeleteGameValidator : Validator<DeleteGameRequest>
    {
        public DeleteGameValidator()
        {
            RuleFor(x => x.GameId)
                .GreaterThan(0)
                .WithMessage("Game ID must be greater than 0.");
        }
    }
}
