using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppRequestsLinq
{
    static class UserInterface
    {
        static DataSource dataSource = DataSource.Instance;
        static List<Post> postsEntity;
        static List<User> usersEntity;


        public static void RequestInfo()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\tMenu");
            Console.WriteLine("Choose your request");
            Console.Write("1-Get count of user comments \n");
            Console.Write("2-Get user comments where comment body < 50\n");
            Console.Write("3-Get list (id, name) from list todos which is complete for user\n");
            Console.Write("4-Get list of users alphabetically (ascending) with sorted todo items by length name (descending)\n");
            Console.Write(@"5-Get the following structure
                User
                Last post by user(by date)
                Number of comments under the last post
                Number of unloaded todo for the user
                The most popular user post(where there are most comments with a 
                text length of more than 80 characters)
                The most popular user post(where most of all are keynotes)" + "\n");
            Console.Write(@"6-Get the following structure
                Post
                The longest post comment
                The likest post comment
                The number of comments under the post where or 0 words or the 
                length of the text is <80" + "\n");

            Console.WriteLine("0-Leave");
        }

        public static void Action()
        {
            int act = 0;
            int userId, postId;

            while (true)
            {
                try
                {
                    act = int.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Uncorrect format entered data!");
                    Console.ForegroundColor = ConsoleColor.White;
                    RequestInfo();
                    continue;
                }
            }

            Console.Clear();

            switch (act)
            {
                 case 1:
                    {
                        if (postsEntity==null)
                        {
                            postsEntity = dataSource.GetPostsEntity();
                        }
                        userId = GetId();
                        foreach (var i in LinqRequests.CommentsCount(userId, postsEntity))
                        {
                            Console.WriteLine("Post:\n {0}\n Count of comments: {1}",i.post,i.count);
                        } 
                        break;
                    }
                case 2:
                    {
                        if (postsEntity == null)
                        {
                            postsEntity = dataSource.GetPostsEntity();
                        }

                        userId = GetId();

                        foreach (var c in LinqRequests.GetUserComments(userId, postsEntity))
                        {
                            Console.WriteLine(c);
                        }
                        break;
                    }
                case 3:
                    {

                        userId = GetId();
                        foreach (var i in LinqRequests.GetUserTodos(userId, dataSource.Todos))
                        {
                            Console.WriteLine("Todo Id: {0}; Name: {1}", i.Id, i.Name);
                        }
                        break;
                    }
                case 4:
                    {
                        if (usersEntity == null)
                        {
                            usersEntity = dataSource.GetUsersEntity();
                        }
                        foreach (var u in LinqRequests.GetSortedUsers(usersEntity))
                        {
                            Console.WriteLine(u.Name);

                            foreach (var t in u.Todos)
                            {
                                Console.WriteLine(t.Name);
                            }
                            Console.WriteLine();
                        }
                        break;

                    }
                case 5:
                    {
                        if (usersEntity == null)
                        {
                            usersEntity = dataSource.GetUsersEntity();
                        }
                        userId = GetId();

                        var i = LinqRequests.GetAdditionalUserInfo(userId, usersEntity);
                        Console.WriteLine("\n" + i.User + "\n");
                        Console.WriteLine("Last Post: {0}\n", i.LastPost + "\n");
                        Console.WriteLine("Count commnets: {0}\n", i.CountComments + "\n");
                        Console.WriteLine("Uncompleted tasks: {0}\n", i.UncompletedTasks + "\n");
                        Console.WriteLine("Most popular post by comments: {0}\n", i.MostPopularPostByComments == null ? "0" : i.MostPopularPostByComments.ToString());
                        Console.WriteLine("Most popular post by likes: {0}\n", i.MostPopularPostByLikes);

                        break;

                    }
                case 6:
                    {
                        if (postsEntity == null)
                        {
                            postsEntity = dataSource.GetPostsEntity();
                        }
                        postId = GetId();

                        var i = LinqRequests.GetAdditionalPostInfo(postId, postsEntity);
                        Console.WriteLine("Post: {0}\n", i.Post);
                        Console.WriteLine("Longest comment: {0}\n", i.LongestComment);
                        Console.WriteLine("Likest comment: {0}\n", i.LikestComment);
                        Console.WriteLine("Count comments where 0 likes or body < 80: {0}\n", i.CountComments);
                        break;
                    }
                case 0:
                    {
                        break;
                    }
            }
        }

        public static int GetId()
        {
            int id = 0;

            Console.WriteLine("Please input Id.");

            while (true)
            {
                try
                {
                    Console.WriteLine("Id :");
                    id = int.Parse(Console.ReadLine());
                    if (id < 0)
                    {
                        throw new FormatException();
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Uncorrect format entered data!");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
            }
            return id;
        }

    }
}
