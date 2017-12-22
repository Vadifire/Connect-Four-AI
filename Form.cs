using System;
using System.Drawing;
using System.Windows.Forms;

namespace connect_four
{
    public partial class Form : System.Windows.Forms.Form
    {
        private Board board;
        private int innerX, innerY, innerW, innerH;

        public Form()
        {
            innerX = 20;
            innerY = 20;
            innerW = consts.WIDTH - 40;
            innerH = consts.HEIGHT - 40;
            InitializeComponent();
            this.Width = consts.WIDTH + 20;
            this.Height = consts.HEIGHT + 40;
            board = new Board(new AI1(), new AI1());
        }

        /*
         * Draws the outlining for the board and invoke drawPieces(g)
         */
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen pen = new Pen(Color.Blue);

            for (int r = 0; r <= consts.NUM_ROWS; r++)
            {
                g.DrawLine(pen, innerX, innerY + r * innerH / consts.NUM_ROWS, innerX + innerW, innerY + r * innerH / consts.NUM_ROWS);
            }
            for (int c = 0; c <= consts.NUM_COLS; c++)
            {
                g.DrawLine(pen, innerX + c * innerW / consts.NUM_COLS, innerY, innerX + c * innerW / consts.NUM_COLS, innerY + innerH);
            }
            drawPieces(g);
        }


        /*
         * Draws all pieces on the board 
         */
        private void drawPieces(Graphics g)
        {
            for (int r = 0; r < consts.NUM_ROWS; r++)
            {
                for (int c = 0; c < consts.NUM_COLS; c++)
                {
                    switch (board.getPiece(r, c))
                    {
                        case (int)consts.TEAM.YELLOW:
                            g.FillEllipse(new SolidBrush(Color.Yellow), innerX + 2 + c * innerW / consts.NUM_COLS, innerY + 2 + r * innerH / consts.NUM_ROWS,
                               innerW / consts.NUM_COLS - 4, innerH / consts.NUM_ROWS - 4);
                            break;
                        case (int)consts.TEAM.RED:
                            g.FillEllipse(new SolidBrush(Color.Red), innerX + 2 + c * innerW / consts.NUM_COLS, innerY + 2 + r * innerH / consts.NUM_ROWS,
                               innerW / consts.NUM_COLS - 4, innerH / consts.NUM_ROWS - 4);
                            break;
                        default:
                           /*g.FillEllipse(new SolidBrush(Color.Black), innerX + 2 + c * innerW / consts.NUM_COLS, innerY + 2 + r * innerH / consts.NUM_ROWS,
                                innerW / consts.NUM_COLS - 4, innerH / consts.NUM_ROWS - 4);*/
                            break;
                    }
                }
            }
        }

    }
}