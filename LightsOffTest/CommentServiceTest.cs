using System;
using System.Diagnostics;
using Lightsoff.src.LightsOffCore.Service;
using LightsOff.Entity;
using LightsOff.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommentServiceTest { 

    [TestClass]
    public class CommentServiceTest
    {
        [TestMethod]
        public void AddTest1()
        {
            var service = CreateService();

            service.AddComment(new Comment { Player = "Janko123", PlayerComment = "Lots of bugs reee", PlayedAt = DateTime.Now });

            Assert.AreEqual(1, service.GetAllComments().Count);

            Assert.AreEqual("Janko123", service.GetNewComments()[0].Player);
            Assert.AreEqual("Lots of bugs reee", service.GetNewComments()[0].PlayerComment);
        }

        [TestMethod]
        public void AddTest3()
        {
            var service = CreateService();

            DateTime s = DateTime.Now;
            service.AddComment(new Comment { Player = "Janko123", PlayerComment = "Lots of bugs reee", PlayedAt = new DateTime(s.Year, s.Month, s.Day, 6, 36, 44) });
            service.AddComment(new Comment { Player = "Zuzulienocka", PlayerComment = "EZ EZ", PlayedAt = new DateTime(s.Year, s.Month, s.Day, 7, 36, 44) });
            service.AddComment(new Comment { Player = "terminator123", PlayerComment = "Good game", PlayedAt = new DateTime(s.Year, s.Month, s.Day, 8, 36, 44) });

            // Trace.WriteLine(service.GetNewestComments()[0].Player);
            Assert.AreEqual(3, service.GetNewComments().Count);

            Assert.AreEqual("Janko123", service.GetNewComments()[2].Player);
            Assert.AreEqual("Lots of bugs reee", service.GetNewComments()[2].PlayerComment);

            Assert.AreEqual("Zuzulienocka", service.GetNewComments()[1].Player);
            Assert.AreEqual("EZ EZ", service.GetNewComments()[1].PlayerComment);

            Assert.AreEqual("terminator123", service.GetNewComments()[0].Player);
            Assert.AreEqual("Good game", service.GetNewComments()[0].PlayerComment);
        }

        [TestMethod]
        public void AddTest5()
        {
            var service = CreateService();

            DateTime s = DateTime.Now;
            service.AddComment(new Comment { Player = "Janko123", PlayerComment = "Lots of bugs reee", PlayedAt = new DateTime(s.Year, s.Month, s.Day, 6, 36, 44) });
            service.AddComment(new Comment { Player = "Zuzulienocka", PlayerComment = "EZ EZ", PlayedAt = new DateTime(s.Year, s.Month, s.Day, 7, 36, 44) });
            service.AddComment(new Comment { Player = "terminator123", PlayerComment = "Good game", PlayedAt = new DateTime(s.Year, s.Month, s.Day, 8, 36, 44) });
            service.AddComment(new Comment { Player = "Rex", PlayerComment = "Rawrrrrr", PlayedAt = new DateTime(s.Year, s.Month, s.Day, 5, 36, 44) });
            service.AddComment(new Comment { Player = "Cajik", PlayerComment = "Yorkshire tea", PlayedAt = new DateTime(s.Year, s.Month, s.Day, 4, 36, 44) });

            // Trace.WriteLine(service.GetNewestComments()[0].Player);
            Assert.AreEqual(3, service.GetNewComments().Count);

            Assert.AreEqual("Janko123", service.GetNewComments()[2].Player);
            Assert.AreEqual("Lots of bugs reee", service.GetNewComments()[2].PlayerComment);

            Assert.AreEqual("Zuzulienocka", service.GetNewComments()[1].Player);
            Assert.AreEqual("EZ EZ", service.GetNewComments()[1].PlayerComment);

            Assert.AreEqual("terminator123", service.GetNewComments()[0].Player);
            Assert.AreEqual("Good game", service.GetNewComments()[0].PlayerComment);
        }

        [TestMethod]
        public void ResetTest()
        {
            var service = CreateService();

            DateTime s = DateTime.Now;
            service.AddComment(new Comment { Player = "Janko123", PlayerComment = "Lots of bugs reee", PlayedAt = new DateTime(s.Year, s.Month, s.Day, 6, 36, 44) });
            service.AddComment(new Comment { Player = "Zuzulienocka", PlayerComment = "EZ EZ", PlayedAt = new DateTime(s.Year, s.Month, s.Day, 7, 36, 44) });

            service.ResetComments();
            Assert.AreEqual(0, service.GetAllComments().Count);
        }

        private static ICommentService CreateService()
        {

            var service = new CommentServiceEF();
            service.ResetComments();
            return service;
        }
    }
}