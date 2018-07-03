using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppRequestsLinq
{
    sealed class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int Likes { get; set; }
        public string CreatedAt { get; set; }

        public List<Comment> Comments { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; Title: {Title}; Body: {Body}; UserId: {UserId}; Likes: {Likes}; CreatedAt: {CreatedAt}";
        }
    }
}
