using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public struct ChessPoint
    {
        public static readonly ChessPoint Empty = new ChessPoint(0, 0);

        public int X
        {
            get { return this._x; }
        }
        private int _x;

        public int Y
        {
            get { return this._y; }
        }
        private int _y;

        public char CharX
        {
            get { return this._charX; }
        }
        private char _charX;

        public ChessPoint(int x, int y)
        {
            this._x = 0;
            this._y = 0;
            this._charX = '*';
            this._x = x;
            this._y = y;
            this._charX = Utility.IntToChar(x);
        }

        public override bool Equals(object obj)
        {
            ChessPoint point = (ChessPoint)obj;
            if (this.X != point.X) return false;
            if (this.Y != point.Y) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return ((this.X.GetHashCode() + this.Y.GetHashCode() + this.CharX.GetHashCode()) * 3);
        }
        public override string ToString()
        {
            return string.Format("{0}{1}", this.CharX, this.Y);
        }
    }
}
