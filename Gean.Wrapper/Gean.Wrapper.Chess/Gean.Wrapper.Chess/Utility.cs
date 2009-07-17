using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    static class Utility
    {
        public const int TOP = 8;
        public const int FOOTER = 1;
        public const int LEFT = 1;
        public const int RIGHT = 8;

        /// <summary>
        /// 获取一般正规棋局起始的棋子状态
        /// </summary>
        /// <returns>返回正规棋局的32个棋子</returns>
        static public ChessmanState[] GetOpenningsChessmans()
        {
            ChessmanState[] chessmans = new ChessmanState[32];
            Chessman chessman = null;
            Square square = new Square();
            int i = 1;

            #region  - 兵 -
            for (; i <= 8; i++)//初始化黑兵
            {
                chessman = new ChessmanPawn(Enums.ChessmanSide.Black);
                square = new Square(i, 7);
                chessmans[i - 1] = new ChessmanState(chessman, square);
            }
            for (; i <= 16; i++)//初始化白兵
            {
                chessman = new ChessmanPawn(Enums.ChessmanSide.White);
                square = new Square(i - 8, 2);
                chessmans[i - 1] = new ChessmanState(chessman, square);
            }
            #endregion

            #region - 车 -
            //白格黑车
            chessman = new ChessmanRook(Enums.ChessmanSide.Black);
            square = new Square(1, 8);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            i++;
            //黑格黑车
            chessman = new ChessmanRook(Enums.ChessmanSide.Black);
            square = new Square(8, 8);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            i++;
            //黑格白车
            chessman = new ChessmanRook(Enums.ChessmanSide.White);
            square = new Square(1, 1);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            i++;
            //白格白车
            chessman = new ChessmanRook(Enums.ChessmanSide.White);
            square = new Square(8, 1);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            i++;
            #endregion

            #region - 马 -
            //黑格黑马
            chessman = new ChessmanKnight(Enums.ChessmanSide.Black);
            square = new Square(2, 8);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            i++;
            //白格黑马
            chessman = new ChessmanKnight(Enums.ChessmanSide.Black);
            square = new Square(7, 8);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            i++;
            //白格白马
            chessman = new ChessmanKnight(Enums.ChessmanSide.White);
            square = new Square(2, 1);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            i++;
            //黑格白马
            chessman = new ChessmanKnight(Enums.ChessmanSide.White);
            square = new Square(7, 1);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            i++;
            #endregion

            #region - 象 -
            //白格黑象
            chessman = new ChessmanBishop(Enums.ChessmanSide.Black);
            square = new Square(3, 8);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            i++;
            //黑格黑象
            chessman = new ChessmanBishop(Enums.ChessmanSide.Black);
            square = new Square(6, 8);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            i++;
            //黑格白象
            chessman = new ChessmanBishop(Enums.ChessmanSide.White);
            square = new Square(3, 1);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            i++;
            //白格白象
            chessman = new ChessmanBishop(Enums.ChessmanSide.White);
            square = new Square(6, 1);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            i++;
            #endregion

            #region - 后 -
            //黑后
            chessman = new ChessmanQueen(Enums.ChessmanSide.Black);
            square = new Square(4, 8);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            i++;
            //白后
            chessman = new ChessmanQueen(Enums.ChessmanSide.White);
            square = new Square(4, 1);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            i++;
            #endregion

            #region - 王 -
            //黑王
            chessman = new ChessmanKing(Enums.ChessmanSide.Black);
            square = new Square(5, 8);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            i++;
            //白王
            chessman = new ChessmanKing(Enums.ChessmanSide.White);
            square = new Square(5, 1);
            chessmans[i - 1] = new ChessmanState(chessman, square);
            #endregion

            return chessmans;
        }

        /// <summary>
        /// 将指定的棋盘横坐标字符转换成整型数字
        /// </summary>
        /// <param name="c">指定的棋盘横坐标字符</param>
        /// <returns>
        /// 如果返回值为-1，则输入值是非棋盘横坐标字符。
        /// 坐标值遵循象棋规则将从1开始。
        /// </returns>
        static public int CharToInt(char c)
        {
            if (char.IsUpper(c))
            {
                string str = Convert.ToString(c);
                str = str.ToLowerInvariant();
                c = Convert.ToChar(str);
            }
            switch (c)
            {
                #region case
                case 'a':
                    return 1;
                case 'b':
                    return 2;
                case 'c':
                    return 3;
                case 'd':
                    return 4;
                case 'e':
                    return 5;
                case 'f':
                    return 6;
                case 'g':
                    return 7;
                case 'h':
                    return 8;
                default:
                    return -1;
                #endregion
            }
        }

        /// <summary>
        /// 将指定的棋盘横坐标字符转换成整型数字
        /// </summary>
        /// <param name="str">指定的棋盘横坐标字符</param>
        /// <returns>
        /// 如果返回值为-1，则输入值是非棋盘横坐标字符。
        /// 坐标值遵循象棋规则将从1开始。
        /// </returns>
        static public int StringToInt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return -1;
            }
            if (str.Length > 1)
            {
                return -1;
            }
            return Utility.CharToInt(Convert.ToChar(str.ToLowerInvariant()));
        }

        /// <summary>
        /// 将指定的坐标值转换成字符
        /// </summary>
        static public char IntToChar(int i)
        {
            if (i >= 1 && i <= 8)
            {
                #region switch
                switch (i)
                {
                    case 1:
                        return 'a';
                    case 2:
                        return 'b';
                    case 3:
                        return 'c';
                    case 4:
                        return 'd';
                    case 5:
                        return 'e';
                    case 6:
                        return 'f';
                    case 7:
                        return 'g';
                    case 8:
                        return 'h';
                    default:
                        return '*';
                }
                #endregion
            }
            return '*';
        }

        /// <summary>
        /// 将指定的坐标值转换成字符
        /// </summary>
        static public string IntToString(int i)
        {
            return Utility.IntToChar(i).ToString();
        }

    }
}
