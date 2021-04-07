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
    public class Game : ILudoBoard
    {
        public static IBoardObject[,] GameBoard { get; set; }
        public static IBoardObject[,] OriginalGameBoard { get; set; }

        private static List<Position> Traversable = TraversablePath.CreatePath();

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
            OriginalGameBoard = GameBoardGenerator.Generate(11, 11, Players);

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
                var input = GetInput();

                //TODO: Validate input
                switch (input)
                {
                    case ' ':

                        diceRoll = dice.Roll();

                        StatusMessage = $"You rolled {diceRoll}!";

                        //MOVEMENT LOGIC

                        //TODO: Player can put piece on board
                        if ((diceRoll == 1 || diceRoll == 6) && PiecesOnBoard(activePlayer) < Rules.PiecesPerPlayer)
                        {
                            if (PiecesOnBoard(activePlayer) == 0)
                            {
                                StatusMessage = $"You rolled {diceRoll} and placed a piece on the board!";
                                PlacePieceOnBoard(activePlayer);
                            }
                            else
                            {
                                StatusMessage = $"You rolled {diceRoll}! You can place a new piece on the board or move one.";
                                ActionMessage = $"'Spacebar' to move piece to board or choose a piece to move {diceRoll} steps!";

                                while (true)
                                {
                                    Update();
                                    char tempInput = GetInput();

                                    if (tempInput == ' ')
                                    {
                                        PlacePieceOnBoard(activePlayer);
                                        break;
                                    }
                                    //TODO: Validate that the piece is on the board and maybe make a function for this to avoid redundancy
                                    else if (int.TryParse(tempInput.ToString(), out int validInput) && validInput >= 1 && validInput <= 4)
                                    {
                                        //TODO: Validate the move
                                        MovePiece(Players[activePlayer].Pieces[validInput - 1], diceRoll);
                                        break;
                                    }

                                    StatusMessage = $"That didn't seem right, try again.";
                                    ActionMessage = $"'Spacebar' to move piece to board or choose a piece to move {diceRoll} steps!";
                                }
                            }


                            ////TODO: Put piece on board and fix with rules
                            //PlacePieceOnBoard(activePlayer);
                            //ActionMessage = $"fix";
                            //Update();
                        }
                        else
                        {
                            //TODO: Update code to the above which is newer)
                            if (PiecesOnBoard(activePlayer) == 0)
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
                                while (true)
                                {
                                    //TODO: Validate input with selectable pieces
                                    var tempInput = GetInput();

                                    //TODO: Validate that the piece is on the board
                                    if (int.TryParse(tempInput.ToString(), out int validInput) && validInput >= 1 && validInput <= 4)
                                    {
                                        //TODO: Validate the move
                                        MovePiece(Players[activePlayer].Pieces[validInput - 1], diceRoll);

                                        StatusMessage = $"You chose piece {tempInput}!";
                                        ActionMessage = "";
                                        Update();
                                        break;
                                    }
                                }
                            }
                        }

                        break;
                }

                EndTurn();
            }
        }

        private void Update() => Draw.Update(Draw.Scene.Game, this);

        private void EndTurn() => activePlayer = activePlayer >= Rules.NumberOfPlayers - 1 ? 0 : activePlayer + 1;

        private char GetInput()
        {
            ConsoleKeyInfo input = Console.ReadKey();
            return input.KeyChar;
        }

        /// <summary>
        /// Populate a list of players.
        /// </summary>
        /// <param name="NumberOfPlayers">How many players there are</param>
        /// <returns>A list populated with players</returns>
        private List<Player> AddPlayers(int NumberOfPlayers)
        {
            List<Player> players = new List<Player>();

            var playerColors = new List<ConsoleColor>() { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Yellow};
            var startingPositions = new List<Position>()
            {
                new Position(0, 6),
                new Position(6, 10),
                new Position(10, 4),
                new Position(4, 0),
            };

            for (int i = 0; i < NumberOfPlayers; i++)
            {
                //TODO: Let players change color?
                //TODO: Fix so the positions are in the right spot even without 4 players
                players.Add(new Player(Rules.PiecesPerPlayer, playerColors[i], i, startingPositions[i]));
            }

            return players;
        }

        private void PlacePieceOnBoard(int playerId)
        {
            var player = Players[playerId];

            foreach (var piece in Players[playerId].Pieces)
            {
                if (!piece.IsPlacedOnBoard)
                {
                    //TODO: Make sure piece logic is correct
                    GameBoard[player.StartPosition.Row, player.StartPosition.Col] = piece;
                    piece.IsPlacedOnBoard = true;
                    return;
                }
            }
        }

        private int PiecesOnBoard(int playerId)
        {
            var totalPieces = 0;

            foreach (var piece in Players[playerId].Pieces)
            {
                if (piece.IsPlacedOnBoard)
                {
                    totalPieces++;
                }
            }

            return totalPieces;
        }

        public Position FindObject(IBoardObject[,] board, IBoardObject obj)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j].Equals(obj))
                    {
                        return new Position(i, j);
                    }
                }
            }

            return new Position();
        }

        public void CheckGameState()
        {
            throw new NotImplementedException();
        }

        public void Knuff(GamePiece piece)
        {
            throw new NotImplementedException();
        }

        public bool MovePiece(GamePiece piece, int diceRoll)
        {
            Position position = FindObject(GameBoard, piece);
            Position newPosition = new Position();

            int traversablePos = -1;
            for (int i = 0; i < Traversable.Count; i++)
            {
                if (Traversable[i].Row == position.Row && Traversable[i].Col == position.Col)
                {
                    traversablePos = i;
                }
            }

            if (traversablePos == -1)
            {
                //Couldn't find position
                return false;
            }

            for (int i = 0; i < diceRoll; i++)
            {
                if (!TryMove(Traversable[traversablePos + 1]))
                {
                    //Move failed
                    return false;
                }

                //TODO: Check out of range
                newPosition = Traversable[traversablePos + i + 1];
            }

            GameBoard[newPosition.Row, newPosition.Col] = piece;
            GameBoard[position.Row, position.Col] = OriginalGameBoard[position.Row, position.Col];

            return true;
        }

        public bool TryMove(Position position)
        {
            //TODO: Check if can move

            return true;
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
    }


}
