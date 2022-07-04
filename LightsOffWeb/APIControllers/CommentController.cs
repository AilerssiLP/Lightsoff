using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightsOff.Entity;
using LightsOff.Service;
using Lightsoff.src.LightsOffCore.Service;

namespace LightsOffWeb.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService _commentService = new CommentServiceEF();

        //GET: /api/Score
        [HttpGet]
        public IEnumerable<Comment> GetComments()
        {
            return _commentService.GetAllComments();
        }

        //POST: /api/Score
        [HttpPost]
        public void PostScore(Comment comment)
        {
            comment.PlayedAt = DateTime.Now;
            _commentService.AddComment(comment);
        }
    }
}
