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
            this.InitializeComponent();
            this._chessmans = Helper.GetIntegratedChessman(this);
        }
        public Chessboard(ChessmanBase[] chessmans)
        {
            this.InitializeComponent();
            this._chessmans = chessmans;
        }

        /// <summary>
        /// 初始化组件（棋盘，即初始化整个棋盘的每个棋格）
        /// </summary>
        private void InitializeComponent()
        {
            for (int x = 0; x < _boardGrids.GetLength(0); x++)
            {
                for (int y = 0; y < _boardGrids.GetLength(1); y++)
                {
                    _boardGrids[x, y] = new ChessboardGrid(
                        new ChessboardGrid.AxisX(x),
                        new ChessboardGrid.AxisY(y),
                        this);
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

        /// <summary>
        /// 初始化棋盘中的所有棋子
        /// </summary>
        /// <param name="chessmans"></param>
        private void InitializeChessmans(ChessmanBase[] chessmans)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取指定坐标的棋格
        /// </summary>
        /// <param name="x">指定坐标的x轴</param>
        /// <param name="y">指定坐标的y轴</param>
        /// <returns></returns>
        public ChessboardGrid GetGrid(int x, int y)
        {
            return this._boardGrids[x, y];
        }

        static class Helper
        {
            internal static ChessmanBase[] GetIntegratedChessman(Chessboard board)
            {
                ChessmanBase[] chessmans = new ChessmanBase[32];
                int i = 1;

                #region 兵
                for (; i <= 8; i++)//初始化黑兵
                {
                    chessmans[i] = new ChessmanPawn(board.GetGrid(i, 7), Enums.ChessmanSide.Black);
                }
                for (; i <= 16; i++)//初始化白兵
                {
                    chessmans[i] = new ChessmanPawn(board.GetGrid(i, 2), Enums.ChessmanSide.White);
                }
                #endregion

                #region 车
                //白格黑车
                chessmans[i] = new ChessmanRook(board.GetGrid(1, 8), Enums.ChessmanSide.Black);
                i++;
                //黑格黑车
                chessmans[i] = new ChessmanRook(board.GetGrid(8, 8), Enums.ChessmanSide.Black);
                i++;
                //黑格白车
                chessmans[i] = new ChessmanRook(board.GetGrid(1, 1), Enums.ChessmanSide.White);
                i++;
                //白格白车
                chessmans[i] = new ChessmanRook(board.GetGrid(8, 1), Enums.ChessmanSide.White);
                i++;
                #endregion

                #region 马
                //黑格黑马
                chessmans[i] = new ChessmanKnight(board.GetGrid(2, 8), Enums.ChessmanSide.Black);
                i++;
                //白格黑马
                chessmans[i] = new ChessmanKnight(board.GetGrid(7, 8), Enums.ChessmanSide.Black);
                i++;
                //白格白马
                chessmans[i] = new ChessmanKnight(board.GetGrid(2, 1), Enums.ChessmanSide.White);
                i++;
                //黑格白马
                chessmans[i] = new ChessmanKnight(board.GetGrid(7, 1), Enums.ChessmanSide.White);
                i++;
                #endregion

                #region 象
                //白格黑象
                chessmans[i] = new ChessmanBishop(board.GetGrid(3, 8), Enums.ChessmanSide.Black);
                i++;
                //黑格黑象
                chessmans[i] = new ChessmanBishop(board.GetGrid(6, 8), Enums.ChessmanSide.Black);
                i++;
                //黑格白象
                chessmans[i] = new ChessmanBishop(board.GetGrid(3, 1), Enums.ChessmanSide.White);
                i++;
                //白格白象
                chessmans[i] = new ChessmanBishop(board.GetGrid(6, 1), Enums.ChessmanSide.White);
                i++;
                #endregion

                #region 后
                //黑后
                chessmans[i] = new ChessmanQueen(board.GetGrid(4, 8), Enums.ChessmanSide.Black);
                i++;
                //白后
                chessmans[i] = new ChessmanQueen(board.GetGrid(4, 1), Enums.ChessmanSide.White);
                i++;
                #endregion

                #region 王
                //黑王
                chessmans[i] = new ChessmanRook(board.GetGrid(5, 8), Enums.ChessmanSide.Black);
                i++;
                //白王
                chessmans[i] = new ChessmanRook(board.GetGrid(5, 1), Enums.ChessmanSide.White);
                i++;
                #endregion

                return chessmans;
            }
        }
    }
}
