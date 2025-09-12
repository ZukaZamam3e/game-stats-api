namespace GameStats.Data.Entities;

public class MATCH_TYPE
{
    public int MATCH_TYPE_ID { get; set; }
    public required string MATCH_TYPE_NAME { get; set; }
    public int GAME_ID { get; set; }

    public virtual GAME? GAME { get; set; }
    public virtual ICollection<MATCH>? MATCHES { get; set; }
}
