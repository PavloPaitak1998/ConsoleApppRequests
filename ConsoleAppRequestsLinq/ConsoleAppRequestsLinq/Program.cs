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

        }
    }
}
