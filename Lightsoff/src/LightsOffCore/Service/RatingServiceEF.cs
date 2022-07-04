using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LightsOff.Entity;
using LightsOff.Service;

namespace Lightsoff.src.LightsOffCore.Service
{
    public class RatingServiceEF : IRatingService
    {
        public void AddRating(Rating rating)
        {
            using (var context = new LightsOffDbContext())
            {
                context.Rating.Add(rating);
                context.SaveChanges();
            }
        }

        public void ResetRatings()
        {
            using (var context = new LightsOffDbContext())
            {
                context.Database.ExecuteSqlRaw("DELETE FROM Rating");
            }
        }

        public IList<Rating> GetAllRatings()
        {
            using (var context = new LightsOffDbContext())
            {
                return (from s in context.Rating orderby s.PlayedAt descending select s).ToList();
            }
        }

        float IRatingService.GetAveRating()
        {
            //GetAllRatings();
            var _ratings = GetAllRatings();

            //IList<Rating> _ratings;
           // using (var context = new LightsOffDbContext())
            //{
            //    _ratings = (from s in context.Rating orderby s.PlayedAt descending select s).ToList();
            //}
            float averageRating = 0;
            for (int i = 0; i < _ratings.Count; i++)
            {
                averageRating += _ratings[i].PlayerRating;
            }
            return averageRating / _ratings.Count;
        }

        IList<Rating> IRatingService.GetNewestRatings()
        {
            using (var context = new LightsOffDbContext())
            {
                return (from s in context.Rating orderby s.PlayedAt descending select s).Take(3).ToList();
            }
        }
    }
}
