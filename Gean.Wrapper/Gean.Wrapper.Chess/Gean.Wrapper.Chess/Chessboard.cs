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

        public Chessboard()
        {
            this.InitializeComponent();
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
                        new ChessboardGrid.AxisY(y));
                    if ((y % 2) == 0)
                        if ((x % 2) == 0)
                            _boardGrids[x, y].ChessboardGridType = Enums.ChessboardGridType.White;
                        else
                            _boardGrids[x, y].ChessboardGridType = Enums.ChessboardGridType.Black;
                    else
                        if ((x % 2) == 0)
                            _boardGrids[x, y].ChessboardGridType = Enums.ChessboardGridType.Black;
                        else
                            _boardGrids[x, y].ChessboardGridType = Enums.ChessboardGridType.White;
                }
            }
        }
    }
}
