using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleAppRequestsLinq
{
    static class HTTPRequests
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<List<User>> GetUsers()
        {
            string result;

            string page = "https://5b128555d50a5c0014ef1204.mockapi.io/users";

            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                result = await content.ReadAsStringAsync();
            }

            var users = JsonConvert.DeserializeObject<List<User>>(result);

            return users;
        }

        public static async Task<List<Post>> GetPosts()
        {
            string result;

            string page = "https://5b128555d50a5c0014ef1204.mockapi.io/posts";

            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                result = await content.ReadAsStringAsync();
            }

            var posts = JsonConvert.DeserializeObject<List<Post>>(result);

            return posts;
        }

        public static async Task<List<Comment>> GetComments()
        {
            string result;

            string page = "https://5b128555d50a5c0014ef1204.mockapi.io/comments";

            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                result = await content.ReadAsStringAsync();
            }

            var comments = JsonConvert.DeserializeObject<List<Comment>>(result);

            return comments;
        }

        public static async Task<List<Todo>> GetTodos()
        {
            string result;

            string page = "https://5b128555d50a5c0014ef1204.mockapi.io/todos";

            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                result = await content.ReadAsStringAsync();
            }

            var todos = JsonConvert.DeserializeObject<List<Todo>>(result);

            return todos;
        }

    }
}
