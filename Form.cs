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
            // Use Buffering to avoid flickering
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);

            innerX = 20;
            innerY = 20;
            innerW = Consts.WIDTH - 40;
            innerH = Consts.HEIGHT - 40;
            InitializeComponent();
            this.Width = Consts.WIDTH + 20;
            this.Height = Consts.HEIGHT + 40;
            board = new Board(this, new AI2(), new AI2());
        }

        /*
         * Draws the outlining for the board and invoke drawPieces(g)
         */
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Blue);

            for (int r = 0; r <= Consts.NUM_ROWS; r++)
            {
                g.DrawLine(pen, innerX, innerY + r * innerH / Consts.NUM_ROWS, innerX + innerW, 
                    innerY + r * innerH / Consts.NUM_ROWS);
            }
            for (int c = 0; c <= Consts.NUM_COLS; c++)
            {
                g.DrawLine(pen, innerX + c * innerW / Consts.NUM_COLS, 
                    innerY, innerX + c * innerW / Consts.NUM_COLS, innerY + innerH);
            }
            drawPieces(g);
        }


        /*
         * Draws all pieces on the board 
         */
        private void drawPieces(Graphics g)
        {
            for (int r = 0; r < Consts.NUM_ROWS; r++)
            {
                for (int c = 0; c < Consts.NUM_COLS; c++)
                {
                    switch (board.getPiece(r, c))
                    {
                        case (int)Consts.TEAM.YELLOW:
                            g.FillEllipse(new SolidBrush(Color.Yellow), innerX + 2 + c * innerW / Consts.NUM_COLS,
                               innerY + innerH + 2 - ((r+1) * innerH / Consts.NUM_ROWS),
                               innerW / Consts.NUM_COLS - 4, innerH / Consts.NUM_ROWS - 4);
                            break;
                        case (int)Consts.TEAM.RED:
                            g.FillEllipse(new SolidBrush(Color.Red), innerX + 2 + c * innerW / Consts.NUM_COLS,
                               innerY + innerH + 2 - ((r+1) * innerH / Consts.NUM_ROWS),
                               innerW / Consts.NUM_COLS - 4, innerH / Consts.NUM_ROWS - 4);
                            break;
                    }
                }
            }
        }

    }
}