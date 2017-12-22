using System;

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

        /*
         * Intializes empty Board
         */
        public Board(AI p1, AI p2)
        {
            this.player1 = p1;
            this.player2 = p2;
            pieces = new int[consts.NUM_ROWS, consts.NUM_COLS];
            nextRows = new int[consts.NUM_COLS]; //default int value 0
            pieceCount = 0;
            playerTurn = (int)consts.TEAM.YELLOW;
            gameOver = false;

            while (gameOver == false)
            {
                processTurn();
                //System.Threading.Thread.Sleep(consts.SLEEP_TIME);
            }
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
            if (col >= consts.NUM_COLS)
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
                pieces[nextRows[col]++, col] = playerTurn;
                checkWin(); // check win condition
                pieceCount++; //increment number of pieces by 1
                swapPlayerTurn();

                // check draw condition
                if (pieceCount == consts.NUM_ROWS * consts.NUM_COLS)
                {
                    declareWinner((int)consts.TEAM.NONE);
                }
            }
        }

        /*
         * Returns an AI's selected column based on who's turn it is
         */
        private int getColumn()
        {
            if (playerTurn == (int)consts.TEAM.YELLOW)
            {
                return player1.selectColumn(this, (int)consts.TEAM.YELLOW);
            }
            else
            {
                return player2.selectColumn(this, (int)consts.TEAM.RED);
            }
        }

        /*
         * Passes the turn over to the other player
         */
        private void swapPlayerTurn()
        {
            if (playerTurn == (int)consts.TEAM.YELLOW)
            {
                playerTurn = (int)consts.TEAM.RED;
            }
            else
            {
                playerTurn = (int)consts.TEAM.YELLOW;
            }
        }

        /*
         * Declares the new winner
         */
        private void declareWinner(int winner)
        {
            switch (winner)
            {
                case (int)consts.TEAM.NONE:
                    Console.WriteLine("The game ended in a Draw.");
                    break;
                case (int)consts.TEAM.YELLOW:
                    Console.WriteLine("The Yellow Player has Won.");
                    break;
                case (int)consts.TEAM.RED:
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
            if (row >= (int)consts.NUM_ROWS || col >= (int)consts.NUM_COLS)
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
            if (col >= (int)consts.NUM_COLS)
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
            if (col >= (int)consts.NUM_COLS)
            {
                return false; // out of bounds
            }
            return nextRows[col] >= consts.NUM_ROWS;
        }

        /*
         * Check if the last placed piece causes a win
         */
        private bool checkWin()
        {
            return false;
        }
    }
}