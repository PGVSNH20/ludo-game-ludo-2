using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using GameEngine.Objects;
using GameEngine;
using GameEngine.Classes;
using static GameEngine.Game;
using GameEngine.Interfaces;
using Xunit.Abstractions;

namespace LudoTest
{

    public class InputTest
    {

        private readonly ITestOutputHelper output;

        public InputTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Check_If_Dice_Is_In_Right_Range()
        {
            //Action: 
            //Arrange:
            int[] diceNumbers = { 1, 2, 3, 4, 5, 6 };
            //Expect: 
            Assert.Contains(new Dice().Roll(), diceNumbers);
        }

        [Theory]
        [InlineData('1')]
        [InlineData('2')]
        [InlineData('3')]
        [InlineData('4')]
        public void Check_If_Game_Piece_Index_Is_Avaible_To_Select_From_Char_Input(char inputNumber)
        {
            //Arrange:
            //Action: 
            bool parsedChar = Game.ValidInputRange(inputNumber, out short outputRange);
            //Expect: 
            Assert.True(parsedChar);
            Assert.Contains(outputRange, new short[] { 0, 1, 2, 3 }); //GamePieceIndex

        }
        [Fact]
        public void Add_Players_By_Default_Rule_Setting()
        {
            var rule = new Rules();
            List<Player> players = Game.AddPlayers(rule.NumberOfPlayers, rule.PiecesPerPlayer, playerColors, startingPositions);

            Assert.NotNull(players);
            Assert.Equal(rule.NumberOfPlayers, players.Count);
            Assert.Equal(4, players.Count);

        }
        [Theory]
        [InlineData(ConsoleColor.Red)]
        [InlineData(ConsoleColor.Blue)]
        [InlineData(ConsoleColor.Green)]
        [InlineData(ConsoleColor.Yellow)]
        public void Check_If_Player_Is_Set_To_Default_Color(ConsoleColor color)
        {
            var rule = new Rules();
            List<Player> players = Game.AddPlayers(rule.NumberOfPlayers, rule.PiecesPerPlayer, playerColors, startingPositions);
            Assert.Contains(players, p => p.Color == color);
        }

        [Theory]
        [InlineData(11, 11)]
        public void Generate_A_GameBoard(int row, int col)
        {
            //Arr
            var rule = new Rules();
            List<Player> players = Game.AddPlayers(rule.NumberOfPlayers, rule.PiecesPerPlayer, playerColors, startingPositions);
            IBoardObject[,] GameBoard = GameBoardGenerator.Generate(row, col, players, 4);
            int dimension = GameBoard.GetLength(0) * GameBoard.GetLength(1);
            // Act 
            int totalCol = 0;
            for (int i = 0; i < GameBoard.Length; i++) ++totalCol;

            // Expect
            Assert.NotNull(GameBoard);
            Assert.Equal(totalCol, dimension);
        }

        //[Theory]
        //[InlineData(typeof(Goal))]
        //public void Check_If_Specific_Object_Exist_On_Board(Type type)
        //{
        //    IBoardObject obj = (IBoardObject)Activator.CreateInstance(type);

        //    var rule = new Rules();
        //    List<Player> players = Game.AddPlayers(rule.NumberOfPlayers, rule.PiecesPerPlayer, playerColors, startingPositions);
        //    IBoardObject[,] GameBoard = GameBoardGenerator.Generate(11, 11, players, 4);

        //    // Act 

        //    var position = GameBoardGenerator.FindObject(GameBoard, obj);
        //    output.WriteLine("This is output from {0}", position);
        //    var foundObject = GameBoard[position.Row, position.Col];

        //    // Expect
        //    //Assert.Contains()
        //    Assert.Equal(obj.CharToDraw, foundObject.CharToDraw);

        //}
  
    }
}

