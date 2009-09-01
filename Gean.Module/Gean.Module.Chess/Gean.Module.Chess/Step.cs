using System;
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
        /// 获取或设置一步棋的动作说明
        /// </summary>
        public List<Enums.Action> Actions { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的棋子类型
        /// </summary>
        public Enums.PieceType PieceType { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的棋子战方
        /// </summary>
        public Enums.GameSide GameSide { get; internal set; }
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
        private Enums.Orientation _hasSame = Enums.Orientation.None;
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
            throw new NotImplementedException();
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

        internal static Step Parse(int p, string p_2, Enums.GameSide gameSide)
        {
            throw new NotImplementedException();
        }
    }
}