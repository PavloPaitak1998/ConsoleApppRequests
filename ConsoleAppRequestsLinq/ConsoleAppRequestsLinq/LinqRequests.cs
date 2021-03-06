﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppRequestsLinq
{
    static class LinqRequests
    {
        //1
        public static IEnumerable<(Post post, int count)> CommentsCount(int id, 
            IEnumerable<Post> _postsEntity)
        {
            return _postsEntity.Where(p => p.UserId == id)
                .Select(p => (Post: p, Count: p.Comments.Count()));
        }

        //2
        public static IEnumerable<Comment> GetUserComments(int id, IEnumerable<Post> _postsEntity)
        {
            return _postsEntity.Where(p => p.UserId == id)
                .SelectMany(p=>p.Comments.Where(c=>c.Body.Length<50));
        }

        //3
        public static IEnumerable<(int Id, string Name)> GetUserTodos(int id,
            IEnumerable<Todo> _todos)
        {
            return _todos.Where(t => t.UserId == id && t.IsComplete == true)
                .Select(t => (Id: t.Id, Name: t.Name));
        }

        //4
        public static IEnumerable<User> GetSortedUsers(IEnumerable<User> _usersEntity)
        {
            return _usersEntity.OrderBy(u => u.Name).Select(
                (u) => new User
                {
                    Id = u.Id,
                    Name = u.Name,
                    Avatar = u.Avatar,
                    CreatedAt = u.CreatedAt,
                    Email = u.Email,
                    Posts = u.Posts,
                    Todos = u.Todos.OrderByDescending(todo => todo.Name.Length).ToList()
                });
        }

        //5
        public static (User User, Post LastPost, int CountComments, 
            int UncompletedTasks, Post MostPopularPostByComments, 
            Post MostPopularPostByLikes) GetAdditionalUserInfo(int id,
            IEnumerable<User> _usersEntity)
        {
            var res = from u in _usersEntity
                      where u.Id == id

                      let lastPost = u.Posts.
                      OrderByDescending(p => DateTime.Parse(p.CreatedAt))
                      .FirstOrDefault()

                      let countComments = lastPost == null ? 0 : lastPost.Comments.Count()

                      let uncompletedTasks = u.Todos.Where(t => t.IsComplete == false).Count()

                      let mostPopularPostByComments = u.Posts.
                      OrderByDescending(p => p.Comments.Where(c => c.Body.Length > 80)
                      .Count()).FirstOrDefault()

                      let mostPopularPostByLikes = u.Posts.
                      OrderByDescending(p => p.Likes).FirstOrDefault()

                      select (
                      User: u,
                      LastPost: lastPost,
                      CountComments: countComments,
                      UncompletedTasks: uncompletedTasks,
                      MostPopularPostByComments: mostPopularPostByComments,
                      MostPopularPostByLikes: mostPopularPostByLikes
                      );

            return res.FirstOrDefault();
        }

        //6
        public static (Post Post, Comment LongestComment, Comment LikestComment, int CountComments)
            GetAdditionalPostInfo(int id, IEnumerable<Post> _postsEntity)
        {
            var res = from p in _postsEntity
                      where p.Id == id

                      let longestComment = p.Comments
                      .OrderByDescending(c => c.Body.Length)
                      .FirstOrDefault()

                      let likestComment = p.Comments
                      .OrderByDescending(c => c.Likes)
                      .FirstOrDefault()

                      let countComments = p.Comments
                      .Where(c => c.Likes == 0 || c.Body.Length < 80)
                      .Count()

                      select (
                      Post: p,
                      LongestComment: longestComment,
                      LikestComment: likestComment,
                      CountComments: countComments
                      );
            return res.FirstOrDefault();
        }

        public static List<Post> GetPostsEntity(List<Post> _posts, List<Comment> _comments)
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

        public static List<User> GetUsersEntity(List<User> _users,
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
