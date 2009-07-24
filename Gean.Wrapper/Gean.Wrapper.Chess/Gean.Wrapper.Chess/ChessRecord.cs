using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述一局棋的记录，该记录可能与更多的棋局记录保存在一个PGN文件中
    /// </summary>
    /// <example>
    /// [Event "WCh"]
    /// [Site "Bonn GER"]
    /// [Date "2008.10.14"]
    /// [Round "1"]
    /// [White "Kramnik,V"]
    /// [Black "Anand,V"]
    /// [Result "1/2-1/2"]
    /// [WhiteELO "2772"]
    /// [BlackELO "2783"]
    /// 1. d4 d5 2. c4 {c4冲击中心} 2... c6 3. Nc3 Nf6 { 快出子,保护d5兵,防御白e4兵的挺进} 4. cxd5
    /// cxd5 5. Bf4 Nc6 6. e3 {保护d4兵,通白格象路} 6... Bf5 7. Nf3 e6 8. Qb3 Bb4 9. Bb5 O-O
    /// 10. Bxc6 Bxc3+ 11. Qxc3 Rc8 {黑车好棋,牵制白后} 12. Ne5 Ng4 13. Nxg4 Bxg4 14. Qb4
    /// Rxc6 15. Qxb7 Qc8 16. Qxc8 Rfxc8 17. O-O a5 18. f3 Bf5 19. Rfe1 Bg6 20. b3 f6
    /// 21. e4 dxe4 22. fxe4 Rd8 23. Rad1 Rc2 24. e5 fxe5 25. Bxe5 Rxa2 26. Ra1 Rxa1
    /// 27. Rxa1 Rd5 28. Rc1 Rd7 29. Rc5 Ra7 30. Rc7 Rxc7 31. Bxc7 Bc2 32. Bxa5 Bxb3
    /// 1/2-1/2
    /// </example>
    public class ChessRecord
    {
        public Definer Definer { get; internal set; }
        public ChessStepPairSequence Sequence { get; internal set; }
        public ChessCommentCollection Comments { get; internal set; }

        public ChessRecord()
        {
            this.Definer = new Definer();
            this.Sequence = new ChessStepPairSequence();
            this.Comments = new ChessCommentCollection();
        }

        public override bool Equals(object obj)
        {
            ChessRecord pr = obj as ChessRecord;
            if (!UtilityEquals.EnumerableEquals(this.Definer, pr.Definer))
                return false;
            if (!UtilityEquals.CollectionsEquals<ChessStepPair>(this.Sequence, pr.Sequence))
                return false;
            if (!UtilityEquals.CollectionsEquals<ChessComment>(this.Comments, pr.Comments))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked(7 * (this.Definer.GetHashCode() + this.Sequence.GetHashCode()));
        }
    }
}
