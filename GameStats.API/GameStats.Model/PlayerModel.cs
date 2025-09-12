namespace GameStats.Model;

public class PlayerModel
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; } = null!;
    public int GameId { get; set; }
    public string Emblem { get; set; } = null!;
}
