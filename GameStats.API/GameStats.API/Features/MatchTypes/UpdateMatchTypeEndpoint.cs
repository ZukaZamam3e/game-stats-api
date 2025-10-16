using FastEndpoints;
using GameStats.API.Features.MatchTypes.Shared;
using GameStats.API.Features.MatchTypes.Shared.Responses;
using GameStats.Model;
using GameStats.Store.Interfaces;

namespace GameStats.API.Features.MatchTypes;

public class UpdateMatchTypeRequest
{
    public int MatchTypeId { get; set; }
    public required string MatchTypeName { get; set; }
    public int GameId { get; set; }
}
public class UpdateMatchTypeEndpoint(IMatchTypeStore matchTypeStore) : Endpoint<UpdateMatchTypeRequest, MatchTypeResponse>
{
    public override void Configure()
    {
        Post("/api/matchtype/update");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateMatchTypeRequest request, CancellationToken cancellationToken)
    {
        var matchType = await matchTypeStore.UpdateMatchType(new MatchTypeModel
        {
            MatchTypeId = request.MatchTypeId,
            MatchTypeName = request.MatchTypeName,
            GameId = request.GameId
        });

        if (matchType == null)
            await Send.ErrorsAsync(400, cancellationToken);
        else
            await Send.OkAsync(matchType.MapToResponse(), cancellationToken);
    }
}
