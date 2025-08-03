namespace GameStats.Data.Entities;

public class GAME
{
    public int GAME_ID { get; set; }
    public required string GAME_NAME { get; set; }

    public required ICollection<PLAYER> PLAYERS { get; set; }
    public required ICollection<MAP> MAPS { get; set; }
    public required ICollection<MATCH> MATCHES { get; set; }
    public required ICollection<MATCH_TYPE> MATCH_TYPES { get; set; }
}
