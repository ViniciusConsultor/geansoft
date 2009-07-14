using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 一个描述棋盘的每个格子类
    /// </summary>
    public class ChessboardGrid
    {
        public AxisX X { get; set; }
        public AxisY Y { get; set; }
        public Enums.ChessboardGridType ChessboardGridType { get; set; }

        public ChessboardGrid(int x, int y)
            : this(new AxisX(x), new AxisY(y)) { }

        public ChessboardGrid(AxisX x, AxisY y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// 棋盘的X轴
        /// </summary>
        public struct AxisX
        {
            public int X
            {
                get { return this._x; }
            }
            private int _x;
            private char _word;

            public AxisX(int x)
            {
                this._x = 0;
                this._word = 'z';
                if (x < 1 || x > 8)
                {
                    this._x = x;
                    switch (x)
                    {
                        case 1:
                            this._word = 'a';
                            break;
                        case 2:
                            this._word = 'b';
                            break;
                        case 3:
                            this._word = 'c';
                            break;
                        case 4:
                            this._word = 'd';
                            break;
                        case 5:
                            this._word = 'e';
                            break;
                        case 6:
                            this._word = 'f';
                            break;
                        case 7:
                            this._word = 'g';
                            break;
                        case 8:
                            this._word = 'h';
                            break;
                        default:
                            this._word = 'z';
                            break;
                    }
                }
            }
            public AxisX(char c)
            {
                this._x = 0;
                this._word = 'z';

                if (c < 'a' || c > 'h')
                {
                    string str = (Convert.ToString(c)).Trim().ToLowerInvariant();
                    this._word = char.Parse(str);
                    switch (c)
                    {
                        case 'a':
                            this._x = 1;
                            break;
                        case 'b':
                            this._x = 2;
                            break;
                        case 'c':
                            this._x = 3;
                            break;
                        case 'd':
                            this._x = 4;
                            break;
                        case 'e':
                            this._x = 5;
                            break;
                        case 'f':
                            this._x = 6;
                            break;
                        case 'g':
                            this._x = 7;
                            break;
                        case 'h':
                            this._x = 8;
                            break;
                    }
                }
            }

            public override string ToString()
            {
                return Convert.ToString(this._word);
            }
        }

        /// <summary>
        /// 棋盘的Y轴
        /// </summary>
        public struct AxisY
        {
            public int Y
            {
                get { return this._y; }
            }
            private int _y;

            public AxisY(int y)
            {
                this._y = y;
            }

            public override string ToString()
            {
                return this._y.ToString();
            }
        }

    }
}
