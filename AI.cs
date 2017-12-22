namespace connect_four
{
    /*
     * The interface for an AI player. They receive input in the form
     * of the board state, and are solely responsible for picking
     * a column as a move.
     */
    public interface AI
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
        int selectColumn(Board board, int team);
    }
}
