using System;

[System.Serializable]
public class PlayerRequest
{
    public int playerId;
    public string playerName;
    public int playerScore;

    public PlayerRequest() { }

    public PlayerRequest(int playerId, string playerName, int playerScore)
    {
        this.playerId = playerId;
        this.playerName = playerName;
        this.playerScore = playerScore;
    }

    public PlayerRequest(int playerId, string playerName)
    {
        this.playerId = playerId;
        this.playerName = playerName;
    }
}
