using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        private readonly ILogger<HelloController> _logger;
        
        public HelloController(ILogger<HelloController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost]
        public IActionResult Welcome([FromBody] PlayerRequest req)
        {
            if (req.PlayerId <= 0 || string.IsNullOrEmpty(req.PlayerName))
            {
                _logger.LogWarning("잘못된 요청: 유효하지 않은 PlayerId({PlayerId}) 또는 PlayerName({PlayerName})", req.PlayerId, req.PlayerName);
                return BadRequest(new { message = "유효한 플레이어의 Id와 이름이 아닙니다." });
            }

            if (PlayerData.RegisteredPlayers.ContainsKey(req.PlayerId))
            {
                _logger.LogWarning("중복 PlayerId 감지: {PlayerId}, 이름: {PlayerName}", req.PlayerId, req.PlayerName);
                return Conflict(new { message = $"PlayerId: {req.PlayerId}는 이미 등록되어 있습니다." });
            }

            var added = PlayerData.RegisteredPlayers.TryAdd(req.PlayerId, req.PlayerName);
            if (!added)
            {
                _logger.LogError("PlayerId 추가 실패: {PlayerId}", req.PlayerId);
                return Conflict(new { message = $"PlayerId: {req.PlayerId}는 이미 등록되어있습니다." });
            }

            PlayerData.PlayerScores[req.PlayerId] = 0;

            _logger.LogInformation("신규 플레이어 등록: {PlayerId}, 이름: {PlayerName}", req.PlayerId, req.PlayerName);
            return Ok(new { message = $"환영합니다. {req.PlayerName}님! ID: {req.PlayerId}" });
        }
    }
}
