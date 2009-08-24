using System;
using System.Collections.Generic;
using System.Text;
using Gean.Wrapper.Chess;

namespace Gean.UI.ChessControl
{
    public interface IActiveRecord
    {
        ChessRecord ActiveRecord { get; }
        int CurrChessStepPair { get; }
        Enums.ChessmanSide CurrChessmanSide { get; }
        ChessStep GetStep();
    }
}
