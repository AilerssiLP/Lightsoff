using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightsOff.Entity;
using LightsOff.Service;
using Lightsoff.src.LightsOffCore.Service;

namespace LightsOffWeb.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private IRatingService _ratingService = new RatingServiceEF();

        //GET: /api/Score
        [HttpGet]
        public IEnumerable<Rating> GetRatings()
        {
            return _ratingService.GetNewestRatings();
        }

        //POST: /api/Score
        [HttpPost]
        public void PostScore(Rating rating)
        {
            rating.PlayedAt = DateTime.Now;
            _ratingService.AddRating(rating);
        }
    }
}
