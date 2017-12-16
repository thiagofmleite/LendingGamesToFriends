namespace LendGamesToMyFriends.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual UserReference User { get; set; }
        public bool Status { get; set; }
    }
}