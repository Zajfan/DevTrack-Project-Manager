// Comment.cs
using System;

namespace DevTrack.DAL.Repositories
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int TaskID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}