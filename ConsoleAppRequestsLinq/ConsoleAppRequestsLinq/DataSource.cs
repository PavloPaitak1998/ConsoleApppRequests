using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppRequestsLinq
{
    class DataSource
    {
        private static readonly Lazy<DataSource> lazy = new Lazy<DataSource>(() => new DataSource());
        public static DataSource Instance { get { return lazy.Value; } }

        //Initialization of default values
        private DataSource()
        {
        }

        public List<User> Users { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Todo> Todos { get; set; }

        public List<Post> GetPostsEntity()
        {
            return LinqRequests.GetPostsEntity(Posts, Comments);
        }
          
        public List<User> GetUsersEntity()
        {
            return  LinqRequests.GetUsersEntity(Users, GetPostsEntity(), Todos);
        }


    }
}
