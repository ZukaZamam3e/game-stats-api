namespace GameStats.Data.Entities;

public class PLAYER
{
    public int PLAYER_ID { get; set; }

    public string PLAYER_NAME { get; set; } = null!;

    public int GAME_ID { get; set; }

    public string EMBLEM { get; set; } = null!;

    public virtual GAME? GAME { get; set; }
    public virtual ICollection<MATCH_PLAYER>? MATCH_PLAYERS { get; set; }
}
