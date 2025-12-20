namespace GameStats.Model;

public class MatchPlayerModel
{
    public int MatchPlayerId { get; set; }

    public int MatchId { get; set; }

    public int? MatchTeamId { get; set; }

    public int PlayerId { get; set; }

    public int Score { get; set; }
}
