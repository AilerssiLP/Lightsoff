using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LightsOff.Entity;
using LightsOff.Service;
using Lightsoff.src.LightsOffCore.Service;

namespace RatingServiceTest
{

    [TestClass]
    public class RatingServiceTest
    {
        [TestMethod]
        public void AddTest1()
        {
            var service = CreateService();

            service.AddRating(new Rating { Player = "Janko123", PlayerRating = 3, PlayedAt = DateTime.Now });

            Assert.AreEqual(1, service.GetAllRatings().Count);

            Assert.AreEqual("Janko123", service.GetAllRatings()[0].Player);
            Assert.AreEqual(3, service.GetAllRatings()[0].PlayerRating);
        }


        [TestMethod]
        public void AddTest2()
        {
            var service = CreateService();

            DateTime s = DateTime.Now;
            service.AddRating(new Rating { Player = "Janko123", PlayerRating = 3, PlayedAt = new DateTime(s.Year, s.Month, s.Day, 6, 36, 44) });
            service.AddRating(new Rating { Player = "Zuzulienocka", PlayerRating = 1, PlayedAt = new DateTime(s.Year, s.Month, s.Day, 7, 36, 44) });
            service.AddRating(new Rating { Player = "terminator123", PlayerRating = 2, PlayedAt = new DateTime(s.Year, s.Month, s.Day, 8, 36, 44) });

            // Trace.WriteLine(service.GetNewestComments()[0].Player);
            Assert.AreEqual(3, service.GetNewestRatings().Count);

            Assert.AreEqual("Janko123", service.GetNewestRatings()[2].Player);
            Assert.AreEqual(3, service.GetNewestRatings()[2].PlayerRating);

            Assert.AreEqual("Zuzulienocka", service.GetNewestRatings()[1].Player);
            Assert.AreEqual(1, service.GetNewestRatings()[1].PlayerRating);

            Assert.AreEqual("terminator123", service.GetNewestRatings()[0].Player);
            Assert.AreEqual(2, service.GetNewestRatings()[0].PlayerRating);
        }

        [TestMethod]
        public void AddTest3()
        {
            var service = CreateService();

            DateTime s = DateTime.Now;
            service.AddRating(new Rating { Player = "Janko123", PlayerRating = 1, PlayedAt = new DateTime(s.Year, s.Month, s.Day, 6, 36, 44) });
            service.AddRating(new Rating { Player = "Zuzulienocka", PlayerRating = 2, PlayedAt = new DateTime(s.Year, s.Month, s.Day, 7, 36, 44) });
            service.AddRating(new Rating { Player = "terminator123", PlayerRating = 3, PlayedAt = new DateTime(s.Year, s.Month, s.Day, 8, 36, 44) });
            service.AddRating(new Rating { Player = "Shira", PlayerRating = 4, PlayedAt = new DateTime(s.Year, s.Month, s.Day, 5, 36, 44) });
            service.AddRating(new Rating { Player = "Larva", PlayerRating = 5, PlayedAt = new DateTime(s.Year, s.Month, s.Day, 4, 36, 44) });

            // Trace.WriteLine(service.GetNewestComments()[0].Player);
            Assert.AreEqual(3, service.GetNewestRatings().Count);

            Assert.AreEqual("Janko123", service.GetNewestRatings()[2].Player);
            Assert.AreEqual(1, service.GetNewestRatings()[2].PlayerRating);

            Assert.AreEqual("Zuzulienocka", service.GetNewestRatings()[1].Player);
            Assert.AreEqual(2, service.GetNewestRatings()[1].PlayerRating);

            Assert.AreEqual("terminator123", service.GetNewestRatings()[0].Player);
            Assert.AreEqual(3, service.GetNewestRatings()[0].PlayerRating);
        }

        [TestMethod]
        public void AveRatingTest()
        {
            var service = CreateService();

            DateTime s = DateTime.Now;
            service.AddRating(new Rating { Player = "Jaroslav", PlayerRating = 1, PlayedAt = new DateTime(s.Year, s.Month, s.Day, 6, 36, 44) });

            service.AddRating(new Rating { Player = "Petrolio", PlayerRating = 2, PlayedAt = new DateTime(s.Year, s.Month, s.Day, 7, 36, 44) });

            Assert.AreEqual(1.5, service.GetAveRating());
        }

        [TestMethod]
        public void ResetTest()
        {
            var service = CreateService();

            DateTime s = DateTime.Now;
            service.AddRating(new Rating { Player = "Jaroslav", PlayerRating = 1, PlayedAt = new DateTime(s.Year, s.Month, s.Day, 6, 36, 44) });

            service.AddRating(new Rating { Player = "Petrolio", PlayerRating = 1, PlayedAt = new DateTime(s.Year, s.Month, s.Day, 7, 36, 44) });

            service.ResetRatings();
            Assert.AreEqual(0, service.GetAllRatings().Count);
        }

        private IRatingService CreateService()
        {
            var service = new RatingServiceEF();
            service.ResetRatings();
            return service;
        }
    }
}