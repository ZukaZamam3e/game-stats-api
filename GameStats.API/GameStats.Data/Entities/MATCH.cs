namespace GameStats.Data.Entities;

public class MATCH
{
    public int MATCH_ID { get; set; }

    public int GAME_ID { get; set; }

    public int MATCH_NAME { get; set; }

    public int TYPE_CD { get; set; }

    public int MAP_ID { get; set; }

    public int? TOTAL_TIME { get; set; }

    public DateTime? CREATED_DATE { get; set; }

    public required GAME GAME { get; set; }
    public required MAP MAP { get; set; }
    public required MATCH_TYPE MATCH_TYPE { get; set; }
    public required ICollection<MATCH_TEAM> MATCH_TEAM { get; set; }
}
