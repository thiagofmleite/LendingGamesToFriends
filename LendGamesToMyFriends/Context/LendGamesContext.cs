namespace LendGamesToMyFriends.Context
{
    using System.Data.Entity;
    using LendGamesToMyFriends.Models;

    public partial class LendGamesContext : DbContext
    {
        public DbSet<Game> Games{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Lending> Lendings { get; set; }

        public LendGamesContext()
            : base("name=LendGamesContext")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
