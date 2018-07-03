using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppRequestsLinq
{
    sealed class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public int UserId { get; set; }
        public string CreatedAt { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; Name:{Name}; UserId:{UserId}; IsComplete:{IsComplete}; CreatedAt:{CreatedAt} ";
        }
    }
}
