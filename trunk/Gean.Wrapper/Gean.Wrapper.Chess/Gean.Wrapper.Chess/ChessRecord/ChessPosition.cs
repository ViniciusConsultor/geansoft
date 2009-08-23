using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Gean.Wrapper.Chess
{
    /*   FEN Dot
     * 
     *   8 |     1   2   3   4   5   6   7   8
     *   7 |     9  10  11  12  13  14  15  16
     *   6 |    17  18  19  20  21  22  23  24
     *   5 |    25  26  27  28  29  30  31  32
     *   4 |    33  34  35  36  37  38  39  40
     *   3 |    41  42  43  44  45  46  47  48
     *   2 |    49  50  51  52  53  54  55  56
     *   1 |    57  58  59  60  61  62  63  64
     *          ------------------------------
     *           1   2   3   4   5   6   7   8
     *           a   b   c   d   e   f   g   h
     *   
     *   this.Dot = (8 * (7 - _y)) + (_x + 1);
     */

    /// <summary>
    /// 一个描述棋盘位置的类型
    /// </summary>
    public class ChessPosition
    {
        #region static

        public static readonly ChessPosition Empty = null;

        public static ChessPosition Parse(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException();
            if (value.Length != 2) throw new ArgumentOutOfRangeException(value);

            char horizontal = value[0];
            int vertical = int.Parse(value[1].ToString());

            return new ChessPosition(horizontal, vertical);
        }

        public static ChessPosition GetPositionByDot(int dot)
        {
            if (dot < 1 || dot > 64)
            {
                return Empty;
            }
            int x;
            int y;
            ChessPosition.CalculateXY(dot, out x, out y);
            return new ChessPosition(x, y);
        }

        public static void CalculateXY(int dot, out int x, out int y)
        {
            x = dot % 8;
            y = 8 - ((dot - 1) / 8);
            if (x == 0) x = 8;
        }

        public static int CalculateDot(int x, int y)
        {
            return (8 * (7 - y)) + (x + 1);
        }

        #endregion

        #region ctor

        /// <summary>
        /// 一个描述棋盘位置的类型
        /// </summary>
        /// <param name="horizontal">横坐标(a-h)</param>
        /// <param name="vertical">纵坐标(1-8)</param>
        public ChessPosition(char horizontal, int vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
            _x = Utility.CharToInt(horizontal) - 1;
            _y = vertical - 1;
            this.Dot = ChessPosition.CalculateDot(_x, _y);
        }

        /// <summary>
        /// 一个描述棋盘位置的类型
        /// </summary>
        /// <param name="X">横坐标(1-8)</param>
        /// <param name="Y">纵坐标(1-8)</param>
        public ChessPosition(int x, int y)
        {
            _x = x - 1;
            _y = y - 1;
            _horizontal = Utility.IntToChar(x);
            _vertical = y;
            this.Dot = ChessPosition.CalculateDot(_x, _y);
        }

        #endregion

        #region Property

        /// <summary>
        /// 获取或设置当前位置的棋盘横坐标(a-h)
        /// </summary>
        public char Horizontal { get { return _horizontal; } }
        /// <summary>
        /// 当前位置的棋盘横坐标(a-h)
        /// </summary>
        private char _horizontal;

        /// <summary>
        /// 获取或设置当前位置的棋盘纵坐标(1-8)
        /// </summary>
        public int Vertical { get { return _vertical; } }
        /// <summary>
        /// 当前位置的棋盘纵坐标(1-8)
        /// </summary>
        private int _vertical;

        /// <summary>
        /// 获取或设置当前位置的计算机X坐标(0-7)
        /// </summary>
        public int X { get { return _x; } }
        /// <summary>
        /// 当前位置的计算机X坐标(0-7)
        /// </summary>
        private int _x;

        /// <summary>
        /// 获取或设置当前位置的计算机Y坐标(0-7)
        /// </summary>
        public int Y { get { return _y; } }
        /// <summary>
        /// 当前位置的计算机Y坐标(0-7)
        /// </summary>
        private int _y;

        /// <summary>
        /// 获取该位置对应FEN的点(1-64)
        /// </summary>
        public int Dot { get; private set; }

        #endregion

        #region Override

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            ChessPosition point = (ChessPosition)obj;
            if (this.X != point.X) return false;
            if (this.Y != point.Y) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked
                (3 * (
                this._x.GetHashCode() +
                this._y.GetHashCode()
                ));
        }
        public override string ToString()
        {
            return string.Format("{0}{1}", this.Horizontal, this.Vertical);
        }

        #endregion

        #region Shift

        /// <summary>
        /// 向北移一格
        /// </summary>
        internal ChessPosition ShiftNorth()
        {
            if (_y == 7)
                return ChessPosition.Empty;
            return new ChessPosition(_horizontal, _y + 2);
        }
        /// <summary>
        /// 向南移一格
        /// </summary>
        internal ChessPosition ShiftSouth()
        {
            if (_y == 0)
                return ChessPosition.Empty;
            return new ChessPosition(_horizontal, _y);
        }
        /// <summary>
        /// 向西移一格
        /// </summary>
        internal ChessPosition ShiftWest()
        {
            if (_x == 0)
                return ChessPosition.Empty;
            return new ChessPosition(_x, _vertical);
        }
        /// <summary>
        /// 向东移一格
        /// </summary>
        internal ChessPosition ShiftEast()
        {
            if (_x == 7)
                return ChessPosition.Empty;
            return new ChessPosition(_x + 2, _vertical);
        }
        /// <summary>
        /// 向西北移一格
        /// </summary>
        internal ChessPosition ShiftWestNorth()
        {
            if (_x == 0 || _y == 7)
                return ChessPosition.Empty;
            return new ChessPosition(_x, _y + 2);
        }
        /// <summary>
        /// 向东北移一格
        /// </summary>
        internal ChessPosition ShiftEastNorth()
        {
            if (_x == 7 || _y == 7)
                return ChessPosition.Empty;
            return new ChessPosition(_x + 2, _y + 2);
        }
        /// <summary>
        /// 向西南移一格
        /// </summary>
        internal ChessPosition ShiftWestSouth()
        {
            if (_x == 0 || _y == 0)
                return ChessPosition.Empty;
            return new ChessPosition(_x, _y);
        }
        /// <summary>
        /// 向东南移一格
        /// </summary>
        internal ChessPosition ShiftEastSouth()
        {
            if (_x == 7 || _y == 0)
                return ChessPosition.Empty;
            return new ChessPosition(_x + 2, _y);
        }

        #endregion

        #region Get enable position array

        public ChessPosition[] GetPositions(Chessman man, Enums.ChessmanSide side)
        {
            switch (man.ChessmanType)
            {
                case Enums.ChessmanType.Rook:
                    return this.GetRookPositions();
                case Enums.ChessmanType.Knight:
                    return this.GetKnightPositions();
                case Enums.ChessmanType.Bishop:
                    return this.GetBishopPositions();
                case Enums.ChessmanType.Queen:
                    return this.GetQueenPositions();
                case Enums.ChessmanType.King:
                    return this.GetKingPositions();
                case Enums.ChessmanType.Pawn:
                    return this.GetPawnPositions(side);
                case Enums.ChessmanType.None:
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取“兵”的可能移动到的位置
        /// </summary>
        public ChessPosition[] GetPawnPositions(Enums.ChessmanSide side)
        {
            List<ChessPosition> positions = new List<ChessPosition>();
            ChessPosition pos;
            if (side == Enums.ChessmanSide.White)
            {
                if (this._y < 1) return null;

                pos = this.ShiftNorth();
                positions.Add(pos);

                if (_y == 1)
                {
                    pos = pos.ShiftNorth();
                    positions.Add(pos);
                }

                pos = this.ShiftWestNorth();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);

                pos = this.ShiftEastNorth();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
            }
            if (side == Enums.ChessmanSide.Black)
            {
                if (this._y > 6) return null;

                pos = this.ShiftSouth();
                positions.Add(pos);

                if (_y == 6)
                {
                    pos = pos.ShiftSouth();
                    positions.Add(pos);
                }

                pos = this.ShiftWestSouth();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);

                pos = this.ShiftEastSouth();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
            }
            return positions.ToArray();
        }

        /// <summary>
        /// 获取“车”的可能移动到的位置
        /// </summary>
        public ChessPosition[] GetRookPositions()
        {
            List<ChessPosition> positions = new List<ChessPosition>();

            ChessPosition pos = this;
            while (pos != ChessPosition.Empty)
            {
                pos = pos.ShiftEast();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
            }
            pos = this;
            while (pos != ChessPosition.Empty)
            {
                pos = pos.ShiftWest();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
            }
            pos = this;
            while (pos != ChessPosition.Empty)
            {
                pos = pos.ShiftNorth();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
            }
            pos = this;
            while (pos != ChessPosition.Empty)
            {
                pos = pos.ShiftSouth();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
            }

            return positions.ToArray();
        }

        /// <summary>
        /// 获取“马”的可能移动到的位置
        /// </summary>
        public ChessPosition[] GetKnightPositions()
        {
            List<ChessPosition> positions = new List<ChessPosition>();
            ChessPosition aPos = this.ShiftWestNorth();
            ChessPosition bPos = this.ShiftEastNorth();
            ChessPosition cPos = this.ShiftWestSouth();
            ChessPosition dPos = this.ShiftEastSouth();
            ChessPosition pos;
            if (aPos != ChessPosition.Empty)
            {
                pos = aPos.ShiftNorth();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
                pos = aPos.ShiftWest();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
            }
            if (bPos != ChessPosition.Empty)
            {
                pos = bPos.ShiftNorth();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
                pos = bPos.ShiftEast();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
            }
            if (cPos != ChessPosition.Empty)
            {
                pos = cPos.ShiftWest();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
                pos = cPos.ShiftSouth();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
            }
            if (dPos != ChessPosition.Empty)
            {
                pos = dPos.ShiftEast();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
                pos = dPos.ShiftSouth();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
            }

            return positions.ToArray();
        }

        /// <summary>
        /// 获取“象”的可能移动到的位置
        /// </summary>
        public ChessPosition[] GetBishopPositions()
        {
            List<ChessPosition> positions = new List<ChessPosition>();
            ChessPosition pos = this;

            while (pos != ChessPosition.Empty)
            {
                pos = pos.ShiftEastNorth();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
            }
            pos = this;
            while (pos != ChessPosition.Empty)
            {
                pos = pos.ShiftEastSouth();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
            }
            pos = this;
            while (pos != ChessPosition.Empty)
            {
                pos = pos.ShiftWestNorth();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
            }
            pos = this;
            while (pos != ChessPosition.Empty)
            {
                pos = pos.ShiftWestSouth();
                if (pos != ChessPosition.Empty)
                    positions.Add(pos);
            }
            pos = this;


            return positions.ToArray();
        }

        /// <summary>
        /// 获取“后”的可能移动到的位置
        /// </summary>
        public ChessPosition[] GetQueenPositions()
        {
            List<ChessPosition> positions = new List<ChessPosition>();
            positions.AddRange(this.GetRookPositions());
            positions.AddRange(this.GetBishopPositions());
            return positions.ToArray();
        }

        /// <summary>
        /// 获取“王”的可能移动到的位置
        /// </summary>
        public ChessPosition[] GetKingPositions()
        {
            List<ChessPosition> positions = new List<ChessPosition>();
            ChessPosition pos = this;

            pos = this.ShiftEast();
            if (pos != ChessPosition.Empty)
                positions.Add(pos);
            pos = this.ShiftWest();
            if (pos != ChessPosition.Empty)
                positions.Add(pos);
            pos = this.ShiftSouth();
            if (pos != ChessPosition.Empty)
                positions.Add(pos);
            pos = this.ShiftNorth();
            if (pos != ChessPosition.Empty)
                positions.Add(pos);
            pos = this.ShiftEastNorth();
            if (pos != ChessPosition.Empty)
                positions.Add(pos);
            pos = this.ShiftEastSouth();
            if (pos != ChessPosition.Empty)
                positions.Add(pos);
            pos = this.ShiftWestNorth();
            if (pos != ChessPosition.Empty)
                positions.Add(pos);
            pos = this.ShiftWestSouth();
            if (pos != ChessPosition.Empty)
                positions.Add(pos);


            return positions.ToArray();
        }

        #endregion
    }
}
