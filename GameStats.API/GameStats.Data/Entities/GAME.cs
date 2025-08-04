namespace GameStats.Data.Entities;

public class GAME
{
    public int GAME_ID { get; set; }
    public required string GAME_NAME { get; set; }

    public virtual ICollection<PLAYER>? PLAYERS { get; set; }
    public virtual ICollection<MAP>? MAPS { get; set; }
    public virtual ICollection<MATCH>? MATCHES { get; set; }
    public virtual ICollection<MATCH_TYPE>? MATCH_TYPES { get; set; }
}
