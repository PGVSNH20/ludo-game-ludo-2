using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GameEngine.Classes;
using GameEngine.Objects;
using Microsoft.EntityFrameworkCore;

namespace GameEngine
{
    class DbModel : DbContext
    {
        public DbSet<SaveGame> SaveGames { get; set; }
        public DbSet<Rules> Rules { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<GamePiece> Pieces { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=./ludo.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Pieces);
            modelBuilder.Entity<Player>().Ignore(t => t.Pieces);
            base.OnModelCreating(modelBuilder);
        }

        public static void SaveGame(List<Player> players, int currentPlayer, int saveId)
        {
            Game.Rules.SaveGameId = saveId;

            foreach (var player in players)
            {
                player.SaveGameId = saveId;
                foreach (var piece in player.Pieces)
                {
                    var pos = GameBoardGenerator.FindObject(Game.GameBoard, piece);
                    piece.Row = pos.Row;
                    piece.Col = pos.Col;
                }
            }

            var context = new DbModel();

            if (saveId < 0)
            {
                context.SaveGames.Add(new SaveGame(currentPlayer));
                context.Rules.Add(new Rules(Game.Rules.NumberOfPlayers, Game.Rules.PiecesPerPlayer, Game.Rules.ThrowAgainOnSixEnabled, Game.Rules.InitialSixRuleEnabled));
                //context.Rules.Add(Game.Rules);

                foreach (var player in players)
                {
                    context.Players.Add(player);
                    foreach (var piece in player.Pieces)
                    {
                        context.Pieces.Add(piece);
                    }
                }
            }
            else
            {
                //TODO: Update existing
            }
            context.SaveChanges();
        }

        public static void RemoveSaveGame(SaveGame saveGame)
        {
            var context = new DbModel();
            context.SaveGames.Remove(saveGame);
            context.SaveChanges();
        }
    }
}
