using System;
using System.Collections.Generic;

namespace connect_four
{
    // AI responsible for selecting moves (i.e., picking columns)
    public class AI2 : AI
    {
        /*
         * Selects the column to place a piece in
         * 
         * Inputs:
         *  {Board} board - Represents the state of the game
         *  {int} team - the team for this AI
         * 
         * Output:
         *  {int} - An integer within the domain: [0, Consts.NUM_COLS-1]
         * 
         * Note: Choosing a column that is full will result in disqualification
         */
        public int selectColumn(Board board, int team)
        {
            //Example: pick random valid column
            List<int> validColumns = new List<int>();

            for (int c = 0; c < (int)Consts.NUM_COLS; c++)
            {
                if (board.columnFull(c) == false)
                {
                    validColumns.Add(c); //Add column if valid (i.e. not full)
                }
            }

            Random random = new Random();
            int randomNumber = random.Next(0,validColumns.Count);

            return validColumns.ToArray()[randomNumber];
        }
    }
}
