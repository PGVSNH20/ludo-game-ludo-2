using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using GameEngine.Objects;
using GameEngine;
using GameEngine.Classes;

namespace LudoTest
{
    
    public class InputTest
    {
        [Fact]
        public void Check_If_Dice_Is_In_Right_Range()
        {
            //Action: 
            //Arrange:
            int[] diceNumbers = { 1, 2, 3, 4, 5, 6 };
            //Expect: 
            Assert.Contains(new Dice().Roll(), diceNumbers);
        }
        [Fact]
        public void Check_If_Specific_Piece_Exists()
        {   
           Rules rules = new Rules();
            var game = new Game(rules);
              Dice dice = new Dice();
           var diceRoll = dice.Roll();

            //game.TryMoveByValidInputRange(diceRoll, out Message message);

            var tempInput = 1;
            //Assert.True(game.TryMoveByValidInputRange(diceRoll, out Message message));

        }

        //[Fact]
        //public void Check_If_Specific_Piece_Exists_And_Is_Avaible()
        //{


        //}
    }
}
