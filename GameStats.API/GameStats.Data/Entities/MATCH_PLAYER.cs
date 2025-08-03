namespace GameStats.Data.Entities;

public class MATCH_PLAYER
{
    public int MATCH_PLAYER_ID { get; set; }
    public int MATCH_TEAM_ID { get; set; }
    public int PLAYER_ID { get; set; }
    public int TEAM_COLOR { get; set; }
    public int SCORE { get; set; }

    public required MATCH_TEAM MATCH_TEAM { get; set; }
    public required PLAYER PLAYER { get; set; }
}
