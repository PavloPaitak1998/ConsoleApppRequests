using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppRequestsLinq
{
    sealed class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; Name:{Name}; Avatar:{Avatar}; Email:{Email}; CreatedAt:{CreatedAt}";
        }
    }
}
