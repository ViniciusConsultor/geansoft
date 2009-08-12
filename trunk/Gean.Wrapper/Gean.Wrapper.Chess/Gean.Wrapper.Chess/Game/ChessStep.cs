﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;

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
        public List<Enums.Action> Actions { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的棋子类型
        /// </summary>
        public Enums.ChessmanType ChessmanType { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的棋子战方
        /// </summary>
        public Enums.ChessmanSide ChessmanSide { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的升变后棋子类型
        /// </summary>
        public Enums.ChessmanType PromotionChessmanType { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的源棋格
        /// </summary>
        public ChessPosition SourcePosition
        {
            get { return this._srcPosition; }
            internal set
            {
                this._srcPosition = value;
                if (value != ChessPosition.Empty)
                {
                    this._sameHorizontal = Utility.CharToInt(value.Horizontal);
                    this._sameVertical = value.Vertical;
                }
            }
        }
        private ChessPosition _srcPosition; 
        /// <summary>
        /// 获取或设置该步棋的目标棋格
        /// </summary>
        public ChessPosition TargetPosition { get; internal set; }
        /// <summary>
        /// 获取当前棋步后的Fen记录
        /// </summary>
        public FenNotation Fen { get; internal set; }
        /// <summary>
        /// 有同行与同列的棋子可能产生同样的棋步
        /// </summary>
        public Enums.SameOrientation HasSame
        {
            get { return this._hasSame; }
            internal set { this._hasSame = value; }
        }
        /// <summary>
        /// 有同行与同列的棋子可能产生同样的棋步
        /// </summary>
        private Enums.SameOrientation _hasSame = Enums.SameOrientation.None;
        /// <summary>
        /// 当同 “行” 有棋子可能产生同样的棋步时的值
        /// </summary>
        public int SameHorizontal
        {
            get { return this._sameHorizontal; }
        }
        private int _sameHorizontal;
        /// <summary>
        /// 当同 “列” 有棋子可能产生同样的棋步时的值
        /// </summary>
        public int SameVertical
        {
            get { return this._sameVertical; }
        }
        private int _sameVertical;

        #endregion

        #region ctor

        public ChessStep(Enums.ChessmanSide manSide, Enums.ChessmanType chessmanType, ChessPosition srcPos, ChessPosition tagPos, params Enums.Action[] action)
        {
            this.Actions = new List<Enums.Action>();
            this.Actions.AddRange(action);
            this.ChessmanType = chessmanType;
            this.ChessmanSide = manSide;
            this.TargetPosition = tagPos;
            this.SourcePosition = srcPos;
        }

        #endregion

        #region override

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            #region ToString()
            if ((this.Actions[0] != Enums.Action.KingSideCastling) && (this.Actions[0] != Enums.Action.QueenSideCastling))
            {
                sb.Append(this.TargetPosition.ToString());
            }
            foreach (Enums.Action action in this.Actions)
            {
                switch (action)
                {
                    #region case
                    case Enums.Action.Kill:
                        sb.Insert(0, 'x');
                        break;
                    case Enums.Action.Check:
                        sb.Append('+');
                        break;
                    case Enums.Action.KingSideCastling:
                        sb.Append("O-O");
                        break;
                    case Enums.Action.QueenSideCastling:
                        sb.Append("O-O-O");
                        break;
                    case Enums.Action.General:
                    case Enums.Action.Opennings:
                    case Enums.Action.EnPassant:
                        break;
                    case Enums.Action.Promotion:
                        if (sb.ToString().EndsWith("+"))//fxe8=Q+
                        {
                            string str = "=" + Enums.ChessmanTypeToString(this.PromotionChessmanType);
                            sb.Insert(sb.Length - 1, str);
                        }
                        else
                        {
                            sb.Append('=').Append(Enums.ChessmanTypeToString(this.PromotionChessmanType));
                        }
                        break;
                    case Enums.Action.Invalid:
                    default:
                        break;
                    #endregion
                }
            }
            if ((this.Actions[0] != Enums.Action.KingSideCastling) &&
                (this.Actions[0] != Enums.Action.QueenSideCastling) &&
                (this.ChessmanType != Enums.ChessmanType.Pawn))
            {
                sb.Insert(0, Enums.ChessmanTypeToString(this.ChessmanType));
            }
            else if (this.ChessmanType == Enums.ChessmanType.Pawn && this.Actions.Contains(Enums.Action.Kill))
            {
                if (SourcePosition != ChessPosition.Empty)
                    sb.Insert(0, SourcePosition.Horizontal);
            }
            if (_hasSame != Enums.SameOrientation.None)//是否能使用该棋步的棋子
            {
                if (this.ChessmanType != Enums.ChessmanType.Pawn)
                {
                        switch (_hasSame)
                        {
                            case Enums.SameOrientation.Horizontal:
                                sb.Insert(1, Utility.IntToChar(this.SameHorizontal));
                                break;
                            case Enums.SameOrientation.Vertical:
                                sb.Insert(1, this.SameVertical.ToString());
                                break;
                            case Enums.SameOrientation.None:
                            default:
                                break;
                        }
                }
                else
                {
                    if (!this.Actions.Contains(Enums.Action.Kill))
                    {
                        switch (_hasSame)
                        {
                            case Enums.SameOrientation.Horizontal:
                                sb.Insert(0, Utility.IntToChar(this.SameHorizontal));
                                break;
                            case Enums.SameOrientation.Vertical:
                                sb.Insert(0, this.SameVertical.ToString());
                                break;
                            case Enums.SameOrientation.None:
                            default:
                                break;
                        }
                    }
                }
            }
            #endregion
            return sb.ToString();
        }
        public override int GetHashCode()
        {
            return unchecked
                (3 * (
                this.Actions.GetHashCode() +
                this.ChessmanType.GetHashCode() +
                this.PromotionChessmanType.GetHashCode() +
                this.TargetPosition.GetHashCode() + 
                this.SourcePosition.GetHashCode()
                ));
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            ChessStep step = (ChessStep)obj;
            if (this.ChessmanType != step.ChessmanType)
                return false;
            if (this.ChessmanSide != step.ChessmanSide)
                return false;
            if (!UtilityEquals.CollectionsNoSortedEquals<Enums.Action>(this.Actions, step.Actions))
                return false;
            if (!UtilityEquals.PairEquals(this.TargetPosition, step.TargetPosition))
                return false;
            if (!UtilityEquals.PairEquals(this.SourcePosition, step.SourcePosition))
                return false;
            if (this.PromotionChessmanType != step.PromotionChessmanType)
                return false;
            return true;
        }

        #endregion

        #region static Parse

        /// <summary>
        /// 根据指定的字符串解析.
        /// (该方法逻辑较复杂，日后可优化)Gean: 2009-08-08 23:25:06
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <param name="manSide">棋子的战方（主要是针对兵的源棋格使用）</param>
        /// <returns></returns>
        public static ChessStep Parse(string value, Enums.ChessmanSide manSide)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 2 || value == "\r\n") 
                throw new ArgumentOutOfRangeException(value);
            value = value.Trim();//移除所有前导空白字符和尾部空白字符

            List<Enums.Action> actionList = new List<Enums.Action>();
            Enums.ChessmanType manType = Enums.ChessmanType.None;

            string srcChar = string.Empty;
            string tgtChar = string.Empty;

            bool isPromotion = false;//是否是“升变”的标记
            string promotionString = string.Empty;

            ChessPosition srcPos = ChessPosition.Empty;
            ChessPosition tgtPos = ChessPosition.Empty;

            Enums.SameOrientation hasSame = Enums.SameOrientation.None;
            int sameHorizontal = 0;
            int sameVertical = 0;

            #region 针对尾部标记符进行一些操作,并返回裁剪掉尾部标记符的Value值
            ChessStepFlag flags = new ChessStepFlag();
            string endString = string.Empty;
            foreach (string flagword in flags)
            {
                #region EndsWith(flagword)
                if (value.EndsWith(flagword))
                {
                    if (flagword.Equals("+"))//Qh5+
                        actionList.Add(Enums.Action.Check);
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
                #region

                value = value.Trim().Replace(" ", string.Empty);
                switch (value.Length)
                {
                    case 3://O-O 短易位
                        actionList.Add(Enums.Action.KingSideCastling);
                        break;
                    case 5://O-O-O 长易位
                        actionList.Add(Enums.Action.QueenSideCastling);
                        break;
                    default:
                        break;
                }

                #endregion
                goto END_PARSE_Castling;
            }
            else if ((i = value.IndexOf('=')) >= 2)//如果有等号，该步棋即为升变 fxe8=Q+, e8=Q, cxd8=Q+
            {
                #region

                isPromotion = true;
                actionList.Add(Enums.Action.Promotion);

                promotionString = value.Substring(i + 1);
                value = value.Substring(0, i);

                #endregion
                goto BEGIN_PARSE;
            }
            else if ((i = value.IndexOf('x')) >= 1)//Rfxe8+,Nxa3+;
            {
                #region

                actionList.Add(Enums.Action.Kill);
                ChessStep.ParseSrcPos(value.Substring(0, i), value.Substring(i + 1), manSide,
                                        out manType, out srcPos, out hasSame, out sameHorizontal, out sameVertical);
                value = value.Substring(i + 1);

                #endregion
            }
            else if (value.Length == 3)
            {
                if (char.IsUpper(value[0]))
                {
                    manType = Enums.StringToChessmanType(value[0]);
                    value = value.Remove(0, 1);
                }
                else
                {
                    manType = Enums.ChessmanType.Pawn;
                    hasSame = Enums.SameOrientation.Horizontal;
                    sameHorizontal = Utility.CharToInt(value[0]);
                    sameVertical = int.Parse(value[2].ToString());
                    srcPos = new ChessPosition(sameHorizontal, sameVertical);
                    value = value.Remove(0, 1);
                    goto BEGIN_PARSE;
                }
            }
            else if (value.Length == 4)//Rhb1,R1b7+,Rac2,a8=Q,Rae8+,Ngf3,Nbd7
            {
                ChessStep.ParseSrcPos(value.Substring(0, 2), value.Substring(2), manSide,
                                        out manType, out srcPos, out hasSame, out sameHorizontal, out sameVertical);
                value = value.Substring(2);
            }
            
        END_PARSE:

            tgtPos = ChessPosition.Parse(value);

        END_PARSE_Castling:
            if (actionList.Count == 0)
                actionList.Add(Enums.Action.General);

            ChessStep step = new ChessStep(manSide, manType, srcPos, tgtPos, actionList.ToArray());
            step._hasSame = hasSame;
            step._sameHorizontal = sameHorizontal;
            step._sameVertical = sameVertical;
            if (isPromotion)//如果是升变
                step.PromotionChessmanType = Enums.StringToChessmanType(promotionString);
            return step;
        }

        private static void ParseSrcPos(string before, 
                                        string after, 
                                        Enums.ChessmanSide manSide, 
                                        out Enums.ChessmanType type, 
                                        out ChessPosition pos,
                                        out Enums.SameOrientation hasSame,
                                        out int sameHorizontal,
                                        out int sameVertical)
        {
            pos = ChessPosition.Empty;
            type = Enums.ChessmanType.None;
            hasSame = Enums.SameOrientation.None;
            sameHorizontal = 0;
            sameVertical = 0;
            if (char.IsUpper(before, 0))//首字母是大写的
            {
                type = Enums.StringToChessmanType(before[0]);
                before = before.Remove(0, 1);
            }
            else if (char.IsLower(before, 0))//首字母是小写的
            {
                type = Enums.ChessmanType.Pawn;
            }
            //Parse value, 一般是指“axb5+”，“Rfxe8”，“B3xd6”中的第2个字符的解析
            if (!string.IsNullOrEmpty(before))
            {
                char c = before[0];

                if (c >= 'a' && c <= 'h')
                {
                    hasSame = Enums.SameOrientation.Horizontal;
                    sameHorizontal = Utility.CharToInt(c);
                    sameVertical = int.Parse(after[1].ToString());
                    if (type == Enums.ChessmanType.Pawn)
                    {
                        if (manSide == Enums.ChessmanSide.Black)
                            sameVertical++;
                        else
                            sameVertical--;
                    }
                }
                else
                {
                    hasSame = Enums.SameOrientation.Vertical;
                    sameHorizontal = Utility.CharToInt(after[0]);
                    sameVertical = int.Parse(c.ToString());
                }
                pos = new ChessPosition(sameHorizontal, sameVertical);
            }
        }

        #endregion
    }
}