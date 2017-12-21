using System;

namespace LendGamesToMyFriends.Models
{
    public class UserReference
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}