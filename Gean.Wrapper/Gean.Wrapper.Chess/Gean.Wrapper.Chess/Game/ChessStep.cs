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
            if (string.IsNullOrEmpty(value) || value.Length < 2)
                throw new ArgumentOutOfRangeException(value);
            value = value.Trim();

            Enums.Action action = Enums.Action.General;
            Enums.ChessmanType manType = Enums.ChessmanType.None;
            ChessGrid rid = ChessGrid.Empty;

            //针对尾部标记符进行一些操作
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
                    int i = value.LastIndexOf(flagword);
                    value = value.Substring(0, i);
                    break;
                }
                #endregion
            }

            ChessPosition srcPos = ChessPosition.Empty;
            if (value.Contains("="))//升变
            {
                return new ChessStep(action, Enums.ChessmanType.Pawn, srcPos, new ChessPosition(1, 1));
            }
            else
            {
                if (char.IsUpper(value, 0))//首字母是大写的
                {
                    #region Upper
                    if (value[0] == 'O')
                    {
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
                    }
                    else
                    {
                        manType = Enums.StringToChessmanType(value[0]);
                        if (value.Length > 3 && value.Contains("x")) //Rxe3,Rfxe3;
                        {
                            if (action == Enums.Action.Check)
                                action = Enums.Action.KillAndCheck;
                            else
                                action = Enums.Action.Kill;
                            if (value[1] == 'x')
                            {
                                rid = ChessGrid.Parse(value.Substring(2, 2));
                            }
                            else
                            {
                                char c = value[1];
                                int i;
                                if (c >= 'a' && c <= 'h')
                                    i = Utility.CharToInt(c);
                                else
                                    i = int.Parse(c.ToString());
                                srcPos = new ChessPosition(i, int.Parse(value[4].ToString()));
                                rid = ChessGrid.Parse(value.Substring(3, 2));
                            }
                        }
                        else if (value.Length > 3 && !value.Contains("x"))//Rfe3
                        {
                            char c = value[1];
                            int i;
                            if (c >= 'a' && c <= 'h')
                                i = Utility.CharToInt(c);
                            else
                                i = int.Parse(c.ToString());
                            srcPos = new ChessPosition(i, int.Parse(value[3].ToString()));
                            rid = ChessGrid.Parse(value.Substring(2, 2));
                        }
                        else if (value.Length == 3)//Re3
                        {
                            rid = ChessGrid.Parse(value.Substring(1, 2));
                        }
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
                            rid = ChessGrid.Parse(value);
                            break;
                        case 4:
                            rid = ChessGrid.Parse(value.Substring(value.IndexOf('x') + 1));
                            char cx = value.Substring(0, 1).ToCharArray()[0];
                            int iy = int.Parse(value.Substring(3, 1));
                            if (manSide == Enums.ChessmanSide.White)
                                iy--;
                            else
                                iy++;
                            srcPos = new ChessPosition(Utility.CharToInt(cx), iy);
                            if (action == Enums.Action.Check)
                                action = Enums.Action.KillAndCheck;
                            else
                                action = Enums.Action.Kill;
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
            }
            ChessPosition newPos;
            if (action == Enums.Action.KingSideCastling || action == Enums.Action.QueenSideCastling)
                newPos = ChessPosition.Empty;
            else
                newPos = new ChessPosition(rid.X, rid.Y);
            return new ChessStep(action, manType, srcPos, newPos);
        }

        #endregion
    }
}