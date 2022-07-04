using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightsOff.Entity;
using LightsOff.Service;

namespace LightsOffWeb.APIControllers
{
    //https://localhost:44319/api/Score
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private IScoreService _scoreService = new ScoreServiceEF();

        //GET: /api/Score
        [HttpGet]
        public IEnumerable<Score> GetScores()
        {
            return _scoreService.GetTopScores();
        }

        //POST: /api/Score
        [HttpPost]
        public void PostScore(Score score)
        {
            score.PlayedAt = DateTime.Now;
            _scoreService.AddScore(score);
        }
    }
}
