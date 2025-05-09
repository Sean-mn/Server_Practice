namespace Server.Models;

public class PlayerRequest
{
    public int PlayerId { get; set; }
    public string? PlayerName { get; set; } 
    public int PlayerScore { get; set; }
}