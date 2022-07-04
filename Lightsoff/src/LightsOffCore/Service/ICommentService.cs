using System;
using System.Collections.Generic;
using System.Text;
using LightsOff.Entity;

namespace LightsOff.Service
{
    public interface ICommentService
    {
        void AddComment(Comment comment);

        IList<Comment> GetAllComments();

        IList<Comment> GetNewComments();

        void ResetComments();

    }
}
