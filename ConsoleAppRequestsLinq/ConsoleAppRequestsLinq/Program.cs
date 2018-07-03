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
            DataSource dataSource = DataSource.Instance;
            Task<List<User>> t1 = HTTPRequests.GetUsers();
            t1.Wait();

            Task<List<Post>> t2 = HTTPRequests.GetPosts();
            t2.Wait();

            Task<List<Comment>> t3 = HTTPRequests.GetComments();
            t3.Wait();

            Task<List<Todo>> t4 = HTTPRequests.GetTodos();
            t4.Wait();

            dataSource.Users = t1.Result;
            dataSource.Posts = t2.Result;
            dataSource.Comments = t3.Result;
            dataSource.Todos = t4.Result;

            while (true)
            {
                Console.WriteLine("Welcome !");
                UserInterface.RequestInfo();
                UserInterface.Action();
                if (Leave())
                {
                    break;
                }
                Console.Clear();
            }
        }

        static bool Leave()
        {
            bool leave = false;
            while (true)
            {
                Console.WriteLine("Do you want to go back to Menu press (1) or leave (2)....");

                switch (Console.ReadLine())
                {
                    case "2":
                        leave = true;
                        break;

                    case "1":
                        break;

                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Uncorrect answer try again");
                        Console.ForegroundColor = ConsoleColor.White;

                        continue;
                }
                Console.Clear();
                break;
            }

            return leave;
        }
    }
}
