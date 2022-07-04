using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LightsOff.Entity;
using LightsOff.Service;

namespace Lightsoff.src.LightsOffCore.Service
{
    public class CommentServiceEF : ICommentService
    {
        public void AddComment(Comment comment)
        {
            using (var context = new LightsOffDbContext())
            {
                context.Comment.Add(comment);
                context.SaveChanges();
            }
        }

        public IList<Comment> GetNewComments()
        {
            using (var context = new LightsOffDbContext())
            {
                return (from s in context.Comment orderby s.PlayedAt descending select s).Take(3).ToList();
            }
        }

        public void ResetComments()
        {
            using (var context = new LightsOffDbContext())
            {
                context.Database.ExecuteSqlRaw("DELETE FROM Comment");
            }
        }

        IList<Comment> ICommentService.GetAllComments()
        {
            using (var context = new LightsOffDbContext())
            {
                return (from s in context.Comment orderby s.PlayedAt descending select s).ToList();
            }
        }
    }
}
