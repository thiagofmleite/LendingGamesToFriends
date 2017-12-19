using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LendGamesToMyFriends.Models
{
    public class Friend
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public virtual User User { get; set; }
    }
}