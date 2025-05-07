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
            return Ok(new { message = $"환영합니다 {req.PlayerName}님" });
        }
    }
}
