using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppRequestsLinq
{
    static class LinqRequests
    {
        //1
        static IEnumerable<(Post post, int count)> CommentsCount(int id, IEnumerable<Post> _postsEntity)
        {
            return _postsEntity.Where(p => p.UserId == id).Select(p => (Post: p, Count: p.Comments.Count()));
        }

        //2
        static IEnumerable<Comment> GetUserComments(int id, IEnumerable<Comment> _comments)
        {
            return _comments.Where(c => c.UserId == id && c.Body.Length < 50);
        }


    }
}
