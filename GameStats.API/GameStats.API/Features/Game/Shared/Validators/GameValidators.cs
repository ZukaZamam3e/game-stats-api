using FluentValidation;
using GameStats.API.Features.Game.Create;

namespace GameStats.API.Features.Game.Shared.Validators
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
}
