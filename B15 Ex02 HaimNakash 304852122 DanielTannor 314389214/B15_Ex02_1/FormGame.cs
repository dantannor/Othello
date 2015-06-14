// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FormGame.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace B15_Ex02_1
{
    using System;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    using B15_Ex02_1.Control;
    using B15_Ex02_1.Logic;

    public partial class FormGame : Form
    {
        private const int k_GameButtonSize = 30;

        private const int k_Width = 50;

        private const int k_Height = 70;

        public static StringBuilder s_Sb = new StringBuilder();

        private static Board s_Board;

        private static Player s_Player1;

        private static Player s_Player2;

        private static string s_PlayerMove;

        private static eTurn s_PlayerTurn;

        private static Game s_Game;

        private static ePlayer s_PlayerOrPc;

        private readonly eBoardSize r_BoardSize;

        private eTurn m_Turn = eTurn.Player1;

        private string m_Victor;

        private string m_OtherPlayer;

        /// <summary>
        /// Starts the game form
        /// </summary>
        public FormGame()
        {
            FormStart formStart = new FormStart();

            if (formStart.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            initPlayers(formStart.AgainstComputer ? ePlayer.PC : ePlayer.Player);

            this.r_BoardSize = formStart.BoardSize;
            initBoard(this.r_BoardSize);
            s_Game = new Game(s_Player1, s_Player2, s_Board);

            this.Text = "Othello - Black's Turn";
            this.Size = new Size(
                (k_GameButtonSize * (int)r_BoardSize) + k_Width,
                (k_GameButtonSize * (int)r_BoardSize) + k_Height);
            this.BackColor = Color.Gray;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            s_Board = s_Board;

            /*
            if (againstComputer)
            {
              Controller controller = new Controller();
            }
            */
        }

        /// <summary>
        /// Gets the player move
        /// </summary>
        /// <param name="i_PlayerName">Player's name</param>
        /// <param name="i_PlayerTurn">Player's turn</param>
        /// <returns>Player's move from the string builder</returns>
        private static string getPlayerMove(string i_PlayerName, eTurn i_PlayerTurn)
        {
            return !Game.ValidMove(s_Sb.ToString(), i_PlayerTurn) ? null : s_Sb.ToString();
        }

        /// <summary>
        /// Gets the PC move
        /// </summary>
        /// <param name="i_PlayerTurn"></param>
        /// <returns></returns>
        private static string getPcMove(eTurn i_PlayerTurn)
        {
            Random rnd = new Random();
            string pcMove;
            if (i_PlayerTurn == eTurn.Player1)
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

        /// <summary>
        /// Initializes the board according to the size
        /// </summary>
        /// <param name="io_BoardSize"></param>
        private static void initBoard(eBoardSize io_BoardSize)
        {
            s_Board = new Board((int)io_BoardSize);

            char firstBoardNum = (char)(((int)io_BoardSize / 2) + '0');
            char firstBoardLetter = (char)((((int)io_BoardSize / 2) - 1) + 'A');
            char secondBoardLetter = (char)(((int)io_BoardSize / 2) + 'A');
            char secondBoardNum = (char)((((int)io_BoardSize / 2) + 1) + '0');

            s_Board.setCell('O', firstBoardNum, firstBoardLetter);
            s_Board.setCell('X', firstBoardNum, secondBoardLetter);
            s_Board.setCell('X', secondBoardNum, firstBoardLetter);
            s_Board.setCell('O', secondBoardNum, secondBoardLetter);
        }

        private static void initPlayers(ePlayer ePlayer)
        {
            string player1Name = "haim";
            s_Player1 = new Player(player1Name, ePlayer.Player);

            // Determine player2 type and act accordingly
            s_PlayerOrPc = ePlayer;

            switch (s_PlayerOrPc)
            {
                case ePlayer.Player:
                    string player2Name = "nakash";
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

        private void play()
        {
            m_Victor = string.Empty;
            m_OtherPlayer = string.Empty;
            s_PlayerTurn = eTurn.Player1;

            // While game didn't end and player didn't press "Q"
            // while ((s_PlayerTurn != eTurn.GameOver) && (s_PlayerMove != "Q"))
            // {
            // Get player turn
            s_PlayerTurn = s_Game.GetTurn();
            eTurn noMovesPlayer = Game.TurnTransfer();
            if (noMovesPlayer != eTurn.NoTransfer)
            {
                string noMovesPlayerName = playerNoMoves(noMovesPlayer);

                // View.DisplayTurnSwitch(noMovesPlayerName);
            }

            // Get player move, validate and pass it on to game move, which updates the board.
            switch (s_PlayerTurn)
            {
                case eTurn.Player1:

                    if ((s_PlayerMove = getPlayerMove(s_Player1.PlayerName, eTurn.Player1)) == null)
                    {
                        s_PlayerTurn = s_Game.GetTurn();
                        return;
                    }

                    if (s_PlayerMove == "Q")
                    {
                        // continue;
                    }

                    // SetBoard with playermove

                    // SetBoard with playermove
                    s_Game.Move(s_PlayerTurn, s_PlayerMove);

                    // View.DrawBoard(s_Board);
                    break;
                case eTurn.Player2:

                    // Player2
                    if (s_Player2.Type == ePlayer.Player2)
                    {
                        if ((s_PlayerMove = getPlayerMove(s_Player2.PlayerName, eTurn.Player2)) == null)
                        {
                            s_PlayerTurn = s_Game.GetTurn();
                            return;
                        }

                        if (s_PlayerMove == "Q")
                        {
                            // continue;
                        }

                        // SetBoard with playermove
                        s_Game.Move(s_PlayerTurn, s_PlayerMove);

                        // View.DrawBoard(s_Board);
                    }
                    else if (s_Player2.Type == ePlayer.PC)
                    {
                        // PC
                        s_PlayerMove = getPcMove(eTurn.Player2);

                        // SetBoard with playermove
                        s_Game.Move(s_PlayerTurn, s_PlayerMove);

                        // View.DrawBoard(s_Board);
                    }

                    break;
                case eTurn.GameOver:
                    Environment.Exit(1);

                    break;

                // }
            }
        }

        private void newGame()
        {
            restartPlayers();
            initBoard(this.r_BoardSize);
            s_Game = new Game(s_Player1, s_Player2, s_Board);

            // View.DrawBoard(s_Board);
            play();
        }

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

            // View.PrintGameOver(s_Player1.PlayerPoints, s_Player2.PlayerPoints, m_Victor, m_OtherPlayer);
        }

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.initializeControls();
        }

        public void initializeControls()
        {
            for (int i = 0; i < (int)r_BoardSize; i++)
            {
                for (int j = 0; j < (int)r_BoardSize; j++)
                {
                    GameButton boardButton = new GameButton(i, j);
                    boardButton.Location = new Point(j * k_GameButtonSize + 15, i * k_GameButtonSize + 15);

                    boardButton.Size = new Size(k_GameButtonSize, k_GameButtonSize);
                    char coin = s_Board.getCell(i, j);
                    if (coin != null)
                    {
                        boardButton.Text = coin.ToString();

                        this.Controls.AddRange(new System.Windows.Forms.Control[] { boardButton });
                        boardButton.Click += new EventHandler(this.boardButton_Click);
                        this.Controls.Add(boardButton);
                    }
                }
            }

            updateBoard();
        }

        private void boardButton_Click(object sender, EventArgs e)
        {
            GameButton button = sender as GameButton;

            s_Sb.Append((char)('A' + button.Col));
            s_Sb.Append((char)('1' + button.Row));
            play();
            updateBoard();
            s_Sb.Length = 0;
            string colorTurn = string.Empty;
            if (s_PlayerTurn == eTurn.Player1)
            {
                colorTurn = "White's";
            }
            else
            {
                colorTurn = "Black's";
            }

            this.Text = string.Format("Othello - {0} Turn", colorTurn);
            if (s_Player2.Type == ePlayer.PC)
            {
                play();
                updateBoard();
            }

            if (s_Game.GetTurn() == eTurn.GameOver)
            {
                Environment.Exit(1);
            }

            s_Game.GetTurn();
        }

        private void updateBoard()
        {
            foreach (System.Windows.Forms.Control control in this.Controls)
            {
                GameButton button = control as GameButton;
                if (button != null)
                {
                    char soldier = s_Board.getCell(button.Row, button.Col);
                    if (soldier != null)
                    {
                        button.Text = soldier.ToString();
                    }
                    else
                    {
                        button.Text = string.Empty;
                    }

                    if (button.Text == "X")
                    {
                        button.ForeColor = Color.Black;
                        button.BackColor = Color.White;
                    }
                    else if (button.Text == "O")
                    {
                        button.ForeColor = Color.White;
                        button.BackColor = Color.Black;
                    }
                }
            }
        }

        public class GameButton : Button
        {
            private int m_Row;

            private int m_Col;

            public GameButton(int i_Row, int i_Col)
            {
                this.m_Row = i_Row;
                this.m_Col = i_Col;
            }

            public int Row
            {
                get
                {
                    return this.m_Row;
                }
            }

            public int Col
            {
                get
                {
                    return this.m_Col;
                }
            }
        }
    }
}
