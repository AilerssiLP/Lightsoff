using System;
using System.Collections.Generic;
using System.Text;
using LightsOff.Entity;

namespace LightsOff.Service
{
    public interface IScoreService
    {
        void AddScore(Score score);

        IList<Score> GetTopScores();

        void ResetScore();
    }
}
