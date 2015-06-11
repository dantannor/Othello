// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UI.cs" company="">
//   
// </copyright>
// <summary>
//   The view.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace B15_Ex02_1.UI
{
    using System;
    using System.Threading;

    using B15_Ex02_1.Logic;

    // The view. I/O to and from the user.
    public class View
    {
        /*
         * Scans player name
         */

        /*
         * Scans the player names
         */
        public static string ScanPlayerName()
        {
            Console.WriteLine();
            Console.WriteLine("Please enter player name:");

            string playerName = Console.ReadLine();
            Ex02.ConsoleUtils.Screen.Clear();
            return playerName;
        }

        /*
         * Prints invalid input messages to the user
         */
        public static void PrintInvalidInput(string i_InvalidInputMsg)
        {
            Console.WriteLine(i_InvalidInputMsg);

            // The sleep thread here simply allows the user to see the message instead of clearing right away.
            Thread.Sleep(1000);
            Ex02.ConsoleUtils.Screen.Clear();
        }

        /*
         * Asks who the player wants to play against: PC/Player
         */
        public static string AskPlayerType()
        {
            Console.WriteLine();
            Console.WriteLine(@"Choose your opponent:

1. Player
2. PC");

            string playerType = Console.ReadKey().KeyChar.ToString();
            Ex02.ConsoleUtils.Screen.Clear();
            return playerType;
        }

        /*
         * Scans the player's move
         */
        public static string ScanPlayerMove(string io_PlayerName)
        {
            Console.WriteLine(
@"{0}'s Move:",
 io_PlayerName);
            return Console.ReadLine();
        }

        /*
         * Scans board size
         */

        /*
         * Asks for the board size
         */
        public static string AskBoardSize()
        {
            Console.WriteLine();
            Console.WriteLine(
@"Choose board size:

1. 6x6
2. 8x8");

            string boardSize = Console.ReadKey().KeyChar.ToString();
            Ex02.ConsoleUtils.Screen.Clear();
            return boardSize;
        }

        /*
         * Draws the board
         */
        public static void DrawBoard(Board io_Board)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            int size = io_Board.Size;
            char[] firtRowBoardSizeEight = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            char[] firtRowBoardSizeSix = { 'A', 'B', 'C', 'D', 'E', 'F' };
            char[] firtColBoardSizeEight = { '1', '2', '3', '4', '5', '6', '7', '8' };

            string LineEight = " =================================";
            string LineSix = " =========================";

            if (size == 8)
            {
                Console.Write("   {0}   ", firtRowBoardSizeEight[0]);
                for (int i = 1; i < size; i++)
                {
                    Console.Write("{0}   ", firtRowBoardSizeEight[i]);
                }

                Console.WriteLine();
                Console.Write(LineEight);

                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine();
                    Console.Write(
                        "{0}| {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} |", 
                        firtColBoardSizeEight[i], 
                        io_Board.getCell(i, 0), 
                        io_Board.getCell(i, 1), 
                        io_Board.getCell(i, 2), 
                        io_Board.getCell(i, 3), 
                        io_Board.getCell(i, 4), 
                        io_Board.getCell(i, 5), 
                        io_Board.getCell(i, 6), 
                        io_Board.getCell(i, 7));
                    Console.WriteLine();
                    Console.Write(LineEight);
                }
            }
            else
            {
                if (size == 6)
                {
                    Console.Write("   {0}   ", firtRowBoardSizeSix[0]);
                    for (int i = 1; i < size; i++)
                    {
                        Console.Write("{0}   ", firtRowBoardSizeSix[i]);
                    }

                    Console.WriteLine();
                    Console.Write(LineSix);

                    for (int i = 0; i < size; i++)
                    {
                        Console.WriteLine();
                        Console.Write(
                            "{0}| {1} | {2} | {3} | {4} | {5} | {6} |", 
                            firtColBoardSizeEight[i], 
                            io_Board.getCell(i, 0), 
                            io_Board.getCell(i, 1), 
                            io_Board.getCell(i, 2), 
                            io_Board.getCell(i, 3), 
                            io_Board.getCell(i, 4), 
                            io_Board.getCell(i, 5));
                        Console.WriteLine();
                        Console.Write(LineSix);
                    }
                }
            }

            Console.WriteLine();
        }

        /*
         * Prints end of game: the winner, and #points for each player
         */
        public static void PrintGameOver(int i_Player1Points, int i_Player2Points, string i_Victor, string i_Player2)
        {
            Console.WriteLine(
@"The game has finished!

{0} is the victor with {1} points!
And {2} with {3} points!
", 
 i_Victor,
 i_Player1Points,
 i_Player2,
 i_Player2Points);
            Console.WriteLine();
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            Ex02.ConsoleUtils.Screen.Clear();
        }

        /*
         * Lets players know there's a turn switch
         */
        public static void DisplayTurnSwitch(string i_NoMovesPlayer)
        {
            Console.WriteLine(@"{0} has no moves, sorry! Turn transfer!", i_NoMovesPlayer);
            Console.WriteLine();
        }

        /*
         * Ask if the players want another round
         */
        public static string AskPlayAgain()
        {
            Console.WriteLine(@"Would you like another round?
1. Yes
2. No");
            string anotherRound = Console.ReadKey().KeyChar.ToString();
            Ex02.ConsoleUtils.Screen.Clear();
            return anotherRound;
        }

        /*
         * Welcome screen
         */
        public static void Welcome()
        {
            Console.WriteLine(
@"



            =========================================================




                                Welcome to Reversee!


                              Press any key to continue...

            (Choose Q during your move if you want to exit! Have fun!) 



            ==========================================================");
           Console.ReadKey();
           Ex02.ConsoleUtils.Screen.Clear();
        }
    }
}