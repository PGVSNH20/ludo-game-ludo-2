using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using GameEngine.Objects;
using GameEngine;

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
    }
}
