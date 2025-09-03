using FastEndpoints;
using FluentValidation;

namespace GameStats.API.Features.Player.Shared;

public class PlayerValidators
{
    public class CreatePlayerValidator : Validator<CreatePlayerRequest>
    {
        public CreatePlayerValidator()
        {
            RuleFor(x => x.GameId)
                .GreaterThan(0)
                .WithMessage("Game ID must be greater than 0.");

            RuleFor(x => x.PlayerName)
                .NotEmpty()
                .WithMessage("Player name cannot be empty.")
                .MaximumLength(100)
                .WithMessage("Player name cannot exceed 100 characters.");

            RuleFor(x => x.Emblem)
                .NotEmpty()
                .WithMessage("Emblem cannot be empty.");
        }
    }

    public class UpdatePlayerValidator : Validator<UpdatePlayerRequest>
    {
        public UpdatePlayerValidator()
        {
            RuleFor(x => x.PlayerId)
                .GreaterThan(0)
                .WithMessage("Player ID must be greater than 0.");

            RuleFor(x => x.GameId)
                .GreaterThan(0)
                .WithMessage("Game ID must be greater than 0.");

            RuleFor(x => x.PlayerName)
                .NotEmpty()
                .WithMessage("Player name cannot be empty.")
                .MaximumLength(100)
                .WithMessage("Player name cannot exceed 100 characters.");

            RuleFor(x => x.Emblem)
                .NotEmpty()
                .WithMessage("Emblem cannot be empty.");
        }
    }

    public class DeletePlayerValidator : Validator<DeletePlayerRequest>
    {
        public DeletePlayerValidator()
        {
            RuleFor(x => x.PlayerId)
                .GreaterThan(0)
                .WithMessage("Player ID must be greater than 0.");
        }
    }
}
