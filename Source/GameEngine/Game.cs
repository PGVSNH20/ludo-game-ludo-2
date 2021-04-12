using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public static List<SaveGame> PositionsInGame { get; set; }

        //Whose turn is it
        private int activePlayer = 0;

        //TODO: Set current save if we load from one
        private int currentSave = -1;

        public Game(Rules rules, SaveGame saveGame = null)
        {
            //Initializing
            Rules = rules; //TODO: Correct rules when loading for player count
            Players = AddPlayers(rules.NumberOfPlayers);
            GameBoard = GameBoardGenerator.Generate(11, 11, Players);
            OriginalGameBoard = GameBoardGenerator.Generate(11, 11, Players);

            //Loading for saved games
            if (saveGame != null)
            {
                LoadGame(saveGame);
            }

            //TODO: Dictionary for status messages?

            //Initial variables
            SetStatusMessage($"Started game with {rules.NumberOfPlayers} players and {rules.PiecesPerPlayer} pieces each.");
            SetActionMessage($"It's player {Players[activePlayer].Color}'s turn. Roll the dice.");
            int diceRoll;

            ////////////////////Game loop
            bool running = true;
            while (running)
            {
                ActionMessage = $"It's player {Players[activePlayer].Color}'s turn. Roll the dice.";
                Update();
                var input = GetInput();


                GamePiece activePiece;

                //TODO: Validate input
                switch (input)
                {
                    case 's':
                        DbModel.SaveGame(Players, activePlayer, currentSave);
                        running = false;
                        return;

                    case ' ':

                        diceRoll = dice.Roll();

                        StatusMessage = $"{Players[activePlayer].Color} rolled {diceRoll}!";

                        //MOVEMENT LOGIC
                        int validRange;

                        //TODO: Check for available pieces instead of what the rules says
                        var luckyThrow = (diceRoll == 1 || diceRoll == 6);
                        if (luckyThrow && GameBoardGenerator.PiecesOnBoard(activePlayer) < Rules.PiecesPerPlayer)
                        {
                            //TODO: Six rule
                            if (GameBoardGenerator.PiecesOnBoard(activePlayer) == 0)
                            {
                                StatusMessage = $"{Players[activePlayer].Color} rolled {diceRoll} and placed a piece on the board!";
                                GameBoardGenerator.PlacePieceOnBoard(activePlayer);
                            }
                            else
                            {
                                StatusMessage = $"{Players[activePlayer].Color} rolled {diceRoll}! You can place a new piece on the board or move one.";
                                ActionMessage = $"'Spacebar' to move piece to board or choose a piece to move {diceRoll} steps!";
                                Update();

                                do
                                {

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
                                        activePiece = Players[activePlayer].Pieces[validRange];
                                        Movement.MovePiece(activePiece, diceRoll);
                                        Update();
                                        Thread.Sleep(1500);
                                        break;
                                    }

                                    StatusMessage = $"That didn't seem right, {Players[activePlayer].Color}, try again.";
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
                                StatusMessage = $"{Players[activePlayer].Color} rolled {diceRoll}!";
                                ActionMessage = "But you need 1 or 6 to put a piece on the board...";
                                Update();
                                Thread.Sleep(1500);
                            }
                            else
                            {
                                StatusMessage = $"{Players[activePlayer].Color} rolled {diceRoll}!";
                                ActionMessage = "Choose which piece to move...";
                                Update();

                                if (TryMoveByValidInputRange(diceRoll, out activePiece))
                                {
                                    Update();
                                    Thread.Sleep(500);
                                    break;
                                }

                            }
                        }

                        break;
                }

                if (GameBoardGenerator.PiecesInGoal(activePlayer) == Rules.PiecesPerPlayer)
                {
                    StatusMessage = $"All {Players[activePlayer].Color} game pieces has reached the goal!";
                    ActionMessage = $"Yeaaaah! {Players[activePlayer].Color} won the game!";
                    Update();
                    DbModel.RemoveSaveGame(saveGame);
                    running = false;
                    Draw.Update(Draw.Scene.MainMenu);
                }

                //Reset goal to proper symbol
                GameBoard[5, 5] = OriginalGameBoard[5, 5];

                EndTurn();
            }
        }

        public bool TryMoveByValidInputRange(int diceRoll, out GamePiece activePiece) // out Message message
        {
            int validRange;

            activePiece = null;

            do
            {    //TODO: Validate input with selectable pieces
                var tempInput = GetInput();

                //TODO: Validate that the piece is on the board
                if (ValidInputRange(tempInput, out validRange) && IsGamePieceInGame(validRange, Players[activePlayer]))
                {
                    //ActionMessage = "";
                    //TODO: Validate the move
                    activePiece = Players[activePlayer].Pieces[validRange];
                    Movement.MovePiece(activePiece, diceRoll);
                    //TODO: if false continue;

                    StatusMessage = $"You chose piece {tempInput}!";
                    return true;
                }

            } while (ValidAmountOFGamePieces(validRange) == false);
            return false;
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

        bool ValidInputRange(char tempInput, out int outputRange)
        {
            //validInput = int.TryParse(tempInput.ToString(), out int validInput) && validInput >= 1 && validInput <= 4)

            outputRange = -1;

            if (int.TryParse(tempInput.ToString(), out int validInput) && ValidAmountOFGamePieces(validInput))
            {
                outputRange = validInput - 1;
                return true;
            }
            else return false;

        }

        bool ValidAmountOFGamePieces(int input) => input >= 1 && input <= Players[activePlayer].Pieces.Count; // Ska egentligen bara stå fyra här

        bool IsGamePieceInGame(int selectedPiece, Player player) => player.Pieces[selectedPiece].IsPlacedOnBoard;

        private void LoadGame(SaveGame saveGame)
        {
            var saveId = saveGame.SaveGameId;

            var context = new DbModel();

            activePlayer = saveGame.CurrentPlayer;
            Rules = context.Rules.Find(saveId);
            Players = context.Players.ToList();

            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].Pieces = context.Pieces.Where(p => p.PlayerId == i && p.SaveGameId == saveId).ToList();
            }

            foreach (var player in Players)
            {
                foreach (var piece in player.Pieces)
                {
                    GameBoard[piece.Row, piece.Col] = piece;
                }
            }
        }
    }
}
