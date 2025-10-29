using FastEndpoints;
using GameStats.API.Features.Map.Shared.Responses;
using GameStats.API.Features.MatchTypes.Shared;
using GameStats.API.Features.MatchTypes.Shared.Responses;
using GameStats.Model;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.MatchTypes;

public sealed record CreateMatchTypeRequest
{
    public required string MatchTypeName { get; set; }
    public int GameId { get; set; }
}

public class CreateMatchTypeEndpoint(IMatchTypeStore matchTypeStore) : Endpoint<CreateMatchTypeRequest, MatchTypeResponse>
{
    public override void Configure()
    {
        Post("/api/matchtype/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateMatchTypeRequest request, CancellationToken cancellationToken)
    {
        var matchType = await matchTypeStore.CreateMatchType(new MatchTypeModel
        {
            MatchTypeName = request.MatchTypeName,
            GameId = request.GameId
        });

        if (matchType == null)
            await Send.ErrorsAsync(400, cancellationToken);
        else
            await Send.OkAsync(matchType.MapToResponse(), cancellationToken);
    }
}
