using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using GameEngine.Classes;
using GameEngine.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    foreach (var property in entityType.GetProperties())
            //    {
            //        if (property.ClrType.is)
            //        {
            //            var converterType = typeof(CharToStringConverter)
            //                .MakeGenericType(property.ClrType);
            //            var converter = (ValueConverter)Activator.CreateInstance(converterType, (object)null);
            //            property.SetValueConverter(converter);
            //        }
            //    }
            //}

            var charConverter = new CharToStringConverter();

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Pieces);
            modelBuilder.Entity<Player>().Ignore(t => t.Pieces);

            modelBuilder.Entity<GamePiece>()
                .Ignore(t => t.CharToDraw);

            base.OnModelCreating(modelBuilder);
        }

        public static void SaveGame(List<Player> players, int currentPlayer, int saveId)
        {
            Game.Rules.SaveGameId = saveId;
            var context = new DbModel();

            //if (saveId < 0)
            //{
            context.SaveGames.Add(new SaveGame(currentPlayer));
            saveId = context.SaveGames.Count() + 1;
            context.Rules.Add(new Rules(Game.Rules.NumberOfPlayers, Game.Rules.PiecesPerPlayer, Game.Rules.ThrowAgainOnSixEnabled, Game.Rules.InitialSixRuleEnabled));

            for (var i = 0; i < players.Count; i++)
            {
                var player = players[i];
                var newPlayer = new Player(Game.Rules.PiecesPerPlayer, players[i].Color, i, new Position(players[i].Row, players[i].Col));
                newPlayer.SaveGameId = saveId;

                for (var j = 0; j < player.Pieces.Count; j++)
                {
                    var piece = player.Pieces[j];
                    var pos = GameBoardGenerator.FindObject(Game.GameBoard, piece);
                    var newPiece = new GamePiece(j.ToString().First(), i);
                    newPiece.Row = pos.Row;
                    newPiece.Col = pos.Col;
                    newPiece.SaveGameId = saveId;

                    context.Pieces.Add(newPiece);
                }

                context.Players.Add(newPlayer);
            }

            //}
            //else
            //{
            //    //TODO: Update existing
            //}
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
