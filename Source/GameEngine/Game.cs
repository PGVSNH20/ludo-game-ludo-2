using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                ConsoleKeyInfo input = Console.ReadKey();

                //TODO: Validate input
                switch (input.Key)
                {
                    case ConsoleKey.Spacebar:

                        diceRoll = dice.Roll();

                        StatusMessage = $"You rolled {diceRoll}!";

                        //MOVEMENT LOGIC

                        //DEBUG: Remove/move later
                        PlacePieceOnBoard(activePlayer);

                        //TODO: Player can put piece on board
                        if (diceRoll == 1 || diceRoll == 6)
                        {
                            //TODO: Put piece on board and fix with rules
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
                                //TODO: Validate the move
                                MovePiece(Players[activePlayer].Pieces[validInput - 1], diceRoll);
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
                //TODO: Let players change color?
                //TODO: Fix this, positions and stuff
                players.Add(new Player(Rules.PiecesPerPlayer, playerColors[i], i, new Position(5,0)));
            }

            return players;
        }

        private void PlacePieceOnBoard(int playerId)
        {
            var player = Players[playerId];

            //TODO: Fix piece logic
            GameBoard[player.StartPosition.Col, player.StartPosition.Row] = player.Pieces[0];
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
                if (Traversable[i].Col == position.Col && Traversable[i].Row == position.Row)
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
                if (!TryMove(Traversable[traversablePos]))
                {
                    //Move failed
                    return false;
                }

                //TODO: Check out of range
                newPosition = Traversable[traversablePos + i];
            }

            GameBoard[newPosition.Col, newPosition.Row] = piece;
            GameBoard[position.Col, position.Row] = OriginalGameBoard[position.Col, position.Row];

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
