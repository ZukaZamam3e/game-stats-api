namespace GameStats.Model;

public class MatchTypeModel
{
    public int MatchTypeId { get; set; }
    public required string MatchTypeName { get; set; }
    public int GameId { get; set; }
}
