using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAppCSharp.Entities
{
    public class Bank
    {
        public int Id { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
