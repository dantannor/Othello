// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game.cs" company="">
//   
// </copyright>
// <summary>
//   The e turn.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace B15_Ex02_1.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /*
    * Saves whose turn it is
    */
    public enum eTurn
    {
        Player1 = 1,

        Player2 = 2,

        GameOver,

        NoTransfer
    }

    public class Game
    {
        private static eTurn s_NextPlayerTurn;

        private static List<string> m_Player1MovesList;

        private static List<string> m_Player2MovesList;

        private static eTurn s_PlayerTurn = eTurn.Player1;

        private static bool v_NoPlayer1Moves;

        private static bool v_NoPlayer2Moves;

        private List<char> CellsNeededToChange = new List<char>();

        private List<int> foundLegalMoveNeighbours = new List<int>();

        private List<int> numberOfCellsNeededToChangeArray = new List<int>();

        private List<char> computerLegalMovesRow = new List<char>();

        private List<char> computerLegalMovesCol = new List<char>();

        private int numberOfCellsNeededToChange = 1;

        private bool changeTheSequence;

        private Player m_Player1;

        private Player m_Player2;

        private Board m_Board;

        public static bool checkedValidCell(Board board, char i_Row, char i_Column)
        {
            while (board.getCell(i_Row, i_Column) == 'O' || board.getCell(i_Row, i_Column) == 'X')
            {
                return false;
            }

            return true;
        }

        public static List<string> Player1Moves
        {
            get
            {
                return m_Player1MovesList;
            }

            set
            {
                m_Player1MovesList = value;
            }
        }

        public static List<string> Player2Moves
        {
            get
            {
                return m_Player2MovesList;
            }

            set
            {
                m_Player2MovesList = value;
            }
        }

        /*
         * Creates game instance with two player types.
         */
        public Game(Player io_Player1, Player io_Player2, Board io_Board)
        {
            m_Player1 = io_Player1;
            m_Player2 = io_Player2;
            m_Board = io_Board;
            s_NextPlayerTurn = eTurn.Player1;
        }

        public static eTurn TurnTransfer()
        {
            eTurn turn;
            if (v_NoPlayer1Moves && v_NoPlayer2Moves == false)
            {
                turn = eTurn.Player1;
            }
            else if (v_NoPlayer2Moves && v_NoPlayer1Moves == false)
            {
                turn = eTurn.Player2;
            }
            else
            {
                turn = eTurn.NoTransfer;
            }

            return turn;
        }

        public static bool ValidMove(string io_PlayerMove, eTurn io_Player)
        {
            return (io_Player == eTurn.Player1)
                       ? (m_Player1MovesList.Contains(io_PlayerMove) || io_PlayerMove == "Q")
                       : (m_Player2MovesList.Contains(io_PlayerMove) || io_PlayerMove == "Q");
        }



        public List<string> PcMovesList
        {
            get { return m_Player2MovesList; }
        }


        // check all adjacent cells to  specific cell

        /* 
         * Returns game status
         */

        /*
         * Gets current player turn
         */
        public eTurn GetTurn()
        {
            v_NoPlayer1Moves = false;
            v_NoPlayer2Moves = false;
            s_PlayerTurn = s_NextPlayerTurn;

            switch (s_PlayerTurn)
            {
                // Player 1's turn
                case eTurn.Player1:

                    // Get Player 1 moves
                    m_Player1MovesList = validCells(eTurn.Player1);

                    // Player 1 has no moves
                    if (!m_Player1MovesList.Any())
                    {
                        // View call
                        if (v_NoPlayer2Moves)
                        {
                            // Both players have no moves, return game over.
                            s_PlayerTurn = eTurn.GameOver;
                        }
                        else
                        {
                            // Switch to player 2's turn
                            v_NoPlayer1Moves = true;
                            s_PlayerTurn = eTurn.Player2;
                            goto case eTurn.Player2;
                        }
                    }
                    else
                    {
                        // Player 1 has moves in the move list
                        s_PlayerTurn = eTurn.Player1;
                    }

                    break;

                case eTurn.Player2:
                    m_Player2MovesList = validCells(eTurn.Player2);
                    if (!m_Player2MovesList.Any())
                    {
                        if (v_NoPlayer1Moves)
                        {
                            s_PlayerTurn = eTurn.GameOver;
                        }
                        else
                        {
                            v_NoPlayer2Moves = true;
                            s_PlayerTurn = eTurn.Player1;
                            goto case eTurn.Player1;
                        }
                    }
                    else
                    {
                        // Player 2 has moves in the move list
                        s_PlayerTurn = eTurn.Player2;
                    }

                    break;
            }

            // Set next players turn
            s_NextPlayerTurn = (s_PlayerTurn == eTurn.Player1) ? eTurn.Player2 : eTurn.Player1;

            return s_PlayerTurn;
        }

        public void checkAndFillListValidMoves()
        {
            for (int i = 49; i < 49 + m_Board.Size; i++)
                {
                    for (int j = 65; j < 65 + m_Board.Size; j++)
                    {
                        if (checkedValidCell(m_Board, (char)i, (char)j) && checkedValidMove(m_Board, (char)i, (char)j))
                        {
                            computerLegalMovesRow.Add((char)i);
                            computerLegalMovesCol.Add((char)j);

                            numberOfCellsNeededToChangeArray.Clear();
                            foundLegalMoveNeighbours.Clear();
                            changeTheSequence = false;
                            CellsNeededToChange.Clear();
                        }
                    }
                }

                CellsNeededToChange.Clear();
                foundLegalMoveNeighbours.Clear();
                numberOfCellsNeededToChangeArray.Clear();
                changeTheSequence = false;
            
                // boardSize = 6
//            else
//            {
//                for (int i = 49; i < 55; i++)
//                {
//                    for (int j = 65; j < 71; j++)
//                    {
//                        if (checkedValidCell(m_Board, (char)i, (char)j) && checkedValidMove(m_Board, (char)i, (char)j))
//                        {
//                            computerLegalMovesRow.Add((char)i);
//                            computerLegalMovesCol.Add((char)j);
//
//                            numberOfCellsNeededToChangeArray.Clear();
//                            foundLegalMoveNeighbours.Clear();
//                            changeTheSequence = false;
//                            CellsNeededToChange.Clear();
//                        }
//                    }
//                }
//
//                CellsNeededToChange.Clear();
//                foundLegalMoveNeighbours.Clear();
//                numberOfCellsNeededToChangeArray.Clear();
//                changeTheSequence = false;
//            }
        }

        public List<string> validCells(eTurn io_curPlayer)
        {
            s_PlayerTurn = io_curPlayer;
            string cell = string.Empty;
            List<string> validMoveList = new List<string>();
            checkAndFillListValidMoves();
            for (int i = 0; i < computerLegalMovesRow.Count; i++)
            {
                cell += computerLegalMovesCol[i];
                cell += computerLegalMovesRow[i];
                validMoveList.Add(cell);
                cell = string.Empty;
            }

            computerLegalMovesRow.Clear();
            computerLegalMovesCol.Clear();
            return validMoveList;
        }

        public void Move(eTurn io_curPlayer, string io_cell)
        {
            s_PlayerTurn = io_curPlayer;

            char row;
            char column;

            Console.WriteLine();

            row = io_cell[1];
            column = io_cell[0];

            if (checkedValidCell(m_Board, row, column) && checkedValidMove(m_Board, row, column))
            {
                if (io_curPlayer == eTurn.Player1)
                {
                    m_Board.setCell('O', row, column);
                    for (int i = 0, j = 0; i < foundLegalMoveNeighbours.Count; i++, j += 2)
                    {
                        char cellRowToChange = CellsNeededToChange[j];
                        char cellColumnToChange = CellsNeededToChange[j + 1];
                        int whichNeighborMove = foundLegalMoveNeighbours[i];
                        int numberOfCellsToChange = numberOfCellsNeededToChangeArray[i];
                        drawAllChangeCells(
                            m_Board,
                            cellRowToChange,
                            cellColumnToChange,
                            whichNeighborMove,
                            numberOfCellsToChange);
                        m_Player1.PlayerPoints = m_Player1.PlayerPoints + numberOfCellsToChange;
                        m_Player2.PlayerPoints = m_Player2.PlayerPoints - numberOfCellsToChange;
                    }

                    m_Player1.PlayerPoints++;
                }
                else
                {
                    m_Board.setCell('X', row, column);
                    for (int i = 0, j = 0; i < foundLegalMoveNeighbours.Count; i++, j += 2)
                    {
                        char cellRowToChange = CellsNeededToChange[j];
                        char cellColumnToChange = CellsNeededToChange[j + 1];
                        int whichNeighborMove = foundLegalMoveNeighbours[i];
                        int numberOfCellsToChange = numberOfCellsNeededToChangeArray[i];
                        drawAllChangeCells(
                            m_Board,
                            cellRowToChange,
                            cellColumnToChange,
                            whichNeighborMove,
                            numberOfCellsToChange);
                        m_Player2.PlayerPoints = m_Player2.PlayerPoints + numberOfCellsToChange;
                        m_Player1.PlayerPoints = m_Player1.PlayerPoints - numberOfCellsToChange;
                    }

                    m_Player2.PlayerPoints++;
                }

                CellsNeededToChange.Clear();
                foundLegalMoveNeighbours.Clear();
                numberOfCellsNeededToChangeArray.Clear();
                changeTheSequence = false;

            }
        }


        // check all adjacent cells to  specific cell
        private bool checkedValidMove(Board io_board, char io_row, char io_column)
        {
            int minusLine = io_row - 1;
            int minusColumn = io_column - 1;
            int plusLine = io_row + 1;
            int plusColumn = io_column + 1;
            char opponentTool;

            // player one turn

            if (s_PlayerTurn == eTurn.Player1)
            {
                opponentTool = 'X';
            }
            else
            {
                opponentTool = 'O';
            }

            // get all adjacent cells 
            char neighbor0 = io_board.getCell((char)minusLine, (char)minusColumn);
            char neighbor1 = io_board.getCell((char)minusLine, io_column);
            char neighbor2 = io_board.getCell((char)minusLine, (char)plusColumn);
            char neighbor3 = io_board.getCell(io_row, (char)minusColumn);
            char neighbor4 = io_board.getCell(io_row, (char)plusColumn);
            char neighbor5 = io_board.getCell((char)plusLine, (char)minusColumn);
            char neighbor6 = io_board.getCell((char)plusLine, io_column);
            char neighbor7 = io_board.getCell((char)plusLine, (char)plusColumn);

            // insert adjacent cells to array 
            char[] neighbers =
                            {
                                neighbor0, neighbor1, neighbor2, neighbor3, neighbor4, neighbor5, neighbor6, 
                                neighbor7
                            };

            // check for each adjacent cell whether need to change or not
            for (int i = 0; i < 8; i++)
            {
                this.numberOfCellsNeededToChange = 1;
                if (neighbers[i] == opponentTool)
                {
                    if (i == 0)
                    {
                        bool check = this.checkedValidMoveContinue(io_board, (char)minusLine, (char)minusColumn, i);
                        if (check)
                        {
                            this.changeTheSequence = true;
                        }
                    }
                    else if (i == 1)
                    {
                        bool check = this.checkedValidMoveContinue(io_board, (char)minusLine, io_column, i);
                        if (check)
                        {
                            this.changeTheSequence = true;
                        }
                    }
                    else if (i == 2)
                    {
                        bool check = this.checkedValidMoveContinue(io_board, (char)minusLine, (char)plusColumn, i);
                        if (check)
                        {
                            this.changeTheSequence = true;
                        }
                    }
                    else if (i == 3)
                    {
                        bool check = this.checkedValidMoveContinue(io_board, io_row, (char)minusColumn, i);
                        if (check)
                        {
                            this.changeTheSequence = true;
                        }
                    }
                    else if (i == 4)
                    {
                        bool check = this.checkedValidMoveContinue(io_board, io_row, (char)plusColumn, i);
                        if (check)
                        {
                            this.changeTheSequence = true;
                        }
                    }
                    else if (i == 5)
                    {
                        bool check = this.checkedValidMoveContinue(io_board, (char)plusLine, (char)minusColumn, i);
                        if (check)
                        {
                            this.changeTheSequence = true;
                        }
                    }
                    else if (i == 6)
                    {
                        bool check = this.checkedValidMoveContinue(io_board, (char)plusLine, io_column, i);
                        if (check)
                        {
                            this.changeTheSequence = true;
                        }
                    }
                    else if (i == 7)
                    {
                        bool check = this.checkedValidMoveContinue(io_board, (char)plusLine, (char)plusColumn, i);
                        if (check)
                        {
                            this.changeTheSequence = true;
                        }
                    }
                }

            }

            if (this.changeTheSequence)
            {
                return true;
            }

            return false;
        }

        // change sign for each cell in the sequence 

        private void drawAllChangeCells(Board io_board, char io_row, char io_column, int io_neighbor, int io_numTochange)
        {
            int rowToChange = io_row;
            int columnToChange = io_column;
            char playerTool;

            if (s_PlayerTurn == eTurn.Player1)
            {
                playerTool = 'O';
            }
            else
            {
                playerTool = 'X';
            }



            if (io_neighbor == 0)
            {
                for (int j = 0; j < io_numTochange; j++)
                {
                    io_board.setCell(playerTool, (char)rowToChange, (char)columnToChange);
                    rowToChange++;
                    columnToChange++;
                }
            }

            if (io_neighbor == 1)
            {
                for (int j = 0; j < io_numTochange; j++)
                {
                    io_board.setCell(playerTool, (char)rowToChange, (char)columnToChange);
                    rowToChange++;
                }
            }

            if (io_neighbor == 2)
            {
                for (int j = 0; j < io_numTochange; j++)
                {
                    io_board.setCell(playerTool, (char)rowToChange, (char)columnToChange);
                    rowToChange++;
                    columnToChange--;
                }
            }

            if (io_neighbor == 3)
            {
                for (int j = 0; j < io_numTochange; j++)
                {
                    io_board.setCell(playerTool, (char)rowToChange, (char)columnToChange);
                    columnToChange++;
                }
            }

            if (io_neighbor == 4)
            {
                for (int j = 0; j < io_numTochange; j++)
                {
                    io_board.setCell(playerTool, (char)rowToChange, (char)columnToChange);
                    columnToChange--;
                }
            }

            if (io_neighbor == 5)
            {
                for (int j = 0; j < io_numTochange; j++)
                {
                    io_board.setCell(playerTool, (char)rowToChange, (char)columnToChange);
                    rowToChange--;
                    columnToChange++;
                }
            }

            if (io_neighbor == 6)
            {
                for (int j = 0; j < io_numTochange; j++)
                {
                    io_board.setCell(playerTool, (char)rowToChange, (char)columnToChange);
                    rowToChange--;
                }
            }

            if (io_neighbor == 7)
            {
                for (int j = 0; j < io_numTochange; j++)
                {
                    io_board.setCell(playerTool, (char)rowToChange, (char)columnToChange);
                    rowToChange--;
                    columnToChange--;
                }
            }

        }

        // if adjacent cell with opposite sign, check the sequence in a specific direction
        // if found a legal move use drawAllChangeCells method to change the sequence

        private bool checkedValidMoveContinue(Board board, char row, char column, int io_neighbor)
        {
            int minusLine = row - 1;
            int minusColumn = column - 1;
            int plusLine = row + 1;
            int plusColumn = column + 1;
            char opponentTool;
            char playerTool;


            if (s_PlayerTurn == eTurn.Player1)
            {
                opponentTool = 'X';
                playerTool = 'O';
            }
            else
            {
                opponentTool = 'O';
                playerTool = 'X';
            }
           
            
                if (io_neighbor == 0)
                {
                    char neighbor0 = board.getCell((char)minusLine, (char)minusColumn);
                    if (neighbor0 == opponentTool)
                    {
                        numberOfCellsNeededToChange++;

                        checkedValidMoveContinue(board, (char)minusLine, (char)minusColumn, io_neighbor);
                    }
                    else if (neighbor0 == playerTool)
                    {
                        CellsNeededToChange.Add(row);
                        CellsNeededToChange.Add(column);
                        numberOfCellsNeededToChangeArray.Add(numberOfCellsNeededToChange);
                        numberOfCellsNeededToChange = 1;
                        foundLegalMoveNeighbours.Add(0);

                        return true;
                    }
                }

                if (io_neighbor == 1)
                {
                    char neighbor0 = board.getCell((char)minusLine, column);
                    if (neighbor0 == opponentTool)
                    {
                        numberOfCellsNeededToChange++;
                        checkedValidMoveContinue(board, (char)minusLine, column, io_neighbor);
                    }
                    else if (neighbor0 == playerTool)
                    {
                        CellsNeededToChange.Add(row);
                        CellsNeededToChange.Add(column);
                        numberOfCellsNeededToChangeArray.Add(numberOfCellsNeededToChange);
                        numberOfCellsNeededToChange = 1;
                        foundLegalMoveNeighbours.Add(1);

                        return true;
                    }
                }

                if (io_neighbor == 2)
                {
                    char neighbor0 = board.getCell((char)minusLine, (char)plusColumn);
                    if (neighbor0 == opponentTool)
                    {
                        numberOfCellsNeededToChange++;
                        checkedValidMoveContinue(board, (char)minusLine, (char)plusColumn, io_neighbor);
                    }
                    else if (neighbor0 == playerTool)
                    {
                        CellsNeededToChange.Add(row);
                        CellsNeededToChange.Add(column);
                        numberOfCellsNeededToChangeArray.Add(numberOfCellsNeededToChange);
                        numberOfCellsNeededToChange = 1;
                        foundLegalMoveNeighbours.Add(2);

                        return true;
                    }
                }

                if (io_neighbor == 3)
                {
                    char neighbor0 = board.getCell(row, (char)minusColumn);
                    if (neighbor0 == opponentTool)
                    {
                        numberOfCellsNeededToChange++;
                        checkedValidMoveContinue(board, row, (char)minusColumn, io_neighbor);
                    }
                    else if (neighbor0 == playerTool)
                    {
                        CellsNeededToChange.Add(row);
                        CellsNeededToChange.Add(column);
                        numberOfCellsNeededToChangeArray.Add(numberOfCellsNeededToChange);
                        numberOfCellsNeededToChange = 1;
                        foundLegalMoveNeighbours.Add(3);

                        return true;
                    }
                }

                if (io_neighbor == 4)
                {
                    char neighbor0 = board.getCell(row, (char)plusColumn);
                    if (neighbor0 == opponentTool)
                    {
                        numberOfCellsNeededToChange++;
                        checkedValidMoveContinue(board, row, (char)plusColumn, io_neighbor);
                    }
                    else if (neighbor0 == playerTool)
                    {
                        CellsNeededToChange.Add(row);
                        CellsNeededToChange.Add(column);
                        numberOfCellsNeededToChangeArray.Add(numberOfCellsNeededToChange);
                        numberOfCellsNeededToChange = 1;
                        foundLegalMoveNeighbours.Add(4);

                        return true;
                    }
                }

                if (io_neighbor == 5)
                {
                    char neighbor0 = board.getCell((char)plusLine, (char)minusColumn);
                    if (neighbor0 == opponentTool)
                    {
                        numberOfCellsNeededToChange++;
                        checkedValidMoveContinue(board, (char)plusLine, (char)minusColumn, io_neighbor);
                    }
                    else if (neighbor0 == playerTool)
                    {
                        CellsNeededToChange.Add(row);
                        CellsNeededToChange.Add(column);
                        numberOfCellsNeededToChangeArray.Add(numberOfCellsNeededToChange);
                        numberOfCellsNeededToChange = 1;
                        foundLegalMoveNeighbours.Add(5);

                        return true;
                    }
                }

                if (io_neighbor == 6)
                {
                    char neighbor0 = board.getCell((char)plusLine, column);
                    if (neighbor0 == opponentTool)
                    {
                        numberOfCellsNeededToChange++;
                        checkedValidMoveContinue(board, (char)plusLine, column, io_neighbor);
                    }
                    else if (neighbor0 == playerTool)
                    {
                        CellsNeededToChange.Add(row);
                        CellsNeededToChange.Add(column);
                        numberOfCellsNeededToChangeArray.Add(numberOfCellsNeededToChange);
                        numberOfCellsNeededToChange = 1;
                        foundLegalMoveNeighbours.Add(6);

                        return true;
                    }
                }

                if (io_neighbor == 7)
                {
                    char neighbor0 = board.getCell((char)plusLine, (char)plusColumn);
                    if (neighbor0 == opponentTool)
                    {
                        numberOfCellsNeededToChange++;
                        checkedValidMoveContinue(board, (char)plusLine, (char)plusColumn, io_neighbor);
                    }
                    else if (neighbor0 == playerTool)
                    {
                        CellsNeededToChange.Add(row);
                        CellsNeededToChange.Add(column);
                        numberOfCellsNeededToChangeArray.Add(numberOfCellsNeededToChange);
                        numberOfCellsNeededToChange = 1;
                        foundLegalMoveNeighbours.Add(7);

                        return true;
                    }
                }

            if (foundLegalMoveNeighbours.Count != 0)
            {
                return true;
            }

            return false;
        }
    }
}