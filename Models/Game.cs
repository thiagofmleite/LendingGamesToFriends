using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LendGamesToMyFriends.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] CoverImage { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}