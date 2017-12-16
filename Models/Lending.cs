using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LendGamesToMyFriends.Models
{
    public class Lending
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DevolutionDate { get; set; }
        public virtual Friend Friend { get; set; }
        public virtual Game Game { get; set; }
    }
}