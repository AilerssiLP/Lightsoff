using System;
using System.Collections.Generic;
using System.Text;

namespace LightsOff.Entity
{
    [Serializable]
    public class Score
    {
        public int Id { get; set; }
        public string Player { get; set; }

        public int Points { get; set; }

        public DateTime PlayedAt { get; set; }
    }
}
