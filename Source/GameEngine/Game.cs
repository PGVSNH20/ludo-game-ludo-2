using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Interfaces;
using GameEngine.Objects;

namespace GameEngine
{
    /// <summary>
    /// Overlying logic for how the game should run
    /// </summary>
    public class Game : ILudoBoard
    {
        public static IBoardObject[,] GameBoard { get; set; }
        public static Rules Rules;
        private Dice dice = new Dice();
        public string StatusMessage { get; set; }
        public string ActionMessage { get; set; }
        public static List<Player> Players { get; set; }

        //Whose turn is it
        private int activePlayer = 0;

        public Game(Rules rules)
        {
            //Initializing
            Rules = rules;
            Players = AddPlayers(rules.NumberOfPlayers);
            GameBoard = GameBoardGenerator.Generate(11, 11, Players);

            //TODO: Dictionary for status messages?

            //Initial variables
            StatusMessage = "";
            ActionMessage = $"It's player {Players[activePlayer].Color.ToString()}'s turn. Roll the dice."; //Get player color in some way
            int diceRoll;

            ////////////////////Game loop
            bool running = true;
            while (running)
            {
                ActionMessage = $"It's player {Players[activePlayer].Color.ToString()}'s turn. Roll the dice."; //Get player color in some way
                Update();
                ConsoleKeyInfo input = Console.ReadKey();

                //TODO: Validate input
                switch (input.Key)
                {
                    case ConsoleKey.Spacebar:

                        diceRoll = dice.Roll();

                        StatusMessage = $"You rolled {diceRoll}!";

                        //MOVEMENT LOGIC

                        //TODO: Player can put piece on board
                        if (diceRoll == 1 || diceRoll == 6)
                        {
                            //Put piece on board
                            ActionMessage = $"fix";
                            Update();
                        }

                        //TODO: If player has more than one piece on the board, let player choose piece
                        StatusMessage = $"You rolled {diceRoll}!";
                        ActionMessage = "Choose which piece to move...";
                        Update();

                        string inputLine;
                        while (true)
                        {
                            //TODO: Validate input with selectable pieces
                            inputLine = Console.ReadLine();

                            if (int.TryParse(inputLine, out int validInput) && validInput >= 1 && validInput <= 4) //TODO: list of valid pieces contains?
                            {
                                break;
                            }
                        }

                        StatusMessage = $"You chose piece {inputLine}!";
                        ActionMessage = "";
                        Update();

                        //TODO: Movement stuff
                        //Check path if valid and stuff 

                        break;

                }

                EndTurn();
            }
        }

        private void Update() => Draw.Update(Draw.Scene.Game, this);

        private void EndTurn() => activePlayer = activePlayer >= Rules.NumberOfPlayers - 1 ? 0 : activePlayer + 1;

        /// <summary>
        /// Populate a list of players.
        /// </summary>
        /// <param name="NumberOfPlayers">How many players there are</param>
        /// <returns>A list populated with players</returns>
        private List<Player> AddPlayers(int NumberOfPlayers)
        {
            List<Player> players = new List<Player>();

            var playerColors = new List<ConsoleColor>() { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Blue };

            for (int i = 0; i < NumberOfPlayers; i++)
            {
                // TODO:Låta spelare välja färg??
                players.Add(new Player(Rules.PiecesPerPlayer, playerColors[i], i)); // Ändrar kanske här
            }

            return players;
        }

        public void CheckGameState()
        {
            throw new NotImplementedException();
        }

        public void Knuff(GamePiece piece)
        {
            throw new NotImplementedException();
        }

        public void MovePiece(GamePiece piece, int diceRoll)
        {
            throw new NotImplementedException();
        }

        public bool TryMove(int posX, int posY)
        {
            throw new NotImplementedException();
        }


        public static List<DrawableChar> GenerateDrawable()
        {
            List<DrawableChar> drawableChars = new List<DrawableChar>();

            for (int i = 0; i < GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < GameBoard.GetLength(1); j++)
                {
                    var current = GameBoard[i, j];
                    if (current.GetType() == typeof(Nest))
                    {
                        ConsoleColor currentColor = Players[(current as Nest).PlayerId].Color;
                        drawableChars.Add(new DrawableChar(current.CharToDraw, currentColor));
                    }
                    else if (current.GetType() == typeof(Path))
                    {
                        drawableChars.Add(new DrawableChar(current.CharToDraw, ConsoleColor.Gray));
                    }
                    else if (current.GetType() == typeof(InnerSteppingStone))
                    {
                        ConsoleColor currentColor = Players[(current as InnerSteppingStone).PlayerId].Color;
                        drawableChars.Add(new DrawableChar(current.CharToDraw, currentColor));
                    }
                    else if (current.GetType() == typeof(GamePiece))
                    {
                        drawableChars.Add(new DrawableChar(current.CharToDraw, ConsoleColor.DarkYellow));
                    }
                    else if (current.GetType() == typeof(EmptySpace))
                    {
                        drawableChars.Add(new DrawableChar(current.CharToDraw));
                    }
                    else if (current.GetType() == typeof(Goal))
                    {
                        drawableChars.Add(new DrawableChar(current.CharToDraw, ConsoleColor.White));
                    }
                    else
                    {
                        drawableChars.Add(new DrawableChar('%', ConsoleColor.DarkYellow));
                    }
                }
            }


            return drawableChars;
        }
    }


}
