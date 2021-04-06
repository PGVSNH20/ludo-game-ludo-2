using GameEngine;
using GameEngine.Interfaces;
using GameEngine.Objects;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LudoTest
{
    public class GameBoardTest
    {
        [Fact]
        public  void Create_Four_Player_As_Default_Setting_When_Default_Rules_Is_Implemented()
        {

            //Arrange:
            //Action: 
            Game game = new Game(new Rules());

            //Expect: 

            Assert.Equal(4, Game.Players.Count);
        }
        [Fact]
        public void Create_A_List_Of_Five_Steppingstones_In_InnerPath()
        {

            //Arrange:
            IBoardObject[,] gameBoard = new IBoardObject[11, 11];
            int counter = 0;
            //Action: 
            for (int i = 0; i < gameBoard.GetLength(0); i++)
                for (int j = 0; j < gameBoard.GetLength(1); j++) 
                    gameBoard[i, j] = new Path();
                       
            gameBoard = GameBoardGenerator.PopulateWithInnerPath(gameBoard);

            for (int i = 0; i < gameBoard.GetLength(0); i++)
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                    if (gameBoard[i, j].GetType() == typeof(InnerSteppingStone))
                        ++counter;

            var innerpathList = counter / 4;

            //Expect: 

            Assert.Equal(5, innerpathList);
        }

    }
}
