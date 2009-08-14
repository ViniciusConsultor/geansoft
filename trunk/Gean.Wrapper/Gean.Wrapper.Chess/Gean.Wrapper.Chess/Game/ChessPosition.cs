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

        /// <summary>
        /// 当前位置的棋盘横坐标(a-h)
        /// </summary>
        private char _horizontal;
        /// <summary>
        /// 当前位置的棋盘纵坐标(1-8)
        /// </summary>
        private int _vertical;
        /// <summary>
        /// 当前位置的计算机X坐标(0-7)
        /// </summary>
        private int _x;
        /// <summary>
        /// 当前位置的计算机Y坐标(0-7)
        /// </summary>
        private int _y;

        /// <summary>
        /// 一个描述棋盘位置的类型
        /// </summary>
        /// <param name="horizontal">横坐标(a-h)</param>
        /// <param name="vertical">纵坐标(1-8)</param>
        public ChessPosition(char horizontal, int vertical)
        {
            this.Horizontal = horizontal;
            this.Vertical = vertical;
        }

        /// <summary>
        /// 一个描述棋盘位置的类型
        /// </summary>
        /// <param name="X">横坐标(1-8)</param>
        /// <param name="Y">纵坐标(1-8)</param>
        public ChessPosition(int X, int Y)
        {
            this.X = X - 1;
            this.Y = Y - 1;
        }
        
        /// <summary>
        /// 获取或设置当前位置的棋盘横坐标(a-h)
        /// </summary>
        public char Horizontal
        {
            get { return _horizontal; }
            set
            {
                string str = "abcdefgh";
                if (!str.Contains(value.ToString()))
                    throw new ArgumentException(ExceptionString.ex_illegalHorizontalValue, "horizontal");
                _horizontal = value;
                _x = Utility.CharToInt(value) - 1;
            }
        }
        
        /// <summary>
        /// 获取或设置当前位置的棋盘纵坐标(1-8)
        /// </summary>
        public int Vertical
        {
            get { return _vertical; }
            set
            {
                if (!(value >= 1 && value <= 8))
                    throw new ArgumentException(ExceptionString.ex_illegalVerticalValue, "vertical");
                _vertical = value;
                _y = (value - 1);
            }
        }

        /// <summary>
        /// 获取或设置当前位置的计算机X坐标(0-7)
        /// </summary>
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

        /// <summary>
        /// 获取或设置当前位置的计算机Y坐标(0-7)
        /// </summary>
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

        /// <summary>
        /// 向北移一格
        /// </summary>
        public ChessPosition ShiftNorth()
        {
            if (_y == 7)
                return ChessPosition.Empty;
            return new ChessPosition(_horizontal, _y + 1);
        }
        /// <summary>
        /// 向南移一格
        /// </summary>
        public ChessPosition ShiftSouth()
        {
            if (_y == 1)
                return ChessPosition.Empty;
            return new ChessPosition(_horizontal, _y - 1);
        }
        /// <summary>
        /// 向西移一格
        /// </summary>
        public ChessPosition ShiftWest()
        {
            if (_x == 1)
                return ChessPosition.Empty;
            return new ChessPosition(_x - 1, _vertical);
        }
        /// <summary>
        /// 向东移一格
        /// </summary>
        public ChessPosition ShiftEast()
        {
            if (_x == 7)
                return ChessPosition.Empty;
            return new ChessPosition(_x + 1, _vertical);
        }
        /// <summary>
        /// 向西北移一格
        /// </summary>
        public ChessPosition ShiftWestNorth()
        {
            if (_x == 1 || _y == 8)
                return ChessPosition.Empty;
            return new ChessPosition(_x - 1, _y + 1);
        }
        /// <summary>
        /// 向东北移一格
        /// </summary>
        public ChessPosition ShiftEastNorth()
        {
            if (_x == 8 || _y == 8)
                return ChessPosition.Empty;
            return new ChessPosition(_x + 1, _y + 1);
        }
        /// <summary>
        /// 向西南移一格
        /// </summary>
        public ChessPosition ShiftWestSouth()
        {
            if (_x == 1 || _y == 1)
                return ChessPosition.Empty;
            return new ChessPosition(_x - 1, _y - 1);
        }
        /// <summary>
        /// 向东南移一格
        /// </summary>
        public ChessPosition ShiftEastSouth()
        {
            if (_x == 8 || _y == 1)
                return ChessPosition.Empty;
            return new ChessPosition(_x + 1, _y - 1);
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
