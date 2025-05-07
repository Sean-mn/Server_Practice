using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        [HttpPost]
        public IActionResult PrintScore([FromBody] PlayerRequest req)
        {
            return Ok(new { message = $"현재 점수: {req.PlayerScore}" });
        }
    }
}
