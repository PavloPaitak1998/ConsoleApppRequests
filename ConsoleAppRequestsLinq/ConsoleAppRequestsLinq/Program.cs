using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleAppRequestsLinq
{
    class Program
    {       

        static void Main(string[] args)
        {
            Task<List<User>> t1 = HTTPRequests.GetUsers();
            t1.Wait();

            Task<List<Post>> t2 = HTTPRequests.GetPosts();
            t2.Wait();

            Task<List<Comment>> t3 = HTTPRequests.GetComments();
            t3.Wait();

            Task<List<Todo>> t4 = HTTPRequests.GetTodos();
            t4.Wait();

            var users = t1.Result;
            var posts = t2.Result;
            var comments = t3.Result;
            var todos = t4.Result;

            List<Post> postsEntity = GetPostsEntity(posts, comments);
            List<User> usersEntity = GetUsersEntity(users, postsEntity, todos);



        }

        static List<Post> GetPostsEntity(List<Post> _posts, List<Comment> _comments)
        {
            var postsEntity = (from p in _posts
                               join c in _comments on p.Id equals c.PostId into postComments
                               select new Post
                               {
                                   Id = p.Id,
                                   Body = p.Body,
                                   Title = p.Title,
                                   CreatedAt = p.CreatedAt,
                                   Likes = p.Likes,
                                   UserId = p.UserId,
                                   Comments = postComments.ToList()
                               }).ToList();

            return postsEntity;
        }

        static List<User> GetUsersEntity(List<User> _users,
            List<Post> _postsEntity, List<Todo> _todos)
        {
            var usersEntity = (from u in _users
                               join p in _postsEntity on u.Id equals p.UserId into userPosts
                               join t in _todos on u.Id equals t.UserId into userTodos
                               select new User
                               {
                                   Id = u.Id,
                                   CreatedAt = u.CreatedAt,
                                   Avatar = u.Avatar,
                                   Email = u.Email,
                                   Name = u.Name,
                                   Posts = userPosts.ToList(),
                                   Todos = userTodos.ToList()
                               }).ToList();

            return usersEntity;
        }

    }
}
