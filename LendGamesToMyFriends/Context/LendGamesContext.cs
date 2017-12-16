namespace LendGamesToMyFriends.Context
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using LendGamesToMyFriends.Models;

    public partial class LendGamesContext : DbContext
    {
        public DbSet<Game> Games{ get; set; }

        public LendGamesContext()
            : base("name=LendGamesContext")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
