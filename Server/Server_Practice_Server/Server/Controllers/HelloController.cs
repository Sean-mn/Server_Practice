using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        [HttpPost]
        public IActionResult Welcome([FromBody]PlayerRequest req)
        {
            if (req.PlayerId <= 0 || string.IsNullOrEmpty(req.PlayerName))
                return BadRequest(new { message = "유효한 플레이어의 Id와 이름이 아닙니다." });
            
            var  added = PlayerData.RegisteredPlayers.TryAdd(req.PlayerId, req.PlayerName);
            if (!added)
                return Conflict(new { message = $"PlayerId: {req.PlayerId}는 이미 등록되어있습니다." });

            PlayerData.PlayerScores[req.PlayerId] = 0;
            
            return Ok(new {message = $"환영합니다. {req.PlayerName}님! ID: {req.PlayerId}" });
        }
    }
}
