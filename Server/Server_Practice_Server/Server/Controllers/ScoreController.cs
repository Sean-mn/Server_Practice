using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private static int _totalScore;
        
        // 점수 업데이트
        [HttpPost]
        public IActionResult PrintScore([FromBody] PlayerRequest req)
        {
            _totalScore = req.PlayerScore;
            return Ok(new { message = $"현재 점수: {_totalScore}" });
        }

        // 누적 점수 가져오기
        [HttpGet]
        public IActionResult GetTotalScore()
        {
            return Ok(new { message = $"누적 점수: {_totalScore}", totalScore = _totalScore });
        }
    }
}