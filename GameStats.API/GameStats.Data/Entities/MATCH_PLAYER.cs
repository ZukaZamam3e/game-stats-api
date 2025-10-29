namespace GameStats.Data.Entities;

public class MATCH_PLAYER
{
    public int MATCH_PLAYER_ID { get; set; }
    public int MATCH_ID { get; set; }
    public int? MATCH_TEAM_ID { get; set; }
    public int PLAYER_ID { get; set; }
    public int SCORE { get; set; }

    public virtual MATCH? MATCH { get; set; }
    public virtual MATCH_TEAM? MATCH_TEAM { get; set; }
    public virtual PLAYER? PLAYER { get; set; }
}
