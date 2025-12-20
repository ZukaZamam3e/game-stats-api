namespace GameStats.Model;

public class MatchModel
{
    public int MatchId { get; set; }
    public int OldMatchId { get; set; }
    public int GameId { get; set; }
    public required string MatchName { get; set; }
    public int TypeCd { get; set; }
    public int MapId { get; set; }
    public int? TotalTime { get; set; }
    public DateTime? CreatedDate { get; set; }
}
