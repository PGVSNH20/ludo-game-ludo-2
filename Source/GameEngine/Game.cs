using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameEngine.Classes;
using GameEngine.Interfaces;
using GameEngine.Objects;

namespace GameEngine
{
    /// <summary>
    /// Overlying logic for how the game should run
    /// </summary>
    public class Game : ILudoBoard, IDrawable
    {
        public static IBoardObject[,] GameBoard { get; set; }
        public static IBoardObject[,] OriginalGameBoard { get; set; }

        public static List<Position> Traversable = TraversablePath.CreatePath();

        public static Rules Rules;
        private Dice dice = new Dice();
        public static string StatusMessage { get; set; }
        public static string ActionMessage { get; set; }
        public static List<Player> Players { get; set; }

        //Whose turn is it
        private int activePlayer = 0;

        public Game(Rules rules)
        {
            //Initializing
            Rules = rules;
            Players = AddPlayers(rules.NumberOfPlayers);
            GameBoard = GameBoardGenerator.Generate(11, 11, Players);
            OriginalGameBoard = GameBoardGenerator.Generate(11, 11, Players);

            //TODO: Dictionary for status messages?

            //Initial variables
            SetStatusMessage($"Started game with {rules.NumberOfPlayers} players and {rules.PiecesPerPlayer} pieces each.");
            SetActionMessage($"It's player {Players[activePlayer].Color.ToString()}'s turn. Roll the dice.");
            int diceRoll;

            ////////////////////Game loop
            bool running = true;
            while (running)
            {
                ActionMessage = $"It's player {Players[activePlayer].Color.ToString()}'s turn. Roll the dice.";
                Update();
                var input = GetInput();

                //TODO: Validate input
                switch (input)
                {
                    case ' ':

                        diceRoll = dice.Roll();

                        StatusMessage = $"You rolled {diceRoll}!";

                        //MOVEMENT LOGIC
                        int validRange;

                        //TODO: Check for available pieces instead of what the rules says
                        var luckyThrow = (diceRoll == 1 || diceRoll == 6);
                        if (luckyThrow && GameBoardGenerator.PiecesOnBoard(activePlayer) < Rules.PiecesPerPlayer)
                        {
                            //TODO: Six rule
                            if (GameBoardGenerator.PiecesOnBoard(activePlayer) == 0)
                            {
                                StatusMessage = $"You rolled {diceRoll} and placed a piece on the board!";
                                GameBoardGenerator.PlacePieceOnBoard(activePlayer);
                            }
                            else
                            {
                                StatusMessage = $"You rolled {diceRoll}! You can place a new piece on the board or move one.";
                                ActionMessage = $"'Spacebar' to move piece to board or choose a piece to move {diceRoll} steps!";

                                 do
                                {
                                
                                   Update();
                                   char tempInput = GetInput();

                                   if (tempInput == ' ')
                                   {
                                       GameBoardGenerator.PlacePieceOnBoard(activePlayer);
                                       break;
                                   }
                                   //TODO: Validate that the piece is on the board and maybe make a function for this to avoid redundancy
                                   else if (ValidInputRange(tempInput, out validRange))
                                   {
                                       //TODO: Validate the move
                                       Movement.MovePiece(Players[activePlayer].Pieces[validRange - 1], diceRoll);
                                       break;
                                   }

                                   StatusMessage = $"That didn't seem right, try again.";
                                   ActionMessage = $"'Spacebar' to move piece to board or choose a piece to move {diceRoll} steps!";
                                

                                } while (ValidAmountOFGamePieces(validRange) == false);
                            }


                            ////TODO: Put piece on board and fix with rules
                            //PlacePieceOnBoard(activePlayer);
                            //ActionMessage = $"fix";
                            //Update();
                        }
                        else
                        {
                            if (GameBoardGenerator.PiecesOnBoard(activePlayer) == 0)
                            {
                                StatusMessage = $"You rolled {diceRoll}!";
                                ActionMessage = "But you need 1 or 6 to put a piece on the board...";
                                Update();
                                Thread.Sleep(1500);
                            }
                            else
                            {
                                StatusMessage = $"You rolled {diceRoll}!";
                                ActionMessage = "Choose which piece to move...";
                                Update();

                               
                                do
                                {    //TODO: Validate input with selectable pieces
                                    var tempInput = GetInput();

                                    //TODO: Validate that the piece is on the board
                                    if (ValidInputRange(tempInput, out validRange))
                                    {
                                        ActionMessage = "";
                                        //TODO: Validate the move
                                        Movement.MovePiece(Players[activePlayer].Pieces[validRange - 1], diceRoll);
                                        //TODO: if false continue;
                                        StatusMessage = $"You chose piece {tempInput}!";
                                        Update();
                                        Thread.Sleep(500);
                                        break;
                                    }

                                } while (ValidAmountOFGamePieces(validRange) == false);
                            }
                        }

                        break;
                }

                EndTurn();
            }
        }

        public static void SetActionMessage(string message) => ActionMessage = message;

        public static void SetStatusMessage(string message) => StatusMessage = message;

        public void Update() => Draw.Update(Draw.Scene.Game, this);

        /// <summary>
        /// Populate a list of players.
        /// </summary>
        /// <param name="numberOfPlayers">How many players there are</param>
        /// <returns>A list populated with players</returns>
        private List<Player> AddPlayers(int numberOfPlayers)
        {
            List<Player> players = new List<Player>();

            //TODO: Place these variables somewhere better?
            var playerColors = new List<ConsoleColor>() { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Yellow };
            //TODO: If not 4 players, fix starting positions. Also make it so nests and these get their positions from the same place.
            var startingPositions = new List<Position>()
            {
                new Position(0, 6),
                new Position(6, 10),
                new Position(10, 4),
                new Position(4, 0),
            };

            for (int i = 0; i < numberOfPlayers; i++)
            {
                //TODO: Let players change color?
                players.Add(new Player(Rules.PiecesPerPlayer, playerColors[i], i, startingPositions[i]));
            }

            return players;
        }

        private void EndTurn() => activePlayer = activePlayer >= Rules.NumberOfPlayers - 1 ? 0 : activePlayer + 1;

        private char GetInput() => Console.ReadKey().KeyChar;

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
                    else if (current.GetType() == typeof(DoorwayToInnerPath))
                    {
                        //ConsoleColor currentColor = Players[(current as DoorwayToInnerPath).PlayerId].Color; // Sätt till konstig färg, så länge den tillhör playya
                        drawableChars.Add(new DrawableChar(current.CharToDraw, ConsoleColor.DarkGray));
                    }
                    else if (current.GetType() == typeof(InnerSteppingStone))
                    {
                        ConsoleColor currentColor = Players[(current as InnerSteppingStone).PlayerId].Color;
                        drawableChars.Add(new DrawableChar(current.CharToDraw, currentColor));
                    }
                    else if (current.GetType() == typeof(GamePiece))
                    {
                        ConsoleColor currentColor = Players[(current as GamePiece).PlayerId].Color;
                        drawableChars.Add(new DrawableChar(current.CharToDraw, currentColor));
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
        bool ValidInputRange(char tempInput, out int validInputToOut)
        {
            //validInput = int.TryParse(tempInput.ToString(), out int validInput) && validInput >= 1 && validInput <= 4)

            validInputToOut = -1;

            if (int.TryParse(tempInput.ToString(), out int validInput) && ValidAmountOFGamePieces(validInput))
            {
                validInputToOut = validInput;
                return true;
            }
            else return false;

        }
        bool ValidAmountOFGamePieces(int input) => input >= 1 && input <= 4;


    }
}
