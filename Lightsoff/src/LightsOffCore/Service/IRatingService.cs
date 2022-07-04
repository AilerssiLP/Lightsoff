using System;
using System.Collections.Generic;
using System.Text;
using LightsOff.Entity;

namespace LightsOff.Service
{
    public interface IRatingService

    {
        void AddRating(Rating rating);

        IList<Rating> GetNewestRatings();

        IList<Rating> GetAllRatings();

        float GetAveRating();

        void ResetRatings();

    }
}
