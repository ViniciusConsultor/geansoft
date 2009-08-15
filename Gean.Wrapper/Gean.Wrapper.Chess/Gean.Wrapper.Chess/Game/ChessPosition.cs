using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Gean.Wrapper.Chess
{
    /*
     *   8 |     1   2   3   4   5   6   7   8 = (9-8)*8
     *   7 |     9  10  11  12  13  14  15  16 = (9-7)*8
     *   6 |    17  18  19  20  21  22  23  24 = (9-6)*8
     *   5 |    25  26  27  28  29  30  31  32 = (9-5)*8
     *   4 |    33  34  35  36  37  38  39  40 = (9-4)*8
     *   3 |    41  42  43  44  45  46  47  48 = (9-3)*8
     *   2 |    49  50  51  52  53  54  55  56 = (9-2)*8
     *   1 |    57  58  59  60  61  62  63  64 = (9-1)*8
     *          ------------------------------
     *           1   2   3   4   5   6   7   8
     *           a   b   c   d   e   f   g   h
     */
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
            this.Dot = (9 - (_y + 1)) * (_x + 1);
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
            this.Dot = (9 - (_y + 1)) * (_x + 1);
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
        /// 获取该位置对应FEN的点(1-64)
        /// </summary>
        public int Dot { get; private set; }

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

        /// <summary>
        /// 向北移一格
        /// </summary>
        private ChessPosition ShiftNorth()
        {
            if (_y == 7)
                return ChessPosition.Empty;
            return new ChessPosition(_horizontal, _y + 1);
        }
        /// <summary>
        /// 向南移一格
        /// </summary>
        private ChessPosition ShiftSouth()
        {
            if (_y == 1)
                return ChessPosition.Empty;
            return new ChessPosition(_horizontal, _y - 1);
        }
        /// <summary>
        /// 向西移一格
        /// </summary>
        private ChessPosition ShiftWest()
        {
            if (_x == 1)
                return ChessPosition.Empty;
            return new ChessPosition(_x - 1, _vertical);
        }
        /// <summary>
        /// 向东移一格
        /// </summary>
        private ChessPosition ShiftEast()
        {
            if (_x == 7)
                return ChessPosition.Empty;
            return new ChessPosition(_x + 1, _vertical);
        }
        /// <summary>
        /// 向西北移一格
        /// </summary>
        private ChessPosition ShiftWestNorth()
        {
            if (_x == 1 || _y == 8)
                return ChessPosition.Empty;
            return new ChessPosition(_x - 1, _y + 1);
        }
        /// <summary>
        /// 向东北移一格
        /// </summary>
        private ChessPosition ShiftEastNorth()
        {
            if (_x == 8 || _y == 8)
                return ChessPosition.Empty;
            return new ChessPosition(_x + 1, _y + 1);
        }
        /// <summary>
        /// 向西南移一格
        /// </summary>
        private ChessPosition ShiftWestSouth()
        {
            if (_x == 1 || _y == 1)
                return ChessPosition.Empty;
            return new ChessPosition(_x - 1, _y - 1);
        }
        /// <summary>
        /// 向东南移一格
        /// </summary>
        private ChessPosition ShiftEastSouth()
        {
            if (_x == 8 || _y == 1)
                return ChessPosition.Empty;
            return new ChessPosition(_x + 1, _y - 1);
        }

        /// <summary>
        /// 获取兵的可能移动到的位置
        /// </summary>
        public ChessPosition[] GetPawnPositions(Enums.ChessmanSide side)
        {
            ChessPosition[] poss = new ChessPosition[3];
            if (side == Enums.ChessmanSide.Black)
            {
                poss[0] = this.ShiftWestNorth();
                poss[1] = this.ShiftNorth();
                poss[2] = this.ShiftEastNorth();
            }
            if (side == Enums.ChessmanSide.White)
            {
                poss[0] = this.ShiftWestSouth();
                poss[1] = this.ShiftSouth();
                poss[2] = this.ShiftEastSouth();
            }
            return poss;
        }
    }
}
