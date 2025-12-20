namespace GameStats.API.Features.Shared.Responses;

public sealed record DataResponse<T>(IEnumerable<T> Data, int Count);
