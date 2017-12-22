using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace connect_four
{
    /*
     * Represents the state of a Connect-Four game
     */
    public class Board
    {
        private int[,] pieces;
        private int[] nextRows;
        private int pieceCount;
        private int playerTurn;
        private AI player1; //yellow player
        private AI player2; //red player
        private bool gameOver;
        private Form form;

        /*
         * Intializes empty Board
         */
        public Board(Form f, AI p1, AI p2)
        {
            this.form = f;
            this.player1 = p1;
            this.player2 = p2;
            pieces = new int[Consts.NUM_ROWS, Consts.NUM_COLS];
            nextRows = new int[Consts.NUM_COLS]; //default int value 0
            pieceCount = 0;
            playerTurn = (int)Consts.TEAM.YELLOW;
            gameOver = false;

            Task.Factory.StartNew(async () =>
            {
                while (gameOver == false)
                {
                    await Task.Delay(Consts.SLEEP_TIME);
                    processTurn();
                    f.Invalidate();
                }
            });
        }

        /*
         * Processes a turn. 
         * The column for the new piece is determined by getColumn()
         * If the chosen column is already full, the player is disqualified.
         * Check for a win or draw, then swaps player turns
         */
        private void processTurn()
        {
            int col = getColumn();
            Console.WriteLine("Column " + col + " selected.");
            if (col >= Consts.NUM_COLS)
            {
                Console.WriteLine("Invalid Move - Column Out of Bounds.");
                swapPlayerTurn();
                declareWinner(playerTurn);
            }
            if (columnFull(col))
            {
                Console.WriteLine("Invalid Move - Column Full.");
                swapPlayerTurn();
                declareWinner(playerTurn);
            }
            else
            {
                pieces[nextRows[col], col] = playerTurn;
                if (checkWin(nextRows[col], col)) //returns if player has won
                {
                    declareWinner(playerTurn);
                }
                nextRows[col]++; //increment nextRow indicator
                pieceCount++; //increment number of pieces by 1
                swapPlayerTurn();

                // check draw condition
                if (pieceCount == Consts.NUM_ROWS * Consts.NUM_COLS)
                {
                    declareWinner((int)Consts.TEAM.NONE);
                }
            }
        }

        /*
         * Returns an AI's selected column based on who's turn it is
         */
        private int getColumn()
        {
            if (playerTurn == (int)Consts.TEAM.YELLOW)
            {
                return player1.selectColumn(this, (int)Consts.TEAM.YELLOW);
            }
            else
            {
                return player2.selectColumn(this, (int)Consts.TEAM.RED);
            }
        }

        /*
         * Passes the turn over to the other player
         */
        private void swapPlayerTurn()
        {
            if (playerTurn == (int)Consts.TEAM.YELLOW)
            {
                playerTurn = (int)Consts.TEAM.RED;
            }
            else
            {
                playerTurn = (int)Consts.TEAM.YELLOW;
            }
        }

        /*
         * Declares the new winner
         */
        private void declareWinner(int winner)
        {
            switch (winner)
            {
                case (int)Consts.TEAM.NONE:
                    Console.WriteLine("The game ended in a Draw.");
                    break;
                case (int)Consts.TEAM.YELLOW:
                    Console.WriteLine("The Yellow Player has Won.");
                    break;
                case (int)Consts.TEAM.RED:
                    Console.WriteLine("The Red Player has Won.");
                    break;
            }
            gameOver = true;
        }

        /*
         * Returns the piece at a given row and column
         * Returns -1 if index out of bounds
         */
        public int getPiece(int row, int col)
        {
            if (row >= (int)Consts.NUM_ROWS || col >= (int)Consts.NUM_COLS)
            {
                return -1; // out of bounds
            }
            return pieces[row, col];
        }

        /*
         * Returns the pieces matrix associated with the board
         */
        public int[,] getPieces()
        {
            return pieces;
        }

        /*
         * Returns the next available row for a piece, given a column
         * Returns -1 if index out of bounds
         */
        public int getNextRow(int col)
        {
            if (col >= (int)Consts.NUM_COLS)
            {
                return -1; // out of bounds
            }
            return nextRows[col];
        }

        /*
         * Returns whether the given column is already full
         * Returns false if index out of bounds
         */
        public bool columnFull(int col)
        {
            if (col >= (int)Consts.NUM_COLS)
            {
                return false; // out of bounds
            }
            return nextRows[col] >= Consts.NUM_ROWS;
        }

        /*
         * Check if the last placed piece causes a win
         */
        private bool checkWin(int row, int col)
        {
            if (checkWinHelper(row, col, -1, 0 , 1) >= 4) //Check downwards |
            {
                return true;
            }
            else if ((checkWinHelper(row, col, 0, -1, 1) + checkWinHelper(row, col, 0, 1, 0)) >= 4) //check horz -
            {
                return true;
            }
            else if ((checkWinHelper(row, col, 1, 1, 1) + checkWinHelper(row, col, -1, -1, 0)) >= 4) //check /
            {
                return true;
            }
            else if ((checkWinHelper(row, col, -1, 1, 1) + checkWinHelper(row, col, 1, -1, 0)) >= 4) //check \
            {
                return true;
            }
            return false;
        }

        /*
         * Returns how many pieces are in a row in a given direction
         */ 
        private int checkWinHelper(int row, int col, int rowDir, int colDir, int count)
        {
            row = row + rowDir;
            col = col + colDir;

            if (row < 0 || row >= (int)Consts.NUM_ROWS || col < 0 | col >= (int)Consts.NUM_COLS)
            {
                return count; //out of bounds
            }
            else if (pieces[row,col] != playerTurn) //doesn't continue
            {
                return count;
            }
            else //continues
            {
                count++;
                if (count == 4) //reached 4
                {
                    return 4;
                }
                else //not yet 4
                {
                    return checkWinHelper(row, col, rowDir, colDir, count);
                }
            }
        }
    }
}