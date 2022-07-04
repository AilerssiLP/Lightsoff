using System;
using System.Collections.Generic;
using System.Text;

namespace LightsOff.Entity
{
    [Serializable]
    public class Rating
    {
        public int Id { get; set; }
        public string Player { get; set; }

        public int PlayerRating { get; set; }

        public DateTime PlayedAt { get; set; }

    }
}
