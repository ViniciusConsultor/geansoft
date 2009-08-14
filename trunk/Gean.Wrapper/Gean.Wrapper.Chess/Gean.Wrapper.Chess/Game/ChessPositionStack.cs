using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 棋步集合(后进先出)，一般是应用在棋子类型中，表示一个棋子绑定的棋步路径
    /// </summary>
    public class ChessPositionStack : Stack<ChessPosition>
    {
    }

}