﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    /// <summary>
    /// 一个描述棋子移动时方法的接口
    /// </summary>
    public interface IPieceMove
    {
        Enums.Action PieceMoveIn(Pair<Position, Position> step);
        event PieceMoveIn PieceMoveInEvent;

        void PieceMoveOut(Position pos);
        event PieceMoveOut PieceMoveOutEvent;
    }
    /// <summary>
    /// 一个描述棋子移动入某棋格的委托类型
    /// </summary>
    /// <param name="piece">棋子</param>
    /// <param name="action">移动所产生的动作类型</param>
    /// <param name="positionPair">移动时源、目标棋格对</param>
    public delegate void PieceMoveIn(Piece piece, Enums.Action action, Pair<Position, Position> positions);
    /// <summary>
    /// 一个描述棋子移出棋局（被杀死）的委托类型
    /// </summary>
    /// <param name="piece">棋子</param>
    /// <param name="pos">棋子被杀死时的所在棋格</param>
    public delegate void PieceMoveOut(Piece piece, Position pos);
}
