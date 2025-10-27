namespace GameStats.Model;

public class MatchTeamModel
{
    public int MatchTeamId { get; set; }

    public int MatchId { get; set; }

    public required string TeamColor { get; set; }
}
