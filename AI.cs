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
         * Calculate the column to put a piece in
         * 
         * The board represents the game state
         * The team variable represents which team the AI is on
         */
        int selectColumn(Board board, int team);
    }
}
