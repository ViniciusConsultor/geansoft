using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public interface IChessman
    {
        /// <summary>
        /// 黑棋或是白棋
        /// </summary>
        Enums.ChessmanType ChessmanType { get; }

        /// <summary>
        /// 棋子的具体表现
        /// </summary>
        Enums.ChessmanItem ChessmanItem { get; }

        /// <summary>
        /// 移动棋子到另一个格子
        /// </summary>
        /// <param name="grid">将要被移动到的格子</param>
        /// <returns>是否成功移动</returns>
        bool MoveToGrid(ChessboardGrid grid);

        /// <summary>
        /// 初始化棋子
        /// </summary>
        void InitializeComponent();

        /// <summary>
        /// 移除棋子，作为被其他棋子吃掉
        /// </summary>
        void Remove();
    }
}
