using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LightsOff.Entity;

namespace LightsOff.Service
{
    public class ScoreServiceEF : IScoreService
    {
        public void AddScore(Score score)
        {
            using (var context = new LightsOffDbContext())
            {
                context.Scores.Add(score);
                context.SaveChanges();
            }
        }

        public IList<Score> GetTopScores()
        {
            using (var context = new LightsOffDbContext())
            {
                return (from s in context.Scores orderby s.Points descending select s).Take(3).ToList();
            }
        }

        public void ResetScore()
        {
            using (var context = new LightsOffDbContext())
            {
                context.Database.ExecuteSqlRaw("DELETE FROM Scores");
            }
        }
    }
}