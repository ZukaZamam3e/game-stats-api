using FastEndpoints;
using FluentValidation;

namespace GameStats.API.Features.MatchTeam.Shared;

public class CreateMatchTeamValidator : Validator<CreateMatchTeamRequest>
{
    public CreateMatchTeamValidator()
    {
        RuleFor(x => x.MatchId)
            .GreaterThan(0)
            .WithMessage("Match ID must be greater than 0.");

        RuleFor(x => x.TeamColor)
            .NotEmpty()
            .WithMessage("Team color is required.")
            .MaximumLength(50)
            .WithMessage("Team color cannot exceed 50 characters.");
    }
}

public class UpdateMatchTeamValidator : Validator<UpdateMatchTeamRequest>
{
    public UpdateMatchTeamValidator()
    {
        RuleFor(x => x.MatchTeamId)
            .GreaterThan(0)
            .WithMessage("Match Team ID must be greater than 0.");

        RuleFor(x => x.MatchId)
            .GreaterThan(0)
            .WithMessage("Match ID must be greater than 0.");

        RuleFor(x => x.TeamColor)
            .NotEmpty()
            .WithMessage("Team color is required.")
            .MaximumLength(50)
            .WithMessage("Team color cannot exceed 50 characters.");
    }
}

public class DeleteMatchTeamValidator : Validator<DeleteMatchTeamRequest>
{
    public DeleteMatchTeamValidator()
    {
        RuleFor(x => x.MatchTeamId)
            .GreaterThan(0)
            .WithMessage("Match Team ID must be greater than 0.");
    }
}
