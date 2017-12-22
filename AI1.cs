using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect_four
{
    public class AI1 : AI
    {
        public int selectColumn(Board board, int team)
        {
            for (int c = 0; c < (int)consts.NUM_COLS; c++)
            {
                if (board.columnFull(c) == false)
                {
                    return c;
                }
            }
            return 0; //should never happen
        }
    }
}
