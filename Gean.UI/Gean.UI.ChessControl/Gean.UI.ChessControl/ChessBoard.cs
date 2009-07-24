using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Gean.Wrapper.Chess;

namespace Gean.UI.ChessControl
{
    public class ChessBoard : Control
    {
        public ChessBoard()
        {
            float f = 50;

            this.ChessGame = new ChessGame();
            this.DoubleBuffered = true;
            this.BackColor = Color.NavajoWhite;
            this.ChessGame.GetRectangles(f, out this._whiteRectangle, out this._blackRectangle);
        }

        public ChessGame ChessGame { get; set; }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            this.CreatBoard(pevent.Graphics);
        }

        RectangleF[] _whiteRectangle;
        RectangleF[] _blackRectangle;

        private void CreatBoard(Graphics g)
        {
            int offset = 20;
            int wh;
            int w = this.Width;
            int h = this.Height;

            if (w > h)
                wh = h;
            else
                wh = w;

            wh = wh - offset * 2;

            foreach (RectangleF rect in _whiteRectangle)
                g.FillRectangle(Brushes.WhiteSmoke, rect);
            foreach (RectangleF rect in _blackRectangle)
                g.FillRectangle(Brushes.DimGray, rect);

        }
    }
}
