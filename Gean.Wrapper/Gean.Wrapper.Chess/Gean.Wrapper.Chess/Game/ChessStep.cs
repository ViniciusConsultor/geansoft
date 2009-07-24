using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

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
        public Enums.ActionDescription Action { get; internal set; }
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
        public IndexList CommentIndexs
        {
            get { return this._commentIndexs; }
            internal set { this._commentIndexs = value; }
        }
        private IndexList _commentIndexs = new IndexList();

        /// <summary>
        /// 获取或设置该步棋的变招的索引集合
        /// </summary>
        public IndexList ChoiceStepsIndexs
        { 
            get { return this._choiceStepIndexs; }
            internal set { this._choiceStepIndexs = value; }
        }
        private IndexList _choiceStepIndexs = new IndexList();

        #endregion

        #region 构造函数

        public ChessStep() : this(Enums.Castling.None) { }
        public ChessStep(Enums.Castling castling)
            : this(castling, Enums.ChessmanType.None, Enums.ActionDescription.None, ChessSquare.Empty, ChessSquare.Empty)
        {
            //this
        }
        public ChessStep(Enums.ChessmanType manType, Enums.ActionDescription action, ChessSquare sourceSquare, ChessSquare targetSquare)
            : this(Enums.Castling.None, manType, action, sourceSquare, targetSquare)
        {
            //this
        }
        public ChessStep(Enums.Castling castling, Enums.ChessmanType manType, Enums.ActionDescription action, ChessSquare sourceSquare, ChessSquare targetSquare)
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
                        if (Enums.GetFlag(this.Action, Enums.ActionDescription.Check) == Enums.ActionDescription.Kill)
                        {
                            if (this.ChessmanType == Enums.ChessmanType.Pawn)//如果有子被杀死，列出兵的位置
                            {
                                sb.Append(this.SourceSquare.CharX);
                            }
                            sb.Append('x');
                        }
                        sb.Append(this.TargetSquare.ToString());
                        //有将军的动作，打印'+'
                        if (Enums.GetFlag(this.Action, Enums.ActionDescription.Kill) == Enums.ActionDescription.Check)
                        {
                            sb.Append('+');
                        }
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
            if (this._choiceStepIndexs.Count > 0)//如果有变招，打印变招字符串
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

            if (!UtilityEquals.EnumerableEquals(this.ChoiceStepsIndexs, step.ChoiceStepsIndexs))
                return false;
            if (!UtilityEquals.EnumerableEquals(this.CommentIndexs, step.CommentIndexs))
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

            string comments_choices = "";

            #region 解析注释与变招的索引

            IndexList comments = new IndexList();
            IndexList choices = new IndexList();
            if ((value.IndexOf('(') >= 0) || (value.IndexOf('[') >= 0))
            {
                int x = value.IndexOf('(');
                int y = value.IndexOf('[');
                int i = x > y ? y : x;
                comments_choices = value.Substring(i);
                value = value.Substring(0, i);
                comments = Utility.IndexParse(comments_choices, '(', ')');
                choices = Utility.IndexParse(comments_choices, '[', ']');
            }

            #endregion

            Enums.ActionDescription action = Enums.ActionDescription.General;
            Enums.ChessmanType manType = Enums.ChessmanType.None;
            ChessSquare square = ChessSquare.Empty;

            //针对尾部标记符进行一些操作
            ChessStepFlag flags = new ChessStepFlag();
            string endString = string.Empty;
            foreach (string flagword in flags)
            {
                #region EndsWith(flagword)
                if (value.EndsWith(flagword))
                {
                    if (flagword.Equals("+"))//Qh5+
                        action = Enums.ActionDescription.Check;
                    endString = flagword;
                    int i = value.LastIndexOf(flagword);
                    value = value.Substring(0, i);
                    break;
                }
                #endregion
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
                        if (action == Enums.ActionDescription.Check)
                            action = Enums.ActionDescription.KillAndCheck;
                        else
                            action = Enums.ActionDescription.Kill;
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
            ChessStep step = null;
            if (castling == Enums.Castling.None)
                step = new ChessStep(manType, action, ChessSquare.Empty, square);
            else
                step = new ChessStep(castling);
            step.CommentIndexs = comments;
            step.ChoiceStepsIndexs = choices;
            return step;
        }

        #endregion

        #region class IndexList

        public class IndexList : IEnumerable<int>
        {
            List<int> _indexs = new List<int>();

            public int this[int index]
            {
                get { return _indexs[index]; }
            }

            public int IndexOf(int item)
            {
                return _indexs.IndexOf(item);
            }

            public void Add(int item)
            {
                _indexs.Add(item);
            }

            public void Clear()
            {
                _indexs.Clear();
            }

            public void RemoveAt(int index)
            {
                _indexs.RemoveAt(index);
            }

            public bool Remove(int item)
            {
                return _indexs.Remove(item);
            }

            public int Count
            {
                get { return _indexs.Count; }
            }

            #region IEnumerable<int> 成员

            public IEnumerator<int> GetEnumerator()
            {
                return _indexs.GetEnumerator();
            }

            #endregion

            #region IEnumerable 成员

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _indexs.GetEnumerator();
            }

            #endregion
        }

        #endregion
    }
}