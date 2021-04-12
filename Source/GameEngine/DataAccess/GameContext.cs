using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GameEngine.Classes;
using Microsoft.EntityFrameworkCore;

namespace GameEngine
{
    class GameContext : DbContext
    {
        public DbSet<SaveGame> GameInProgress { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //  => optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=.\ludo.db;Trusted_Connection=True;");


        public static bool isMigration = true;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: This is messy, but needed for migrations.
            // See https://github.com/aspnet/EntityFramework/issues/639
            if (isMigration)
            {
                optionsBuilder.UseSqlServer("(localdb)\\MSSQLLocalDB");
            }
        }

    }
}
