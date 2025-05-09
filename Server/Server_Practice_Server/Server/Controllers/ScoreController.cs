using Microsoft.AspNetCore.Mvc;
using Server.DTO;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        // 점수 업데이트
        [HttpPost]
        public IActionResult UpdateScore([FromBody] PlayerRequest req)
        {
            if (!PlayerData.RegisteredPlayers.TryGetValue(req.PlayerId, out string? value))
                return BadRequest(new { message = $"등록되지 않은 ID: {req.PlayerId}" });
            
            PlayerData.PlayerScores.AddOrUpdate(req.PlayerId, req.PlayerScore, (key, oldValue) => oldValue + req.PlayerScore);

            var response = new ScoreResponseDto
            {
                Message = $"플레이어 {value}의 현재 점수: {PlayerData.PlayerScores[req.PlayerId]}",
                PlayerId = req.PlayerId,
                PlayerScore = PlayerData.PlayerScores[req.PlayerId]
            };
            
            return Ok(response);
        }
    }
}