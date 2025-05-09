using System.Collections.Concurrent;

namespace Server.Models;

public static class PlayerData
{
    public static readonly ConcurrentDictionary<int, string> RegisteredPlayers = new();
    public static readonly ConcurrentDictionary<int, int> PlayerScores = new(); 
}