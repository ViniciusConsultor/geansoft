using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 一个描述棋盘的类
    /// </summary>
    public class Chessboard
    {
        private ChessboardGrid[,] _boardGrids = new ChessboardGrid[8, 8];
        private ChessmanBase[] _chessmans;

        public Chessboard()
        {
            this.InitializeGrid();
        }
        public Chessboard(ChessmanBase[] chessmans)
        {
            this.InitializeGrid();
            this._chessmans = chessmans;
        }

        /// <summary>
        /// 初始化组件（棋盘，即初始化整个棋盘的每个棋格）
        /// </summary>
        private void InitializeGrid()
        {
            for (int x = 0; x < _boardGrids.GetLength(0); x++)
            {
                for (int y = 0; y < _boardGrids.GetLength(1); y++)
                {
                    _boardGrids[x, y] = new ChessboardGrid(x + 1, y + 1, this);
                    if ((y % 2) == 0)
                        if ((x % 2) == 0)
                            _boardGrids[x, y].ChessboardGridSide = Enums.ChessboardGridSide.White;
                        else
                            _boardGrids[x, y].ChessboardGridSide = Enums.ChessboardGridSide.Black;
                    else
                        if ((x % 2) == 0)
                            _boardGrids[x, y].ChessboardGridSide = Enums.ChessboardGridSide.Black;
                        else
                            _boardGrids[x, y].ChessboardGridSide = Enums.ChessboardGridSide.White;
                }
            }
        }

        public void InitializeChessmans()
        {
            this._chessmans = Helper.GetIntegratedChessmans(this);
        }

        /// <summary>
        /// 获取指定坐标的棋格
        /// </summary>
        /// <param name="x">指定坐标的x轴</param>
        /// <param name="y">指定坐标的y轴</param>
        /// <returns></returns>
        public ChessboardGrid GetGrid(int x, int y)
        {
            return this._boardGrids[x - 1, y - 1];
        }
        /// <summary>
        /// 获取指定坐标字符串的棋格
        /// </summary>
        /// <param name="step">指定坐标字符串</param>
        /// <returns></returns>
        public ChessboardGrid GetGrid(string step)
        {
            if (string.IsNullOrEmpty(step))
            {
                throw new ArgumentNullException(step);
            }
            if (step.Length != 2)
            {
                throw new ArgumentException(step);
            }
            char[] chars = step.ToLowerInvariant().ToCharArray();
            int x = Utility.CharToInt(chars[0]);
            int y = Convert.ToInt32(chars[1]);
            return this.GetGrid(x, y);
        }

        /// <summary>
        /// 获取指定的棋子所能到达的棋格。
        /// TODO:该方法代码未完成。不要使用该方法。
        /// </summary>
        /// <param name="chessman">指定的棋子</param>
        /// <returns>能到达的棋格</returns>
        public ChessboardGrid[] GetUsableGrids(ChessmanBase chessman)
        {
            List<ChessboardGrid> grids = new List<ChessboardGrid>();
            switch (chessman.ChessmanType)
            {
                case Enums.ChessmanType.Rook:
                    //grids.AddRange(Helper.GetRookGrid(this, chessman.GridOwner, chessman.ChessmanType, chessman.ChessmanSide));
                    break;
                case Enums.ChessmanType.Knight:
                    break;
                case Enums.ChessmanType.Bishop:
                    break;
                case Enums.ChessmanType.Queen:
                    break;
                case Enums.ChessmanType.King:
                    break;
                case Enums.ChessmanType.Pawn:
                    break;
                default:
                    break;
            }
            return grids.ToArray();
        }

        static class Helper
        {
            /// <summary>
            /// 获取整套棋子，并注册在棋盘
            /// </summary>
            /// <param name="board">需被注册到的棋盘</param>
            /// <returns></returns>
            internal static ChessmanBase[] GetIntegratedChessmans(Chessboard board)
            {
                ChessmanBase[] chessmans = new ChessmanBase[32];
                ChessboardGrid grid;

                int i = 1;

                #region  - 兵 -
                for (; i <= 8; i++)//初始化黑兵
                {
                    grid = board.GetGrid(i, 7);
                    chessmans[i - 1] = new ChessmanPawn(grid, Enums.ChessmanSide.Black);
                    chessmans[i - 1].RegistChessman(grid);
                }
                for (; i <= 16; i++)//初始化白兵
                {
                    grid = board.GetGrid(i - 8, 2);
                    chessmans[i - 1] = new ChessmanPawn(grid, Enums.ChessmanSide.White);
                    chessmans[i - 1].RegistChessman(grid);
                }
                #endregion

                #region - 车 -
                //白格黑车
                grid = board.GetGrid(1, 8);
                chessmans[i - 1] = new ChessmanRook(grid, Enums.ChessmanSide.Black);
                chessmans[i - 1].RegistChessman(grid);
                i++;
                //黑格黑车
                grid = board.GetGrid(8, 8);
                chessmans[i - 1] = new ChessmanRook(grid, Enums.ChessmanSide.Black);
                chessmans[i - 1].RegistChessman(grid);
                i++;
                //黑格白车
                grid = board.GetGrid(1, 1);
                chessmans[i - 1] = new ChessmanRook(grid, Enums.ChessmanSide.White);
                chessmans[i - 1].RegistChessman(grid);
                i++;
                //白格白车
                grid = board.GetGrid(8, 1);
                chessmans[i - 1] = new ChessmanRook(grid, Enums.ChessmanSide.White);
                chessmans[i - 1].RegistChessman(grid);
                i++;
                #endregion

                #region - 马 -
                //黑格黑马
                grid = board.GetGrid(2, 8);
                chessmans[i - 1] = new ChessmanKnight(grid, Enums.ChessmanSide.Black);
                chessmans[i - 1].RegistChessman(grid);
                i++;
                //白格黑马
                grid = board.GetGrid(7, 8);
                chessmans[i - 1] = new ChessmanKnight(grid, Enums.ChessmanSide.Black);
                chessmans[i - 1].RegistChessman(grid);
                i++;
                //白格白马
                grid = board.GetGrid(2, 1);
                chessmans[i - 1] = new ChessmanKnight(grid, Enums.ChessmanSide.White);
                chessmans[i - 1].RegistChessman(grid);
                i++;
                //黑格白马
                grid = board.GetGrid(7, 1);
                chessmans[i - 1] = new ChessmanKnight(grid, Enums.ChessmanSide.White);
                chessmans[i - 1].RegistChessman(grid);
                i++;
                #endregion

                #region - 象 -
                //白格黑象
                grid = board.GetGrid(3,8);
                chessmans[i - 1] = new ChessmanBishop(grid, Enums.ChessmanSide.Black);
                chessmans[i - 1].RegistChessman(grid);
                i++;
                //黑格黑象
                grid = board.GetGrid(6,8);
                chessmans[i - 1] = new ChessmanBishop(grid, Enums.ChessmanSide.Black);
                chessmans[i - 1].RegistChessman(grid);
                i++;
                //黑格白象
                grid = board.GetGrid(3,1);
                chessmans[i - 1] = new ChessmanBishop(grid, Enums.ChessmanSide.White);
                chessmans[i - 1].RegistChessman(grid);
                i++;
                //白格白象
                grid = board.GetGrid(6,1);
                chessmans[i - 1] = new ChessmanBishop(grid, Enums.ChessmanSide.White);
                chessmans[i - 1].RegistChessman(grid);
                i++;
                #endregion

                #region - 后 -
                //黑后
                grid = board.GetGrid(4, 8);
                chessmans[i - 1] = new ChessmanQueen(grid, Enums.ChessmanSide.Black);
                chessmans[i - 1].RegistChessman(grid);
                i++;
                //白后
                grid = board.GetGrid(4, 1);
                chessmans[i - 1] = new ChessmanQueen(grid, Enums.ChessmanSide.White);
                chessmans[i - 1].RegistChessman(grid);
                i++;
                #endregion

                #region - 王 -
                //黑王
                grid = board.GetGrid(5, 8);
                chessmans[i - 1] = new ChessmanKing(grid, Enums.ChessmanSide.Black);
                chessmans[i - 1].RegistChessman(grid);
                i++;
                //白王
                grid = board.GetGrid(5, 1);
                chessmans[i - 1] = new ChessmanKing(grid, Enums.ChessmanSide.White);
                chessmans[i - 1].RegistChessman(grid);
                #endregion

                return chessmans;
            }
        }

    }
}
