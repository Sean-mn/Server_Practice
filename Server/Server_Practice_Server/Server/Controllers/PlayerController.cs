using Microsoft.AspNetCore.Mvc;
using Server.DTO;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController(ILogger<PlayerController> logger) : ControllerBase
    {
        [HttpPost("{register}")]
        public IActionResult RegisterPlayer([FromBody] PlayerRequest req)
        {
            if (req.PlayerId <= 0 || string.IsNullOrEmpty(req.PlayerName))
            {
                logger.LogWarning("잘못된 요청: 유효하지 않은 PlayerId({PlayerId}) 또는 PlayerName({PlayerName})", req.PlayerId, req.PlayerName);
                return BadRequest(new { message = "유효한 플레이어의 Id와 이름이 아닙니다." });
            }

            var added = PlayerData.RegisteredPlayers.TryAdd(req.PlayerId, req.PlayerName);
            if (!added)
            {
                logger.LogWarning("중복 PlayerId 감지: {PlayerId}, 이름: {PlayerName}", req.PlayerId, req.PlayerName);
                return Conflict(new { message = $"PlayerId: {req.PlayerId}는 이미 등록되어 있습니다." });
            }

            PlayerData.PlayerScores[req.PlayerId] = 0;

            logger.LogInformation("신규 플레이어 등록: {PlayerId}, 이름: {PlayerName}", req.PlayerId, req.PlayerName);

            var response = new RegisterResponseDto
            {
                Message = $"환영합니다. {req.PlayerName}님! ID: {req.PlayerId}",
                PlayerId = req.PlayerId
            };
            return Ok(response);
        }

        [HttpDelete("{playerId}")]
        public IActionResult DeletePlayer(int playerId)
        {
            if (!PlayerData.RegisteredPlayers.ContainsKey(playerId))
            {
                logger.LogWarning("삭제 요청: 등록되지 않은 PlayerId({PlayerId})", playerId);
                return NotFound(new { message = $"등록되지 않은 PlayerId: {playerId}" });
            }
            
            PlayerData.RegisteredPlayers.TryRemove(playerId, out _);
            PlayerData.PlayerScores.TryRemove(playerId, out _);

            logger.LogInformation("플레이어 삭제: {PlayerId}", playerId);

            return Ok(new { message = $"PlayerId: {playerId}가 삭제되었습니다." });
        }
    }
}
