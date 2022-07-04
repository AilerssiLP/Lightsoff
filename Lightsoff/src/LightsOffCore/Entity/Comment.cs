using System;

namespace LightsOff.Entity
{
    [Serializable]
    public class Comment
    {
        public int Id { get; set; }
        public string Player { get; set; }

        public string PlayerComment { get; set; }

        public DateTime PlayedAt { get; set; }

    }
}
