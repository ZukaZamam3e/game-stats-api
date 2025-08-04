namespace GameStats.Data.Entities;

public class MAP
{
    public int MAP_ID { get; set; }
    public required string MAP_NAME { get; set; }
    public int GAME_ID { get; set; }

    public required GAME GAME { get; set; }
    public virtual ICollection<MATCH>? MATCHES { get; set; }
}
