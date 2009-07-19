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
        public Square TargetSquare { get; set; }
        /// <summary>
        /// 获取或设置该步棋的源棋格
        /// </summary>
        public Square SourceSquare { get; set; }
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
        }

        public ChessStep(Enums.ChessmanSide manSide, Enums.ChessmanType manType, Square targetSquare, Square sourceSquare, Enums.AccessorialAction action)
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
            //if (this.Chessman.ChessmanType != Enums.ChessmanType.Pawn)//如果是“兵”，不打印
            //{
            //    sb.Append(this.Chessman.ToSimpleString());
            //}
            //sb.Append(this.TargetSquare.ToString());
            //sb.Append(' ');
            //if (this.Comments.Count > 0)//如果有注释，打印注释
            //{
            //    sb.Append("(");
            //    foreach (ChessComment comment in this.Comments)
            //    {
            //        sb.Append(comment.ToString()).Append(' ');
            //    }
            //    sb.Remove(sb.Length - 1, 1).Append(")");
            //}
            //sb.Append(' ');
            //if (this.ChoiceSteps.Count > 0)//如果有变招，打印变招字符串
            //{
            //    sb.Append("{");
            //    foreach (ChessStepPairSequence step in this.ChoiceSteps)
            //    {
            //        sb.Append(step.ToString()).Append(' ');
            //    }
            //    sb.Remove(sb.Length - 1, 1).Append("}");
            //}
            //sb.AppendLine();
            return sb.ToString();
        }

        public static ChessStep Parse(string str, Enums.ChessmanSide manSide)
        {
            if (string.IsNullOrEmpty(str) || str.Length < 2)
                throw new ArgumentOutOfRangeException(str);

            Enums.ChessmanType manType = Enums.ChessmanType.Nothing;
            Square sourceSquare = new Square();
            Square targetSquare = new Square();
            Enums.AccessorialAction action = Enums.AccessorialAction.General;

            if (str.EndsWith("+"))
                action = Enums.AccessorialAction.Check;

            Enums.Castling castling = Enums.Castling.None;
            int n = 0;

            if (char.IsUpper(str, 0))
            {//首字母是大写的
                if (str[0] == 'O')
                {
                    switch (str.Length)
                    {
                        case 3://O-O 短易位
                            castling = Enums.Castling.KingSide;
                            goto CASTLING;
                        case 5://O-O-O 长易位
                            castling = Enums.Castling.QueenSide;
                            goto CASTLING; 
                        default:
                            break;
                    }
                }
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
                targetSquare = Square.Parse(str.Substring(n, 2));
            }
            else
            {//c5 dxc5 首字母是小写的，一般就是“兵”的动作
                manType = Enums.ChessmanType.Pawn;

            }

        CASTLING:
            //return new ChessStep
            if (castling == Enums.Castling.None)
                return new ChessStep(manSide, manType, sourceSquare, targetSquare, action);
            else
                return new ChessStep(manSide, castling);
        }

    }
}

/*
正式的记谱法是先写棋子的字母.再写由哪格走到哪格.
例如Ng1-f3.表示马由g1格走到f3格.也有简略记谱法是只写目的地.没有来源地.
如Rd3.表示有车走到d3格.当有两个同样棋子可以到达同一个目的地时.则写出来源地的行或者列.如Rad3.
如果吃掉对方的棋子.则在两个位置之间加上x.如Bb5xc6或Bxc6. 
*/