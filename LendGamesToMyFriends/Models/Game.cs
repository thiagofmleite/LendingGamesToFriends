using System;

namespace LendGamesToMyFriends.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public virtual User User { get; set; }
        public string Cover { get; set; }
        public bool Status { get; set; }
    }
}