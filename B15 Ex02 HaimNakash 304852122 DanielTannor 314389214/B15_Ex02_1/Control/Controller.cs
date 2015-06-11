// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Controller.cs" company="">
//   
// </copyright>
// <summary>
//   The e board size.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows.Forms;

namespace B15_Ex02_1.Control
{
    using System;
    using System.Text.RegularExpressions;

    using B15_Ex02_1.Logic;
    using B15_Ex02_1.UI;

    /*
    * Board size
    */
    public enum eBoardSize
    {
        Six = 6, 

        Eight = 8,

        Ten = 10,

        Twelve = 12
    }

    public class Controller
    {
        private static FormGame gameForm;
 
        private static Board s_Board;

        private static Player s_Player1;

        private static Player s_Player2;

        private static string s_PlayerMove;

        private static eTurn s_PlayerTurn;

        private static Game s_Game;

        private static ePlayer s_PlayerOrPc;

        private readonly eBoardSize r_BoardSize;

        private string m_Victor;

        private string m_OtherPlayer;

        /*
         * 2nd Player type PC/Player 2
         */
        public enum ePlayer
        {
            Player = 1,

            PC = 2,

            Player2 = 3
        }

        /*
         * Gets board size
         */
        private static eBoardSize getBoardSize()
        {
            int boardSizeNum;
            string boardSize = View.AskBoardSize();
            int.TryParse(boardSize, out boardSizeNum);

            while (!Enum.IsDefined(typeof(eBoardSize), boardSizeNum))
            {
                Console.WriteLine();
                View.PrintInvalidInput("Sorry, that's an invalid board size. Please re-enter:");
                int.TryParse(View.AskBoardSize(), out boardSizeNum);
            }

            return (eBoardSize)boardSizeNum;
        }

        /*
         * Gets player move from user
         */
        private static string getPlayerMove(string io_PlayerName, eTurn io_PlayerTurn)
        {
            // Read player name and check input
            
            string playerMove = View.ScanPlayerMove(io_PlayerName);

            while (!Game.ValidMove(playerMove, io_PlayerTurn))
            {
                Console.WriteLine();
                View.PrintInvalidInput("Sorry, that's an invalid move. Please re-enter:");
                View.DrawBoard(s_Board);
                playerMove = View.ScanPlayerMove(io_PlayerName);
            }

            return playerMove;
        }

        /*
         * PC calculates a random move
         */
        private static string getPcMove(eTurn io_PlayerTurn)
        {
            Random rnd = new Random();
            string pcMove;
            if (io_PlayerTurn == eTurn.Player1)
            {
                int rndCell = rnd.Next(0, s_Game.PcMovesList.Count);
                pcMove = s_Game.PcMovesList[rndCell];
            }
            else
            {
                int rndCell = rnd.Next(0, s_Game.PcMovesList.Count);
                pcMove = s_Game.PcMovesList[rndCell];
            }

            
            return pcMove;
        }

        /*
         * Initializes the board by scanning a size and creating.
         */
        private static void initBoard(eBoardSize io_BoardSize)
        {
            switch (io_BoardSize)
            {
                case eBoardSize.Six:
                    s_Board = new Board(6);

                    s_Board.setCell('O', '3', 'C');
                    s_Board.setCell('X', '3', 'D');
                    s_Board.setCell('X', '4', 'C');
                    s_Board.setCell('O', '4', 'D');

                    break;

                case eBoardSize.Eight:
                    s_Board = new Board(8);

                    s_Board.setCell('O', '4', 'D');
                    s_Board.setCell('X', '4', 'E');
                    s_Board.setCell('X', '5', 'D');
                    s_Board.setCell('O', '5', 'E');

                    break;
            }
        }

        /*
         * Scan player names and create player instances
         */
        private static void initPlayers()
        {
            string player1Name = getPlayerName();
            s_Player1 = new Player(player1Name, ePlayer.Player);

            // Determine player2 type and act accordingly
            s_PlayerOrPc = getPlayer2Type();

            switch (s_PlayerOrPc)
            {
                case ePlayer.Player:
                    string player2Name = getPlayerName();
                    s_Player2 = new Player(player2Name, ePlayer.Player2);
                    break;

                case ePlayer.PC:
                    s_Player2 = new Player("*PC*", ePlayer.PC);
                    break;
            }
        }

        /*
         * Restart the players for another round
         */
        private static void restartPlayers()
        {
            s_Player1 = new Player(s_Player1.PlayerName, ePlayer.Player);

            switch (s_PlayerOrPc)
            {
                case ePlayer.Player:
                    s_Player2 = new Player(s_Player2.PlayerName, ePlayer.Player2);
                    break;

                case ePlayer.PC:
                    s_Player2 = new Player("*PC*", ePlayer.PC);
                    break;
            }
        }
       
        /*
         * Initial start up of values at game beginning.
         */
        public Controller()
        {

            FormStart form = new FormStart();

            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }

           

            View.Welcome();
            initPlayers();
            this.r_BoardSize = getBoardSize();
            initBoard(this.r_BoardSize);
            gameForm = new FormGame();
            gameForm.ShowDialog();
            s_Game = new Game(s_Player1, s_Player2, s_Board);
            View.DrawBoard(s_Board);
            play();
        }

        /*
         * Validates player name
         */
        public static bool ValidPlayerName(string io_PlayerName)
        {
            const string sPattern = "[A-Za-z0-9]+";

            return Regex.IsMatch(io_PlayerName, sPattern);
        }

        /*
         * scans and validates correct player name using UI
         */
        private static string getPlayerName()
        {
            // Read player name and check input
            string playerName = View.ScanPlayerName();

            while (!ValidPlayerName(playerName))
            {
                Console.WriteLine();
                View.PrintInvalidInput("Sorry, that's an invalid player name. Please re-enter:");
                playerName = View.ScanPlayerName();
            }

            return playerName;
        }

        /*
         * Ask for Player 2 or PC from user and validate
         */
        private static ePlayer getPlayer2Type()
        {
            int playerTypeNum;
            string playerType = View.AskPlayerType();
            int.TryParse(playerType, out playerTypeNum);

            while (!Enum.IsDefined(typeof(ePlayer), playerTypeNum) || (playerTypeNum == (int)ePlayer.Player2))
            {
                Console.WriteLine();
                View.PrintInvalidInput("Sorry, that's an invalid player type. Please re-enter:");
                int.TryParse(View.AskPlayerType(), out playerTypeNum);
            }

            return (ePlayer)playerTypeNum;
        }

        /*
         * Starts and handles the game.
         */
        private void play()
        {
            m_Victor = string.Empty;
            m_OtherPlayer = string.Empty;
            s_PlayerTurn = eTurn.Player1;

            // While game didn't end and player didn't press "Q"
            while ((s_PlayerTurn != eTurn.GameOver) && (s_PlayerMove != "Q"))
            {
                // Get player turn
                s_PlayerTurn = s_Game.GetTurn();
                eTurn noMovesPlayer = Game.TurnTransfer();
                if (noMovesPlayer != eTurn.NoTransfer)
                {
                    string noMovesPlayerName = playerNoMoves(noMovesPlayer);
                    View.DisplayTurnSwitch(noMovesPlayerName);
                }

                // Get player move, validate and pass it on to game move, which updates the board.
                switch (s_PlayerTurn)
                {
                    case eTurn.Player1:

                        s_PlayerMove = getPlayerMove(s_Player1.PlayerName, eTurn.Player1);
                        if (s_PlayerMove == "Q")
                        {
                            continue;
                        }
                        // SetBoard with playermove
                        

                        // SetBoard with playermove
                        s_Game.Move(s_PlayerTurn, s_PlayerMove);

                       
                        View.DrawBoard(s_Board);
                        
                        break;
                    case eTurn.Player2:

                        // Player2
                        if (s_Player2.Type == ePlayer.Player2)
                        {
                            s_PlayerMove = getPlayerMove(s_Player2.PlayerName, eTurn.Player2);

                            if (s_PlayerMove == "Q")
                            {
                                continue;
                            }

                            // SetBoard with playermove
                            s_Game.Move(s_PlayerTurn, s_PlayerMove);

                            
                            View.DrawBoard(s_Board);
                        }
                        else if (s_Player2.Type == ePlayer.PC)

                            // PC
                        {
                            s_PlayerMove = getPcMove(eTurn.Player2);
                          

                            // SetBoard with playermove
                            s_Game.Move(s_PlayerTurn, s_PlayerMove);

                            View.DrawBoard(s_Board);
                        }

                        break;
                    case eTurn.GameOver:
                        this.endGame();
                        eAnotherGame anotherGame = (eAnotherGame)int.Parse(View.AskPlayAgain());

                        if (anotherGame == eAnotherGame.Y)
                        {
                            newGame();
                        }

                        break;
                }
            }

            Environment.Exit(0);
        }

        /*
         * Another round!
         */
        private void newGame()
        {
            restartPlayers();
            initBoard(this.r_BoardSize);
            s_Game = new Game(s_Player1, s_Player2, s_Board);
            View.DrawBoard(s_Board);
            play();
        }

        /*
         * Player answer for another game
         */

        public enum eAnotherGame
        {
            Y = 1
        }

        /*
         * Decides victor and prints status to screen
         */
        private void endGame()
        {
            if (s_Player1.PlayerPoints > s_Player2.PlayerPoints)
            {
                m_Victor = s_Player1.PlayerName;
                m_OtherPlayer = s_Player2.PlayerName;
            }
            else if (s_Player2.PlayerPoints > s_Player1.PlayerPoints)
            {
                m_Victor = s_Player2.PlayerName;
                m_OtherPlayer = s_Player1.PlayerName;
            }

            View.PrintGameOver(s_Player1.PlayerPoints, s_Player2.PlayerPoints, m_Victor, m_OtherPlayer);
        }

        /*
         * Lets us know which player has no movies
         */
        private string playerNoMoves(eTurn i_NoMovesPlayer)
        {
            string noMovesPlayerName = null;

            switch (i_NoMovesPlayer)
            {
                case eTurn.Player1:
                    noMovesPlayerName = s_Player1.PlayerName;
                    break;
                case eTurn.Player2:
                    noMovesPlayerName = s_Player2.PlayerName;
                    break;
            }

            return noMovesPlayerName;
        }
    }
}