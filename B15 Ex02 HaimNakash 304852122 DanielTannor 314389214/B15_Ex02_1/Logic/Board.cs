// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Board.cs" company="">
//   
// </copyright>
// <summary>
//   The board.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace B15_Ex02_1.Logic
{
    /// <summary>
    /// The board.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// The cells.
        /// </summary>
        private char[,] cells;

        /// <summary>
        /// The m_board size.
        /// </summary>
        private int m_boardSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="Board"/> class.
        /// </summary>
        /// <param name="io_boardSize">
        /// The board size.
        /// </param>
        public Board(int io_boardSize)
        {
            cells = new char[io_boardSize, io_boardSize];
            m_boardSize = io_boardSize;
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public int Size
        {
            get
            {
                return m_boardSize;
            }
        }

        /// <summary>
        /// The get cell.
        /// </summary>
        /// <param name="io_num">
        /// The num.
        /// </param>
        /// <param name="io_letter">
        /// The letter.
        /// </param>
        /// <returns>
        /// The <see cref="char"/>.
        /// </returns>
        public char getCell(char io_num, char io_letter)
        {
            int letterToNumber = io_letter - 65;
            int numTonumber = io_num - 49;
            if (m_boardSize == 8)
            {
                if (numTonumber < 0 || numTonumber > 7 || letterToNumber < 0 || letterToNumber > 7)
                {
                    return 'E';
                }
            }
            else
            {
                if (numTonumber < 0 || numTonumber > 5 || letterToNumber < 0 || letterToNumber > 5)
                {
                    return 'E';
                }
            }

            return cells[numTonumber, letterToNumber];
        }

        /// <summary>
        /// The get cell.
        /// </summary>
        /// <param name="io_num1">
        /// The num 1.
        /// </param>
        /// <param name="num2">
        /// The num 2.
        /// </param>
        /// <returns>
        /// The <see cref="char"/>.
        /// </returns>
        public char getCell(int io_num1, int io_num2)
        {
            return cells[io_num1, io_num2];
        }

        /// <summary>
        /// The set cell.
        /// </summary>
        /// <param name="io_sign">
        /// The c.
        /// </param>
        /// <param name="io_num">
        /// The num.
        /// </param>
        /// <param name="io_letter">
        /// The letter.
        /// </param>
        public void setCell(char io_sign, char io_num, char io_letter)
        {
            int letterToNumber = io_letter - 65;
            int numTonumber = io_num - 49;
            cells[numTonumber, letterToNumber] = io_sign;
        }
    }
}