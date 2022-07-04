using System.Collections.Generic;
using LightsOff.Core;
using LightsOff.Entity;

namespace LightsOffWeb.Models
{
    public class LightsModel
    {
        public Field Field { get; set;}

        public IList<Score> Scores{get;set;}

        public IList<Rating> Ratings { get; set; }

        public IList<Comment> Comments{ get; set; }
    }
}
