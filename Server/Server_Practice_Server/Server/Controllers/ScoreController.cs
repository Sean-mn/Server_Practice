using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        // 점수 업데이트
        [HttpPost("update")]
        public IActionResult UpdateScore([FromBody] PlayerRequest req)
        {
            if (!PlayerData.RegisteredPlayers.ContainsKey(req.PlayerId))
                return BadRequest(new { message = $"등록되지 않은 ID: {req.PlayerId}" });
            
            PlayerData.PlayerScores.AddOrUpdate(req.PlayerId, req.PlayerScore, (key, oldValue) => oldValue + req.PlayerScore);

            return Ok(new
            {
                message = $"플레이어 {PlayerData.RegisteredPlayers[req.PlayerId]}의 현재 점수: {PlayerData.PlayerScores[req.PlayerId]}",
                playerId = req.PlayerId,
                playerScore = PlayerData.PlayerScores[req.PlayerId]
            });
        }
    }
}