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

    }
}
