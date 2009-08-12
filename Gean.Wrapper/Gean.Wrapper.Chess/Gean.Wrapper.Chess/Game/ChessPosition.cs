using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 一个描述棋盘位置的类型
    /// </summary>
    public class ChessPosition
    {
        public static readonly ChessPosition Empty = null;

        public static ChessPosition Parse(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException();
            if (value.Length != 2) 
                throw new ArgumentOutOfRangeException(value);

            char horizontal = value[0];
            int vertical = int.Parse(value[1].ToString());

            return new ChessPosition(horizontal, vertical);
        }

        private char _horizontal;
        private int  _vertical;
        private int  _x;
        private int  _y;

        private Regex _hRegex = new Regex("[A-Ha-h]");
        private Regex _vRegex = new Regex("[1-8]");

        /// <summary>
        /// 一个描述棋盘位置的类型
        /// </summary>
        /// <param name="horizontal">横坐标</param>
        /// <param name="vertical">纵坐标</param>
        public ChessPosition(char horizontal, int vertical)
        {
            this.Horizontal = horizontal;
            this.Vertical = vertical;
        }

        /// <summary>
        /// 一个描述棋盘位置的类型
        /// </summary>
        /// <param name="X">横坐标</param>
        /// <param name="Y">纵坐标</param>
        public ChessPosition(int X, int Y)
        {
            this.X = X - 1;
            this.Y = Y - 1;
        }
        
        /// <summary>
        /// 横坐标
        /// </summary>
        public char Horizontal
        {
            get { return _horizontal; }
            set
            {
                if (!_hRegex.Match(value.ToString()).Success)
                    throw new ArgumentException(ExceptionString.ex_illegalHorizontalValue, "horizontal");
                _horizontal = value;
                _x = Utility.CharToInt(value) - 1;

            }
        }
        
        /// <summary>
        /// 纵坐标
        /// </summary>
        public int Vertical
        {
            get { return _vertical; }
            set
            {
                if (!_vRegex.Match(value.ToString()).Success)
                    throw new ArgumentException(ExceptionString.ex_illegalVerticalValue, "vertical");
                _vertical = value;
                _y = (value - 1);
            }
        }

        public int X
        {
            get { return _x; }
            set
            {
                if (value >= 0 && value <= 7)
                {
                    _x = value;
                    _horizontal = Utility.IntToChar(value + 1);
                }
                else
                    throw new ArgumentException(ExceptionString.ex_illegalXCoordinateValue, "X");
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                if (value >= 0 && value <= 7)
                {
                    _y = value;
                    _vertical = (value + 1);
                }
                else
                    throw new ArgumentException(ExceptionString.ex_illegalYCoordinateValue, "Y");
            }
        }

        #region override

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            ChessPosition point = (ChessPosition)obj;
            if (this.X != point.X) return false;
            if (this.Y != point.Y) return false;
            if (this.Vertical != point.Vertical) return false;
            if (this.Horizontal != point.Horizontal) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked
                (3 * (
                this._horizontal.GetHashCode() +
                this._vertical.GetHashCode() +
                this._hRegex.GetHashCode() +
                this._vRegex.GetHashCode() +
                this._x.GetHashCode() +
                this._y.GetHashCode()
                ));
        }
        public override string ToString()
        {
            return string.Format("{0}{1}", this.Horizontal, this.Vertical);
        }

        #endregion
    }
}
