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
        /// 获取或设置该步棋的目标棋格
        /// </summary>
        public ChessPoint TargetPoint { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的源棋格
        /// </summary>
        public ChessPoint SourcePoint { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的注释的索引集合
        /// </summary>
        public IndexList CommentIndexs { get; internal set; }
        /// <summary>
        /// 获取或设置该步棋的变招的索引集合
        /// </summary>
        public IndexList ChoiceStepsIndexs { get; internal set; }

        #endregion

        #region ctor

        public ChessStep(Enums.Action action, Enums.ChessmanType chessmanType, ChessPoint sourcePoint, ChessPoint targetPoint)
        {
            this.Action = action;
            this.ChessmanType = chessmanType;
            this.TargetPoint = targetPoint;
            this.SourcePoint = sourcePoint;
            this.CommentIndexs = new IndexList();
            this.ChoiceStepsIndexs = new IndexList();
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
                case Enums.Action.None:
                    {
                        if (this.ChessmanType != Enums.ChessmanType.Pawn)//如果是“兵”，不打印
                        {
                            sb.Append(Enums.ChessmanTypeToString(this.ChessmanType));
                        }
                        if (Enums.GetFlag(this.Action, Enums.Action.Check) == Enums.Action.Kill)
                        {
                            if (this.ChessmanType == Enums.ChessmanType.Pawn)//如果有子被杀死，列出兵的位置
                            {
                                sb.Append(this.SourcePoint.CharX);
                            }
                            sb.Append('x');
                        }
                        sb.Append(this.TargetPoint.ToString());
                        //有将军的动作，打印'+'
                        if (Enums.GetFlag(this.Action, Enums.Action.Kill) == Enums.Action.Check)
                        {
                            sb.Append('+');
                        }
                        break;
                    }
                #endregion
            }
            #region 注释
            if (this.CommentIndexs.Count > 0)//如果有注释，打印注释
            {
                sb.Append('(');
                foreach (int index in CommentIndexs)
                {
                    sb.Append(index.ToString()).Append(',');
                }
                sb.Remove(sb.Length - 1, 1).Append(')');
            }
            #endregion
            #region 变招
            if (this.ChoiceStepsIndexs.Count > 0)//如果有变招，打印变招字符串
            {
                sb.Append("[");
                foreach (int index in this.ChoiceStepsIndexs)
                {
                    sb.Append(index.ToString()).Append(',');
                }
                sb.Remove(sb.Length - 1, 1).Append("]");
            }
            #endregion
            return sb.ToString().Trim();
        }
        public override int GetHashCode()
        {
            return unchecked
                (3 * (
                this.Action.GetHashCode() +
                this.ChessmanType.GetHashCode() +
                this.TargetPoint.GetHashCode() + this.SourcePoint.GetHashCode() +
                this.CommentIndexs.GetHashCode() + this.ChoiceStepsIndexs.GetHashCode()
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
            if (!UtilityEquals.PairEquals(this.TargetPoint, step.TargetPoint))
                return false;
            if (!UtilityEquals.PairEquals(this.SourcePoint, step.SourcePoint))
                return false;
            if (!UtilityEquals.EnumerableEquals(this.ChoiceStepsIndexs, step.ChoiceStepsIndexs))
                return false;
            if (!UtilityEquals.EnumerableEquals(this.CommentIndexs, step.CommentIndexs))
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

            int n = 0;
            ChessPoint sourcePoint = ChessPoint.Empty;

            if (char.IsUpper(value, 0))
            {//首字母是大写的
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
                    manType = Enums.StringToChessmanType(value[n]);
                    n++;
                    if (value[n] == 'x')
                    {
                        n++;
                        if (action == Enums.Action.Check)
                            action = Enums.Action.KillAndCheck;
                        else
                            action = Enums.Action.Kill;
                    }
                    rid = ChessGrid.Parse(value.Substring(n, 2));
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
                        sourcePoint = new ChessPoint(Utility.CharToInt(cx), iy);
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
            ChessStep step = new ChessStep(action, manType, sourcePoint, new ChessPoint(rid.PointX, rid.PointY));
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