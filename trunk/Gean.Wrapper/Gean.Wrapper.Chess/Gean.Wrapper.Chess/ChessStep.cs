using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述棋局中的单方的一步棋。如："Nc6"代表马走到c6格。
    /// 两方各一步棋组成一个棋招（<see>ChessStepPair</see>）。
    /// 对于这步棋，绑定了一个注释的集合，一个变招的集合（变招也是每一步棋的集合）。
    /// </summary>
    public class ChessStep
    {
        /// <summary>
        /// 获取或设置该步棋的棋子类型
        /// </summary>
        public Enums.ChessmanType ChessmanType { get; set; }
        /// <summary>
        /// 获取或设置该步棋的战方
        /// </summary>
        public Enums.ChessmanSide ChessmanSide { get; set; }
        /// <summary>
        /// 获取或设置该步棋的目标棋格
        /// </summary>
        public ChessSquare TargetSquare { get; set; }
        /// <summary>
        /// 获取或设置该步棋的源棋格
        /// </summary>
        public ChessSquare SourceSquare { get; set; }
        /// <summary>
        /// 获取或设置一步棋的动作说明
        /// </summary>
        public Enums.AccessorialAction Action { get; set; }

        /// <summary>
        /// 获取或设置该棋步是“王车易位”
        /// </summary>
        public Enums.Castling Castling 
        {
            get { return this._castling; }
            set { this._castling = value; } 
        }
        private Enums.Castling _castling = Enums.Castling.None;

        /// <summary>
        /// 获取或设置该步棋的注释集合
        /// </summary>
        public ChessCommentCollection Comments { get; set; }
        /// <summary>
        /// 获取或设置该步棋的变招集合
        /// </summary>
        public ChessStepPairSequenceCollection ChoiceSteps { get; set; }

        public ChessStep(Enums.ChessmanSide manSide, Enums.Castling castling)
        {
            this._castling = castling;
            this.ChessmanSide = manSide;

            this.Comments = new ChessCommentCollection();
            this.ChoiceSteps = new ChessStepPairSequenceCollection();
        }

        public ChessStep(Enums.ChessmanSide manSide, Enums.ChessmanType manType, ChessSquare targetSquare, ChessSquare sourceSquare, Enums.AccessorialAction action)
        {
            this.ChessmanType = manType;
            this.ChessmanSide = manSide;
            this.TargetSquare = targetSquare;
            this.SourceSquare = sourceSquare;
            this.Action = action;

            this.Comments = new ChessCommentCollection();
            this.ChoiceSteps = new ChessStepPairSequenceCollection();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.ChessmanType != Enums.ChessmanType.Pawn)//如果是“兵”，不打印
            {
                sb.Append(this.ChessmanType.ToString());
            }
            sb.Append(this.TargetSquare.ToString());
            sb.Append(' ');
            if (this.Comments.Count > 0)//如果有注释，打印注释
            {
                sb.Append("(");
                foreach (ChessComment comment in this.Comments)
                {
                    sb.Append(comment.ToString()).Append(' ');
                }
                sb.Remove(sb.Length - 1, 1).Append(")");
            }
            sb.Append(' ');
            if (this.ChoiceSteps.Count > 0)//如果有变招，打印变招字符串
            {
                sb.Append("{");
                foreach (ChessStepPairSequence step in this.ChoiceSteps)
                {
                    sb.Append(step.ToString()).Append(' ');
                }
                sb.Remove(sb.Length - 1, 1).Append("}");
            }
            sb.AppendLine();
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return unchecked(
                this.Action.GetHashCode() ^ this.Castling.GetHashCode() ^ this.ChessmanSide.GetHashCode() ^
                this.ChessmanType.GetHashCode() ^ this.ChoiceSteps.GetHashCode() ^ this.Comments.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            ChessStep step = (ChessStep)obj;
            if (step.Castling != this.Castling)
                return false;
            if (step.Action != this.Action)
                return false;
            if (step.ChessmanSide != this.ChessmanSide)
                return false;
            if (step.ChessmanType != this.ChessmanType)
                return false;
            //if (step.ChoiceSteps != this.ChoiceSteps)
            //    return false;
            //if (step.Comments != this.Comments)
            //    return false;
            return true;
        }

        public static ChessStep Parse(string str, Enums.ChessmanSide manSide)
        {
            if (string.IsNullOrEmpty(str) || str.Length < 2)
                throw new ArgumentOutOfRangeException(str);
            str = str.Trim();

            Enums.ChessmanType manType = Enums.ChessmanType.Nothing;
            ChessSquare sourceSquare = new ChessSquare();
            ChessSquare targetSquare = new ChessSquare();
            Enums.AccessorialAction action = Enums.AccessorialAction.General;

            //针对尾部标记符进行一些操作
            ChessRecordFlag flags = new ChessRecordFlag();
            string endString = string.Empty;
            foreach (string flagword in flags)
            {
                if (str.EndsWith(flagword))
                {
                    if (flagword.Equals("+"))
                        action = Enums.AccessorialAction.Check;
                    endString = flagword;
                    int i = str.LastIndexOf(flagword);
                    str = str.Substring(0, i);
                    break;
                }
            }

            Enums.Castling castling = Enums.Castling.None;
            int n = 0;

            if (char.IsUpper(str, 0))
            {//首字母是大写的
                #region Upper
                if (str[0] == 'O')
                {
                    str = str.Trim().Replace(" ", string.Empty);
                    switch (str.Length)
                    {
                        case 3://O-O 短易位
                            castling = Enums.Castling.KingSide;
                            break;
                        case 5://O-O-O 长易位
                            castling = Enums.Castling.QueenSide;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    manType = Enums.ParseChessmanType(str[n]);
                    n++;
                    if (str[n] == 'x')
                    {
                        n++;
                        if (action == Enums.AccessorialAction.Check)
                            action = Enums.AccessorialAction.KillAndCheck;
                        else
                            action = Enums.AccessorialAction.Kill;
                    }
                    targetSquare = ChessSquare.Parse(str.Substring(n, 2));
                }
                #endregion
            }
            else
            {//c5 dxc5 dxc5+ 首字母是小写的，一般就是“兵”的动作
                #region Lower
                manType = Enums.ChessmanType.Pawn;
                switch (str.Length)
                {
                    case 2:
                        targetSquare = ChessSquare.Parse(str);
                        break;
                    case 4:
                        targetSquare = ChessSquare.Parse(str.Substring(str.IndexOf('x')));
                        break;
                    default:
                        break;
                }
                #endregion
            }

            //return new ChessStep
            if (castling == Enums.Castling.None)
                return new ChessStep(manSide, manType, sourceSquare, targetSquare, action);
            else
                return new ChessStep(manSide, castling);
        }

    }
}

/* 国际象棋常采用的记录符号如下：

正式的记谱法是先写棋子的字母.再写由哪格走到哪格.
例如Ng1-f3.表示马由g1格走到f3格.也有简略记谱法是只写目的地.没有来源地.
如Rd3.表示有车走到d3格.当有两个同样棋子可以到达同一个目的地时.则写出来源地的行或者列.如Rad3.
如果吃掉对方的棋子.则在两个位置之间加上x.如Bb5xc6或Bxc6. 

　　在目标格前：- 走子； ：或x吃子
　　在目标格后：+ 将军； ++双将；x或# 将死；e.p.吃过路兵（en passant）；棋子名 升变
　　特殊：0-0短易位；0-0-0长易位
　　国际象棋长采用的评论符号如下：
　　!有利的着法；!!非常有利的着法；?错招；??大错招；!?后果不明的着法；∽走任意一着棋。
　　有时为了简便也常用棋的英文打头字母记录：
　　王 K；后 Q；象 B；马 N；车 R；兵 P（兵可不写）
　　简便记法，在可能的时候不用写棋子的起始位置，不得不写时按“先列后行”的规则，只写其一；“-”通常省略；兵吃子时通常只写两个列号。
　　下面我们列举完整记录和简易记录同一个对局的例子。
　　完整记录
　　白方：
　　黑方：
　　1.c2-e4 d7-e5 2.马g1-f3 f7-f6? 3.马f3:e5! f6:e5? 4.后d1-h5+ 王e8-e7 5.后h5:e5+ 王e7-f7
　　6.象f1-c4+ 王f7-g6?? 7.后e5-f5+ 王g6-h6 8.d2-d4+ g7-g5 9.h2-h4 象f8-e7 10.h4:g5++ 王h6-g7
　　11.后f5-f7 ×
　　简易纪录：
　　白方：
　　黑方：
　　1.e4 e5 2.Nf3 f6? 3.N:e5! f : e 4.Qh5+ Ke7 5.Q:e5+ Ke7 6.Bc4 Kg6?? 7.Qf5+ Kh6 8.d4+ g5
　　9.h4 Be7 10h:g++ Kg7 11.Qf7× 
 */
