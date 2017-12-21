using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LendGamesToMyFriends.Models
{
    public class Lending
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Friend")]
        public Guid FriendId { get; set; }
        public virtual Friend Friend { get; set; }
        [ForeignKey("Game")]
        public Guid GameId { get; set; }
        public virtual Game Game { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime LendDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool Status { get; set; }
    }
}