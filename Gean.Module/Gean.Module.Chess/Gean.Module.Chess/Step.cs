﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Gean.Module.Chess
{
    /// <summary>
    /// 描述棋局中的单方的一步棋。如："Nc6"代表马走到c6格。
    /// 对于这步棋，绑定了一个注释的集合，一个变招的集合（变招也是每一步棋的集合）。
    /// </summary>
    [Serializable]
    public class Step : MarshalByRefObject, ITree, IItem, IParse, IGenerator, ICloneable, ISerializable
    {
        #region Property

        /// <summary>
        /// 回合编号
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 获取或设置该步棋的棋子战方
        /// </summary>
        public Enums.GameSide GameSide { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的棋子类型
        /// </summary>
        public Enums.PieceType PieceType { get; internal set; }
        /// <summary>
        /// 获取或设置一步棋的动作说明
        /// </summary>
        public List<Enums.Action> Actions { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的升变后棋子类型
        /// </summary>
        public Enums.PieceType PromotionPieceType { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的源棋格
        /// </summary>
        public Position SourcePosition
        {
            get { return this._srcPosition; }
            internal set
            {
                this._srcPosition = value;
                if (value != Position.Empty)
                {
                    this._sameHorizontal = Utility.CharToInt(value.Horizontal);
                    this._sameVertical = value.Vertical;
                }
            }
        }
        [NonSerialized]
        private Position _srcPosition; 
        /// <summary>
        /// 获取或设置该步棋的目标棋格
        /// </summary>
        public Position TargetPosition { get; internal set; }
        /// <summary>
        /// 有同行与同列的棋子可能产生同样的棋步
        /// </summary>
        public Enums.Orientation HasSame
        {
            get { return this._hasSame; }
            internal set { this._hasSame = value; }
        }
        [NonSerialized]
        private Enums.Orientation _hasSame = Enums.Orientation.None;
        /// <summary>
        /// 当同 “行” 有棋子可能产生同样的棋步时的值
        /// </summary>
        public int SameHorizontal
        {
            get { return this._sameHorizontal; }
        }
        [NonSerialized]
        private int _sameHorizontal;
        /// <summary>
        /// 当同 “列” 有棋子可能产生同样的棋步时的值
        /// </summary>
        public int SameVertical
        {
            get { return this._sameVertical; }
        }
        [NonSerialized]
        private int _sameVertical;

        #endregion

        #region ctor

        public Step(int number,
                         Enums.PieceType chessmanType, 
                         Position srcPos, 
                         Position tagPos, 
                         params Enums.Action[] action)
        {
            this.Number = number;
            this.PieceType = chessmanType;
            this.SourcePosition = srcPos;
            this.TargetPosition = tagPos;
            this.Actions = new List<Enums.Action>();
            this.Actions.AddRange(action);
        }

        #endregion

        #region IItem

        public string Value
        {
            get { return this.ToString(); }
        }

        #endregion

        #region ITree

        public object Parent { get; set; }

        public IList<IItem> Items { get; set; }

        public bool HasChildren
        {
            get
            {
                if (this.Items == null) return false;
                if (this.Items.Count <= 0) return false;
                return true;
            }
        }

        #endregion

        #region IParse

        public void Parse(string value)
        {
        //    if (string.IsNullOrEmpty(value))
        //        throw new ArgumentNullException();
        //    value = value.Trim();//移除所有前导空白字符和尾部空白字符
        //    if (value.Length < 2 || value == "\r\n")
        //        throw new ArgumentOutOfRangeException(value);

        //    List<Enums.Action> actionList = new List<Enums.Action>();
        //    Enums.PieceType manType = Enums.PieceType.None;

        //    string srcChar = string.Empty;
        //    string tgtChar = string.Empty;

        //    bool isPromotion = false;//是否是“升变”的标记
        //    string promotionString = string.Empty;

        //    Position srcPos = Position.Empty;
        //    Position tgtPos = Position.Empty;

        //    Enums.Orientation hasSame = Enums.Orientation.None;
        //    int sameHorizontal = 0;
        //    int sameVertical = 0;

        //    #region 针对尾部标记符进行一些操作,并返回裁剪掉尾部标记符的Value值
        //    Flag flags = new Flag();
        //    string endString = string.Empty;
        //    foreach (string flagword in flags)
        //    {
        //        #region EndsWith(flagword)
        //        if (value.EndsWith(flagword))
        //        {
        //            if (flagword.Equals("+"))//Qh5+
        //                actionList.Add(Enums.Action.Check);
        //            endString = flagword;
        //            value = value.Substring(0, value.LastIndexOf(flagword));//裁剪掉尾部标记符
        //            break;
        //        }
        //        #endregion
        //    }
        //    #endregion

        //BEGIN_PARSE:
        //    int i;
        //    if (value.Length == 2)//如仅是单坐标，仅为兵的动作：d4, f6, c4, g6, c3, d6
        //    {
        //        #region

        //        manType = Enums.PieceType.AllPawns;

        //        #endregion
        //        goto END_PARSE;
        //    }
        //    else if (value[0] == 'O')//Castling, 王车易位
        //    {
        //        #region

        //        value = value.Trim().Replace(" ", string.Empty);
        //        switch (value.Length)
        //        {
        //            case 3://O-O 短易位
        //                actionList.Add(Enums.Action.KingSideCastling);
        //                break;
        //            case 5://O-O-O 长易位
        //                actionList.Add(Enums.Action.QueenSideCastling);
        //                break;
        //            default:
        //                break;
        //        }

        //        #endregion
        //        goto END_PARSE_Castling;
        //    }
        //    else if ((i = value.IndexOf('=')) >= 2)//如果有等号，该步棋即为升变 fxe8=Q+, e8=Q, cxd8=Q+
        //    {
        //        #region

        //        isPromotion = true;
        //        actionList.Add(Enums.Action.Promotion);

        //        promotionString = value.Substring(i + 1);
        //        value = value.Substring(0, i);

        //        #endregion
        //        goto BEGIN_PARSE;
        //    }
        //    else if ((i = value.IndexOf('x')) >= 1)//Rfxe8+,Nxa3+;
        //    {
        //        #region

        //        actionList.Add(Enums.Action.Capture);
        //        Step.ParseSrcPos(value.Substring(0, i), value.Substring(i + 1), manSide,
        //                                out manType, out srcPos, out hasSame, out sameHorizontal, out sameVertical);
        //        value = value.Substring(i + 1);

        //        #endregion
        //    }
        //    else if (value.Length == 3)
        //    {
        //        if (char.IsUpper(value[0]))
        //        {
        //            manType = Enums.ToPieceType(value[0]);
        //            value = value.Remove(0, 1);
        //        }
        //        else
        //        {
        //            manType = Enums.PieceType.WhitePawn;
        //            hasSame = Enums.Orientation.Rank;
        //            sameHorizontal = Utility.CharToInt(value[0]);
        //            sameVertical = int.Parse(value[2].ToString());
        //            srcPos = new Position(sameHorizontal, sameVertical);
        //            value = value.Remove(0, 1);
        //            goto BEGIN_PARSE;
        //        }
        //    }
        //    else if (value.Length == 4)//Rhb1,R1b7+,Rac2,a8=Q,Rae8+,Ngf3,Nbd7
        //    {
        //        Step.ParseSrcPos(value.Substring(0, 2), value.Substring(2), manSide,
        //                                out manType, out srcPos, out hasSame, out sameHorizontal, out sameVertical);
        //        value = value.Substring(2);
        //    }

        //END_PARSE:

        //    tgtPos = Position.Parse(value);

        //END_PARSE_Castling:
        //    if (actionList.Count == 0)
        //        actionList.Add(Enums.Action.General);

        //    Step step = new Step(number, manType, srcPos, tgtPos, actionList.ToArray());
        //    step._hasSame = hasSame;
        //    step._sameHorizontal = sameHorizontal;
        //    step._sameVertical = sameVertical;
        //    if (isPromotion)//如果是升变
        //        step.PromotionPieceType = Enums.ToPieceType(promotionString);
        //    return step;
        }

        #endregion

        #region IGenerator

        public string Generator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISerializable

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region override

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(12);
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
                    case Enums.Action.Capture:
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
                            string str = "=" + Enums.FromPieceType(this.PromotionPieceType);
                            sb.Insert(sb.Length - 1, str);
                        }
                        else
                        {
                            sb.Append('=').Append(Enums.FromPieceType(this.PromotionPieceType));
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
                (this.PieceType != Enums.PieceType.AllPawns))
            {
                sb.Insert(0, Enums.FromPieceType(this.PieceType));
            }
            else if ((this.PieceType == Enums.PieceType.AllPawns) && this.Actions.Contains(Enums.Action.Capture))
            {
                if (SourcePosition != Position.Empty)
                    sb.Insert(0, SourcePosition.Horizontal);
            }
            if (_hasSame != Enums.Orientation.None)//是否能使用该棋步的棋子
            {
                if (this.PieceType != Enums.PieceType.AllPawns)
                {
                        switch (_hasSame)
                        {
                            case Enums.Orientation.Rank:
                                sb.Insert(1, Utility.IntToChar(this.SameHorizontal));
                                break;
                            case Enums.Orientation.File:
                                sb.Insert(1, this.SameVertical.ToString());
                                break;
                            case Enums.Orientation.None:
                            default:
                                break;
                        }
                }
                else
                {
                    if (!this.Actions.Contains(Enums.Action.Capture))
                    {
                        switch (_hasSame)
                        {
                            case Enums.Orientation.Rank:
                                sb.Insert(0, Utility.IntToChar(this.SameHorizontal));
                                break;
                            case Enums.Orientation.File:
                                sb.Insert(0, this.SameVertical.ToString());
                                break;
                            case Enums.Orientation.None:
                            default:
                                break;
                        }
                    }
                }
            }
            if (this.GameSide == Enums.GameSide.White)
            {
                sb.Insert(0, this.Number.ToString() + ". ");
            }
            #endregion
            return sb.ToString();
        }
        public override int GetHashCode()
        {
            return unchecked
                (3 * (
                this.Number.GetHashCode() +
                this.Actions.GetHashCode() +
                this.PieceType.GetHashCode() +
                this.PromotionPieceType.GetHashCode() +
                this.TargetPosition.GetHashCode() + 
                this.SourcePosition.GetHashCode()
                ));
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            Step step = (Step)obj;
            if (this.Number != step.Number)
                return false;
            if (this.PieceType != step.PieceType)
                return false;
            if (this.GameSide != step.GameSide)
                return false;
            if (!UtilityEquals.CollectionsNoSortedEquals<Enums.Action>(this.Actions, step.Actions))
                return false;
            if (!UtilityEquals.PairEquals(this.TargetPosition, step.TargetPosition))
                return false;
            if (!UtilityEquals.PairEquals(this.SourcePosition, step.SourcePosition))
                return false;
            if (this.PromotionPieceType != step.PromotionPieceType)
                return false;
            return true;
        }

        #endregion

        #region step flag

        class Flag : IEnumerable<string>
        {
            private static Dictionary<string, string> _flags = new Dictionary<string, string>();

            public Flag()
            {
                if (_flags.Count <= 0)
                {
                    _flags.Add("Move", this.Move);
                    _flags.Add("Capture", this.Capture);
                    _flags.Add("Check", this.Check);
                    _flags.Add("DoubleCheck", this.DoubleCheck);
                    _flags.Add("Checkmate1", this.Checkmate1);
                    _flags.Add("Checkmate2", this.Checkmate2);
                    _flags.Add("EnPassant", this.EnPassant);
                    _flags.Add("Favorable", this.Favorable);
                    _flags.Add("FavorablePro", this.FavorablePro);
                    _flags.Add("Misestimate", this.Misestimate);
                    _flags.Add("MisestimatePro", this.MisestimatePro);
                    _flags.Add("UnknownConsequences", this.UnknownConsequences);
                }
            }

            /* 简易评论标记符
              在目标格坐标前注明：[-] 走子； [:] or [x] 吃子
              在目标格坐标后注明：[+] 将军； [++] 双将；[x]or[#] 将死；[e.p.] 吃过路兵(en passant)；[棋子名] 升变
            */

            /// <summary>
            /// 目标格坐标前：走子
            /// </summary>
            public string Move { get { return "-"; } }
            /// <summary>
            /// 目标格坐标前：吃子
            /// </summary>
            public string Capture { get { return "x"; } }
            /// <summary>
            /// 目标格坐标后：将军
            /// </summary>
            public string Check { get { return "+"; } }
            /// <summary>
            /// 目标格坐标后：双将
            /// </summary>
            public string DoubleCheck { get { return "++"; } }
            /// <summary>
            /// 目标格坐标后：将死
            /// </summary>
            public string Checkmate1 { get { return "x"; } }
            /// <summary>
            /// 目标格坐标后：将死
            /// </summary>
            public string Checkmate2 { get { return "#"; } }
            /// <summary>
            /// 目标格坐标后：吃过路兵
            /// </summary>
            public string EnPassant { get { return "e.p."; } }

            /// <summary>
            /// 好棋。有利的棋招
            /// </summary>
            public string Favorable { get { return "!"; } }
            /// <summary>
            /// 好棋。非常有利的棋招
            /// </summary>
            public string FavorablePro { get { return "!!"; } }
            /// <summary>
            /// 昏招
            /// </summary>
            public string Misestimate { get { return "?"; } }
            /// <summary>
            /// 严重的昏招
            /// </summary>
            public string MisestimatePro { get { return "??"; } }
            /// <summary>
            /// 后果不明的棋招
            /// </summary>
            public string UnknownConsequences { get { return "!?"; } }

            #region IEnumerable<string> 成员

            public IEnumerator<string> GetEnumerator()
            {
                return _flags.Values.GetEnumerator();
            }

            #endregion

            #region IEnumerable 成员

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return _flags.Values.GetEnumerator();
            }

            #endregion
        }

        #endregion

        #region static Parse

        /// <summary>
        /// 该方法暂不可用。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Step[] Parse(int number, string value)
        {
            string[] steps = value.Split(' ');

            Step white = null;
            Step black = null;
            for (int i = 0; i < steps.Length; i++)
            {
                if (string.IsNullOrEmpty(steps[i]))
                    continue;
                if ((steps[i].StartsWith("(") && steps[i].EndsWith(")")) ||
                    (steps[i].StartsWith("[") && steps[i].EndsWith("]")))
                {
                    //TODO!!!!!!
                }
                if (white == null)
                    white = Step.Parse(number, steps[i], Enums.GameSide.White);
                else if (black == null)
                    black = Step.Parse(number, steps[i], Enums.GameSide.Black);
            }

            return new Step[] { white, black };
        }

        /// <summary>
        /// 根据指定的字符串解析.
        /// (该方法逻辑较复杂，日后可优化)Gean: 2009-08-08 23:25:06
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <param name="manSide">棋子的战方（主要是针对兵的源棋格使用）</param>
        /// <returns></returns>
        public static Step Parse(int number, string value, Enums.GameSide manSide)
        {
            if (string.IsNullOrEmpty(value)) 
                throw new ArgumentNullException();
            value = value.Trim();//移除所有前导空白字符和尾部空白字符
            if (value.Length < 2 || value == "\r\n")
                throw new ArgumentOutOfRangeException(value);

            List<Enums.Action> actionList = new List<Enums.Action>();
            Enums.PieceType manType = Enums.PieceType.None;

            string srcChar = string.Empty;
            string tgtChar = string.Empty;

            bool isPromotion = false;//是否是“升变”的标记
            string promotionString = string.Empty;

            Position srcPos = Position.Empty;
            Position tgtPos = Position.Empty;

            Enums.Orientation hasSame = Enums.Orientation.None;
            int sameHorizontal = 0;
            int sameVertical = 0;

            #region 针对尾部标记符进行一些操作,并返回裁剪掉尾部标记符的Value值
            Flag flags = new Flag();
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

                manType = Enums.PieceType.AllPawns;

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

                actionList.Add(Enums.Action.Capture);
                Step.ParseSrcPos(value.Substring(0, i), value.Substring(i + 1), manSide,
                                        out manType, out srcPos, out hasSame, out sameHorizontal, out sameVertical);
                value = value.Substring(i + 1);

                #endregion
            }
            else if (value.Length == 3)
            {
                if (char.IsUpper(value[0]))
                {
                    manType = Enums.ToPieceType(value[0]);
                    value = value.Remove(0, 1);
                }
                else
                {
                    manType = Enums.PieceType.WhitePawn;
                    hasSame = Enums.Orientation.Rank;
                    sameHorizontal = Utility.CharToInt(value[0]);
                    sameVertical = int.Parse(value[2].ToString());
                    srcPos = new Position(sameHorizontal, sameVertical);
                    value = value.Remove(0, 1);
                    goto BEGIN_PARSE;
                }
            }
            else if (value.Length == 4)//Rhb1,R1b7+,Rac2,a8=Q,Rae8+,Ngf3,Nbd7
            {
                Step.ParseSrcPos(value.Substring(0, 2), value.Substring(2), manSide,
                                        out manType, out srcPos, out hasSame, out sameHorizontal, out sameVertical);
                value = value.Substring(2);
            }
            
        END_PARSE:

            tgtPos = Position.Parse(value);

        END_PARSE_Castling:
            if (actionList.Count == 0)
                actionList.Add(Enums.Action.General);

            Step step = new Step(number, manType, srcPos, tgtPos, actionList.ToArray());
            step._hasSame = hasSame;
            step._sameHorizontal = sameHorizontal;
            step._sameVertical = sameVertical;
            if (isPromotion)//如果是升变
                step.PromotionPieceType = Enums.ToPieceType(promotionString);
            return step;
        }

        private static void ParseSrcPos(string before, 
                                        string after, 
                                        Enums.GameSide manSide, 
                                        out Enums.PieceType type, 
                                        out Position pos,
                                        out Enums.Orientation hasSame,
                                        out int sameHorizontal,
                                        out int sameVertical)
        {
            pos = Position.Empty;
            type = Enums.PieceType.None;
            hasSame = Enums.Orientation.None;
            sameHorizontal = 0;
            sameVertical = 0;
            if (char.IsUpper(before, 0))//首字母是大写的
            {
                type = Enums.ToPieceType(before[0]);
                before = before.Remove(0, 1);
            }
            else if (char.IsLower(before, 0))//首字母是小写的
            {
                type = Enums.PieceType.AllPawns;
            }
            //Parse value, 一般是指“axb5+”，“Rfxe8”，“B3xd6”中的第2个字符的解析
            if (!string.IsNullOrEmpty(before))
            {
                char c = before[0];

                if (c >= 'a' && c <= 'h')
                {
                    hasSame = Enums.Orientation.Rank;
                    sameHorizontal = Utility.CharToInt(c);
                    sameVertical = int.Parse(after[1].ToString());
                    if (type == Enums.PieceType.AllPawns)
                    {
                        if (manSide == Enums.GameSide.Black)
                            sameVertical++;
                        else
                            sameVertical--;
                    }
                }
                else
                {
                    hasSame = Enums.Orientation.File;
                    sameHorizontal = Utility.CharToInt(after[0]);
                    sameVertical = int.Parse(c.ToString());
                }
                pos = new Position(sameHorizontal, sameVertical);
            }
        }

        #endregion
    }
}