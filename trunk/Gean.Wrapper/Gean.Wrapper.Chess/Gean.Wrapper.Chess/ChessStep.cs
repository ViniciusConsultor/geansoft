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

        #region Property

        /// <summary>
        /// 获取或设置一步棋的动作说明
        /// </summary>
        public Enums.AccessorialAction Action { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的棋子类型
        /// </summary>
        public Enums.ChessmanType ChessmanType { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的目标棋格
        /// </summary>
        public ChessSquare TargetSquare { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的源棋格
        /// </summary>
        public ChessSquare SourceSquare { get; internal set; }

        /// <summary>
        /// 获取或设置该棋步是“王车易位”
        /// </summary>
        public Enums.Castling Castling 
        {
            get { return this._castling; }
            internal set { this._castling = value; } 
        }
        private Enums.Castling _castling;

        /// <summary>
        /// 获取或设置该步棋的注释的索引集合
        /// </summary>
        public int[] CommentIndexs
        {
            get { return this._commentIndexs.ToArray(); }
        }
        private List<int> _commentIndexs = new List<int>();

        /// <summary>
        /// 获取或设置该步棋的变招的索引集合
        /// </summary>
        public int[] ChoiceStepsIndexs
        { 
            get { return this._choiceStepIndexs.ToArray(); } 
        }
        private List<int> _choiceStepIndexs = new List<int>();

        #endregion

        #region 构造函数

        public ChessStep() : this(Enums.Castling.None) { }
        public ChessStep(Enums.Castling castling)
            : this(castling, Enums.ChessmanType.None, Enums.AccessorialAction.None, ChessSquare.Empty, ChessSquare.Empty)
        {
            //this
        }
        public ChessStep(Enums.ChessmanType manType, Enums.AccessorialAction action, ChessSquare sourceSquare, ChessSquare targetSquare)
            : this(Enums.Castling.None, manType, action, sourceSquare, targetSquare)
        {
            //this
        }
        public ChessStep(Enums.Castling castling, Enums.ChessmanType manType, Enums.AccessorialAction action, ChessSquare sourceSquare, ChessSquare targetSquare)
        {
            this._castling = castling;

            this.Action = action;
            this.ChessmanType = manType;

            this.TargetSquare = targetSquare;
            this.SourceSquare = sourceSquare;
        }

        #endregion

        #region override

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            switch (this._castling)
            {
                #region case
                case Enums.Castling.KingSide://短易位
                    sb.Append("O-O");
                    break;
                case Enums.Castling.QueenSide://长易位
                    sb.Append("O-O-O");
                    break;
                case Enums.Castling.None://不是易位
                    {
                        if (this.ChessmanType != Enums.ChessmanType.Pawn)//如果是“兵”，不打印
                        {
                            sb.Append(Enums.ChessmanTypeToString(this.ChessmanType));
                        }
                        sb.Append(this.TargetSquare.ToString());
                        sb.Append(' ');
                        break;
                    }
                #endregion
            }
            if (this._commentIndexs.Count > 0)//如果有注释，打印注释
            {
                sb.Append('(');
                foreach (int index in _commentIndexs)
                {
                    sb.Append(index.ToString()).Append(',');
                }
                sb.Remove(sb.Length - 1, 1).Append(')');
            }
            if (this.ChoiceStepsIndexs.Length > 0)//如果有变招，打印变招字符串
            {
                sb.Append("[");
                foreach (int index in this.ChoiceStepsIndexs)
                {
                    sb.Append(index.ToString()).Append(',');
                }
                sb.Remove(sb.Length - 1, 1).Append("]");
            }
            return sb.ToString().Trim();
        }

        public override int GetHashCode()
        {
            return unchecked
                (3 *
                this.Action.GetHashCode() +
                this.ChessmanType.GetHashCode() +
                this.TargetSquare.GetHashCode() + this.SourceSquare.GetHashCode() +
                this.Castling.GetHashCode() +
                this.CommentIndexs.GetHashCode() + this.ChoiceStepsIndexs.GetHashCode()
                );
        }

        public override bool Equals(object obj)
        {
            ChessStep step = (ChessStep)obj;

            if (this.Castling       != step.Castling)       return false;
            if (this.ChessmanType   != step.ChessmanType)   return false;
            if (this.Action         != step.Action)         return false;

            if (!UtilityEquals.PairEquals(this.TargetSquare, step.TargetSquare))
                return false;
            if (!UtilityEquals.PairEquals(this.SourceSquare, step.SourceSquare))
                return false;

            if (!UtilityEquals.ListEquals(this.ChoiceStepsIndexs, step.ChoiceStepsIndexs))
                return false;
            if (!UtilityEquals.ListEquals(this.CommentIndexs, step.CommentIndexs))
                return false;
            return true;
        }

        #endregion

        #region static Parse

        public static ChessStep Parse(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 2)
                throw new ArgumentOutOfRangeException(value);
            value = value.Trim();

            #region 解析注释与变招的索引

            #endregion

            Enums.AccessorialAction action = Enums.AccessorialAction.General;
            Enums.ChessmanType manType = Enums.ChessmanType.None;
            ChessSquare square = ChessSquare.Empty;

            //针对尾部标记符进行一些操作
            ChessRecordFlag flags = new ChessRecordFlag();
            string endString = string.Empty;
            foreach (string flagword in flags)
            {
                if (value.EndsWith(flagword))
                {
                    if (flagword.Equals("+"))//Qh5+
                        action = Enums.AccessorialAction.Check;
                    endString = flagword;
                    int i = value.LastIndexOf(flagword);
                    value = value.Substring(0, i);
                    break;
                }
            }

            Enums.Castling castling = Enums.Castling.None;
            int n = 0;

            if (char.IsUpper(value, 0))
            {//首字母是大写的
                #region Upper
                if (value[0] == 'O')
                {
                    value = value.Trim().Replace(" ", string.Empty);
                    switch (value.Length)
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
                    manType = Enums.StringToChessmanType(value[n]);
                    n++;
                    if (value[n] == 'x')
                    {
                        n++;
                        if (action == Enums.AccessorialAction.Check)
                            action = Enums.AccessorialAction.KillAndCheck;
                        else
                            action = Enums.AccessorialAction.Kill;
                    }
                    square = ChessSquare.Parse(value.Substring(n, 2));
                }
                #endregion
            }
            else
            {//c5 dxc5 dxc5+ 首字母是小写的，一般就是“兵”的动作
                #region Lower
                manType = Enums.ChessmanType.Pawn;
                switch (value.Length)
                {
                    case 2:
                        square = ChessSquare.Parse(value);
                        break;
                    case 4:
                        square = ChessSquare.Parse(value.Substring(value.IndexOf('x')));
                        break;
                    default:
                        break;
                }
                #endregion
            }

            //return new ChessStep
            if (castling == Enums.Castling.None)
                return new ChessStep(manType, action, ChessSquare.Empty, square);
            else
                return new ChessStep(castling);
        }

        #endregion

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
