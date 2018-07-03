using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppRequestsLinq
{
   sealed class Comments
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int Likes { get; set; }
        public string CreatedAt { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; PostId:{PostId}; Body:{Body}; UserId:{UserId}; Likes: {Likes}; CreatedAt:{CreatedAt}";
        }
    }
}
