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
            Console.Write("Your action: ");
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

                        userId = GetId("user");

                        var listPostCount = LinqRequests.CommentsCount(userId, postsEntity);

                        if (listPostCount.Count()<1)
                        {
                            Console.WriteLine("This user Id:{0} doesnt have any posts.\n",userId);
                            break;
                        }

                        foreach (var i in listPostCount)
                        {
                            Console.WriteLine("Post:\n {0}\nCount of comments: {1}\n",i.post,i.count);
                        } 
                        break;
                    }
                case 2:
                    {
                        if (postsEntity == null)
                        {
                            postsEntity = dataSource.GetPostsEntity();
                        }

                        userId = GetId("user");

                        var listPost = LinqRequests.GetUserComments(userId, postsEntity);

                        if (listPost.Count() < 1)
                        {
                            Console.WriteLine("This user Id:{0} doesnt have any comments in which body < 50 under post.\n", userId);
                            break;
                        }

                        foreach (var c in listPost)
                        {
                            Console.WriteLine("Comment:\n{0}\n",c);
                        }
                        break;
                    }
                case 3:
                    {
                        userId = GetId("user");

                        var listIdName = LinqRequests.GetUserTodos(userId, dataSource.Todos);

                        if (listIdName.Count() < 1)
                        {
                            Console.WriteLine("This user Id:{0} doesnt have any completed task.\n", userId);
                            break;
                        }

                        foreach (var i in listIdName)
                        {
                            Console.WriteLine("Todo Id: {0}; Name: {1}\n", i.Id, i.Name);
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
                            Console.WriteLine("User Name: {0}",u.Name);

                            foreach (var t in u.Todos)
                            {
                                Console.WriteLine("Todo: {0}",t.Name);
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

                        userId = GetId("user");

                        var i = LinqRequests.GetAdditionalUserInfo(userId, usersEntity);
                        Console.WriteLine("User:\n{0}\n",i.User);
                        Console.WriteLine("Last Post: {0}\n", i.LastPost == null ? "Absent": i.LastPost.ToString());
                        Console.WriteLine("Count commnets: {0}\n", i.CountComments);
                        Console.WriteLine("Uncompleted tasks: {0}\n", i.UncompletedTasks);
                        Console.WriteLine("Most popular post by comments: {0}\n", i.MostPopularPostByComments == null ? "Absent" : i.MostPopularPostByComments.ToString());
                        Console.WriteLine("Most popular post by likes: {0}\n", i.MostPopularPostByLikes == null ? "Absent" : i.MostPopularPostByLikes.ToString());

                        break;
                    }
                case 6:
                    {
                        if (postsEntity == null)
                        {
                            postsEntity = dataSource.GetPostsEntity();
                        }

                        postId = GetId("post");

                        var i = LinqRequests.GetAdditionalPostInfo(postId, postsEntity);
                        Console.WriteLine("Post: {0}\n", i.Post);
                        Console.WriteLine("Longest comment: {0}\n", i.LongestComment == null ? "Absent" : i.LongestComment.ToString());
                        Console.WriteLine("Likest comment: {0}\n", i.LikestComment == null ? "Absent" : i.LikestComment.ToString());
                        Console.WriteLine("Count comments where 0 likes or body < 80: {0}\n", i.CountComments);
                        break;
                    }
                case 0:
                    {
                        break;
                    }
            }
        }

        static int GetId(string whoseId)
        {
            int id = 0;

            while (true)
            {
                try
                {
                    Console.WriteLine($"Please input {whoseId} Id.");
                    Console.Write("Id: ");
                    id = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    if (id < 0)
                    {
                        throw new FormatException();
                    }

                    if (!CheckId(id, whoseId))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no such user id. Please try again.");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
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

        static bool CheckId(int id,string whoseId)
        {
            if (whoseId == "user")
            {
                return dataSource.Users.Exists(u => u.Id == id);
            }
            else if (whoseId == "post")
            {
                return dataSource.Posts.Exists(p => p.Id == id);
            }
            else
                throw new Exception("Uncorrect type id. There is only user/post id.");
        }

    }
}
