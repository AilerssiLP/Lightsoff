using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using LightsOff.Core;
using LightsOff.Service;
using LightsOffWeb.Models;
using LightsOff.Entity;
using Lightsoff.src.LightsOffCore.Service;

namespace LightsOffWeb.Controllers
{
    public class LightsController : Controller
    {
        private const string FieldSessionKey = "field";

        private IScoreService scoreService = new ScoreServiceEF();
        private IRatingService ratingService = new RatingServiceEF();
        private ICommentService commentService = new CommentServiceEF();

        public IActionResult Index()
        {
            var field = new Field(4, 4);
            HttpContext.Session.SetObject(FieldSessionKey, field);

            CreateModel();

            return View("Index", CreateModel());
        }

        public IActionResult Move(int row,int column)
        {
            var field = (Field)HttpContext.Session.GetObject(FieldSessionKey);
            field.Play(row,column);
            HttpContext.Session.SetObject(FieldSessionKey, field);
            return View("Index", CreateModel());
        }

        private LightsModel CreateModel() {
            var scores = scoreService.GetTopScores();
            var comments = commentService.GetNewComments();
            var ratings= ratingService.GetNewestRatings();
            var field = (Field)HttpContext.Session.GetObject(FieldSessionKey);

            return new LightsModel { Field = field, Scores = scores, Comments = comments,Ratings = ratings};
        }

        public IActionResult SaveScore(Score score)
        {
            //_scoreService.AddScore(new Score() { PlayedAt = DateTime.Now, Player = Player, Points = Points});
            score.PlayedAt = DateTime.Now;
            scoreService.AddScore(score);

            return View("Index", CreateModel());
        }

        public IActionResult SaveRating(Rating rating)
        {
            //_scoreService.AddScore(new Score() { PlayedAt = DateTime.Now, Player = Player, Points = Points});
            rating.PlayedAt = DateTime.Now;
            ratingService.AddRating(rating);

            return View("Index", CreateModel());
        }

        public IActionResult SaveComment(Comment comment)
        {
            //_scoreService.AddScore(new Score() { PlayedAt = DateTime.Now, Player = Player, Points = Points});
            comment.PlayedAt = DateTime.Now;
            commentService.AddComment(comment);

            return View("Index", CreateModel());
        }

    }
}
