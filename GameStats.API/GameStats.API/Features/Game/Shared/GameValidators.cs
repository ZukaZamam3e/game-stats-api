using FluentValidation;

namespace GameStats.API.Features.Game.Shared
{
    public class CreateGameValidator : AbstractValidator<CreateGameRequest>
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

    public class UpdateGameValidator : AbstractValidator<UpdateGameRequest>
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

    public class DeleteGameValidator : AbstractValidator<DeleteGameRequest>
    {
        public DeleteGameValidator()
        {
            RuleFor(x => x.GameId)
                .GreaterThan(0)
                .WithMessage("Game ID must be greater than 0.");
        }
    }
}
