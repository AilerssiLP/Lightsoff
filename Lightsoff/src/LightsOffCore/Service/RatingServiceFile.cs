using System;
using System.Collections.Generic;
using System.Text;
using LightsOff.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;


namespace LightsOff.Service
{
    public class RatingServiceFile : IRatingService
    {
        private const string FileName = "ratings.bin";

        private IList<Rating> _ratings = new List<Rating>();

        void IRatingService.AddRating(Rating rating)
        {
            _ratings.Add(rating);
            SaveRatings();
        }

        IList<Rating> IRatingService.GetAllRatings()
        {
            LoadRatings();
            return _ratings;
        }

        IList<Rating> IRatingService.GetNewestRatings()
        {
            LoadRatings();
            return _ratings.OrderByDescending(s => s.PlayedAt).Take(3).ToList();
        }

        public float GetAveRating()
        {
            //GetAllRatings();
            float averageRating = 0;
            for (int i = 0; i < _ratings.Count; i++)
             {
                 averageRating += _ratings[i].PlayerRating;
             }
            return averageRating / _ratings.Count;
        }

        public void ResetRatings()
        {
            _ratings.Clear();
            File.Delete(FileName);
        }

        private void SaveRatings()
        {
            using (var fs = File.OpenWrite(FileName))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, _ratings);
            }
        }
        private void LoadRatings()
        {
            if (File.Exists(FileName))
            {
                using (var fs = File.OpenRead(FileName))
                {
                    var bf = new BinaryFormatter();
                    _ratings = (List<Rating>)bf.Deserialize(fs);
                }
            }
        }
    }
}
