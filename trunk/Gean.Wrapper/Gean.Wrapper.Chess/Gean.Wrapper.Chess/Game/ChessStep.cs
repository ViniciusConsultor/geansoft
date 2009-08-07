using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述棋局中的单方的一步棋。如："Nc6"代表马走到c6格。
    /// 两方各一步棋组成一个棋招<see>ChessStepPair</see>。
    /// 对于这步棋，绑定了一个注释的集合，一个变招的集合（变招也是每一步棋的集合）。
    /// </summary>
    public class ChessStep
    {
        #region Property

        /// <summary>
        /// 获取或设置一步棋的动作说明
        /// </summary>
        public Enums.Action Action { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的棋子类型
        /// </summary>
        public Enums.ChessmanType ChessmanType { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的升变后棋子类型
        /// </summary>
        public Enums.ChessmanType PromotionChessmanType { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的源棋格
        /// </summary>
        public ChessPosition SourcePosition { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的目标棋格
        /// </summary>
        public ChessPosition TargetPosition { get; internal set; }

        #endregion

        #region ctor

        public ChessStep(Enums.Action action, Enums.ChessmanType chessmanType, ChessPosition srcPos, ChessPosition tagPos)
        {
            this.Action = action;
            this.ChessmanType = chessmanType;
            this.TargetPosition = tagPos;
            this.SourcePosition = srcPos;
        }

        #endregion

        #region override

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            switch (this.Action)
            {
                #region case
                case Enums.Action.KingSideCastling://短易位
                    sb.Append("O-O");
                    break;
                case Enums.Action.QueenSideCastling://长易位
                    sb.Append("O-O-O");
                    break;
                //不是易位
                case Enums.Action.Check:
                case Enums.Action.General:
                case Enums.Action.Kill:
                case Enums.Action.KillAndCheck:
                case Enums.Action.Opennings:
                case Enums.Action.Invalid:
                    {
                        if (this.ChessmanType != Enums.ChessmanType.Pawn)//如果是“兵”，不打印
                        {
                            sb.Append(Enums.ChessmanTypeToString(this.ChessmanType));
                        }
                        if (Enums.GetFlag(this.Action, Enums.Action.Check) == Enums.Action.Kill)
                        {
                            if (this.ChessmanType == Enums.ChessmanType.Pawn)//如果有子被杀死，列出兵的位置
                            {
                                sb.Append(this.SourcePosition.Horizontal);
                            }
                            sb.Append('x');
                        }
                        sb.Append(this.TargetPosition.ToString());
                        //有将军的动作，打印'+'
                        if (Enums.GetFlag(this.Action, Enums.Action.Kill) == Enums.Action.Check)
                        {
                            sb.Append('+');
                        }
                        break;
                    }
                #endregion
            }
            return sb.ToString().Trim();
        }
        public override int GetHashCode()
        {
            return unchecked
                (3 * (
                this.Action.GetHashCode() +
                this.ChessmanType.GetHashCode() +
                this.TargetPosition.GetHashCode() + this.SourcePosition.GetHashCode()
                ));
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            ChessStep step = (ChessStep)obj;
            if (this.Action != step.Action)
                return false;
            if (this.ChessmanType != step.ChessmanType)
                return false;
            if (!UtilityEquals.PairEquals(this.TargetPosition, step.TargetPosition))
                return false;
            if (!UtilityEquals.PairEquals(this.SourcePosition, step.SourcePosition))
                return false;
            return true;
        }

        #endregion

        #region static Parse

        public static ChessStep Parse(string value, Enums.ChessmanSide manSide)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 2) throw new ArgumentOutOfRangeException(value);
            value = value.Trim();//移除所有前导空白字符和尾部空白字符

            Enums.Action action = Enums.Action.General;
            Enums.ChessmanType manType = Enums.ChessmanType.None;

            string srcChar = string.Empty;
            string tgtChar = string.Empty;

            bool isPromotion = false;//是否是“升变”的标记
            string promotionString = string.Empty;

            ChessPosition srcPos = ChessPosition.Empty;
            ChessPosition tgtPos = ChessPosition.Empty;

            #region 针对尾部标记符进行一些操作,并返回裁剪掉尾部标记符的Value值
            ChessStepFlag flags = new ChessStepFlag();
            string endString = string.Empty;
            foreach (string flagword in flags)
            {
                #region EndsWith(flagword)
                if (value.EndsWith(flagword))
                {
                    if (flagword.Equals("+"))//Qh5+
                        action = Enums.Action.Check;
                    endString = flagword;
                    value = value.Substring(0, value.LastIndexOf(flagword));//裁剪掉尾部标记符
                    break;
                }
                #endregion
            }
            #endregion

        BEGIN_PARSE:
            int i;
            if (value.Length == 2)//如仅是单坐标，仅为兵的动作：d4, f6, c4, g6, c3, d6
            {
                #region
                manType = Enums.ChessmanType.Pawn;
                #endregion
                goto END_PARSE;
            }
            else if (value[0] == 'O')//Castling, 王车易位
            {
                #region Castling, 王车易位
                value = value.Trim().Replace(" ", string.Empty);
                switch (value.Length)
                {
                    case 3://O-O 短易位
                        action = Enums.Action.KingSideCastling;
                        break;
                    case 5://O-O-O 长易位
                        action = Enums.Action.QueenSideCastling;
                        break;
                    default:
                        break;
                }
                #endregion
                goto END_PARSE_Castling;
            }
            else if ((i = value.IndexOf('=')) >= 2)//如果有等号，该步棋即为升变 Rfxe8=Q+, e8=Q, cxd8=Q+
            {
                #region

                isPromotion = true;
                action = Enums.Action.Promotion;

                value = value.Substring(0, i);
                promotionString = value.Substring(i);

                #endregion
                goto BEGIN_PARSE;
            }
            else if ((i = value.IndexOf('x')) >= 1)//Rfxe8+,Nxa3+;
            {
                if (action == Enums.Action.Check)
                    action = Enums.Action.KillAndCheck;
                else
                    action = Enums.Action.Kill;
                ChessStep.ParseSrcPos(value.Substring(0, i), value.Substring(i), out manType, out srcPos);
                value = value.Substring(i + 1);
            }

        END_PARSE:

            tgtPos = ChessPosition.Parse(value);

        END_PARSE_Castling:

            ChessStep step = new ChessStep(action, manType, srcPos, tgtPos);
            if (isPromotion)//如果是升变
                step.PromotionChessmanType = Enums.StringToChessmanType(promotionString);
            return step;
        }

        private static void ParseSrcPos(string mainValue, string tgtValue, out Enums.ChessmanType type, out ChessPosition pos)
        {
            pos = ChessPosition.Empty;
            type = Enums.ChessmanType.None;
            if (char.IsUpper(mainValue, 0))//首字母是大写的
            {
                type = Enums.StringToChessmanType(mainValue[0]);
                mainValue = mainValue.Remove(0, 1);
            }
            else if (!char.IsUpper(mainValue, 0))//首字母是小写的
            {
                type = Enums.ChessmanType.Pawn;
            }
            //Parse value, 一般是指“Rfxe8”，“B3xd6”中的第2个字符的解析
            if (!string.IsNullOrEmpty(mainValue))
            {
                int x;
                int y;
                char c = mainValue[0];

                if (c >= 'a' && c <= 'h')
                {
                    x = Utility.CharToInt(c);
                    y = int.Parse(tgtValue[1].ToString());
                }
                else
                {
                    x = int.Parse(c.ToString());
                    y = Utility.CharToInt(tgtValue[0]);
                }
                pos = new ChessPosition(x, y);
            }
        }

        #endregion
    }
}