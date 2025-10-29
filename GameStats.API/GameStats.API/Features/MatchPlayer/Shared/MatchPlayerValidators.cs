using FastEndpoints;
using FluentValidation;

namespace GameStats.API.Features.MatchPlayer.Shared;

public class CreateMatchPlayerValidator : Validator<CreateMatchPlayerRequest>
{
    public CreateMatchPlayerValidator()
    {
        RuleFor(x => x.MatchId)
            .GreaterThan(0)
            .WithMessage("Match ID must be greater than 0.");

        RuleFor(x => x.PlayerId)
            .GreaterThan(0)
            .WithMessage("Player ID must be greater than 0.");

        RuleFor(x => x.Score)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Score must be 0 or greater.");

        // MatchTeamId is optional, but if provided, must be > 0
        When(x => x.MatchTeamId.HasValue, () =>
        {
            RuleFor(x => x.MatchTeamId.Value)
                .GreaterThan(0)
                .WithMessage("Match Team ID must be greater than 0 when specified.");
        });
    }
}

public class UpdateMatchPlayerValidator : Validator<UpdateMatchPlayerRequest>
{
    public UpdateMatchPlayerValidator()
    {
        RuleFor(x => x.MatchPlayerId)
            .GreaterThan(0)
            .WithMessage("Match Player ID must be greater than 0.");

        RuleFor(x => x.MatchId)
            .GreaterThan(0)
            .WithMessage("Match ID must be greater than 0.");

        RuleFor(x => x.PlayerId)
            .GreaterThan(0)
            .WithMessage("Player ID must be greater than 0.");

        RuleFor(x => x.Score)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Score must be 0 or greater.");

        When(x => x.MatchTeamId.HasValue, () =>
        {
            RuleFor(x => x.MatchTeamId.Value)
                .GreaterThan(0)
                .WithMessage("Match Team ID must be greater than 0 when specified.");
        });
    }
}

public class DeleteMatchPlayerValidator : Validator<DeleteMatchPlayerRequest>
{
    public DeleteMatchPlayerValidator()
    {
        RuleFor(x => x.MatchPlayerId)
            .GreaterThan(0)
            .WithMessage("Match Player ID must be greater than 0.");
    }
}
