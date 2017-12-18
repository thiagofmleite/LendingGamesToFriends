using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LendGamesToMyFriends.Models
{
    public class Lending
    {
        public Guid Id { get; set; }
        public virtual Friend Friend { get; set; }
        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
        public DateTime LendDate { get; set; }
    }
}