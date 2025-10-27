namespace GameStats.Data.Entities;

public class MATCH_TEAM
{
    public int MATCH_TEAM_ID { get; set; }

    public int MATCH_ID { get; set; }

    public required string TEAM_COLOR { get; set; }

    public virtual MATCH? MATCH { get; set; }

    public virtual ICollection<MATCH_PLAYER>? MATCH_PLAYERS { get; set; }

}
