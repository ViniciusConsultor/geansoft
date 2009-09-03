using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

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
        public Enums.GameSide GameSide { get; set; }
        /// <summary>
        /// 获取或设置该步棋的棋子类型
        /// </summary>
        public Enums.PieceType PieceType { get; set; }
        /// <summary>
        /// 获取或设置该步棋的源棋格
        /// </summary>
        public Position SourcePosition { get; set; }
        /// <summary>
        /// 获取或设置该步棋的目标棋格
        /// </summary>
        public Position TargetPosition { get; set; }
        /// <summary>
        /// 获取或设置一步棋的动作说明
        /// </summary>
        public Enums.Action Action { get; set; }
        /// <summary>
        /// 获取或设置该步棋的升变后棋子类型
        /// </summary>
        public Enums.PieceType PromotionPieceType { get; set; }

        #endregion

        #region ctor

        public Step()
        {
            this.Number = 0;
            this.GameSide = Enums.GameSide.White;
            this.PieceType = Enums.PieceType.None;
            this.SourcePosition = Position.Empty;
            this.TargetPosition = Position.Empty;
            this.Action = Enums.Action.Invalid;
            this.PromotionPieceType = Enums.PieceType.None;
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
            #region Starting

            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException();
            value = value.Trim();
            if (value.Length < 2 || value == "\r\n")
                throw new ArgumentOutOfRangeException(value);
            this.Action = Enums.Action.General;

            #endregion

            #region 针对尾部标记符进行一些操作,并返回裁剪掉尾部标记符的Value值
            foreach (string flagword in Servicer.Flags)
            {
                if (value.EndsWith(flagword))
                {
                    if (flagword.Equals("+"))//Qh5+
                        this.Action = Enums.Action.Check;
                    value = value.Substring(0, value.LastIndexOf(flagword));//裁剪掉尾部标记符
                    break;
                }
            }
            #endregion

            foreach (var item in Servicer.StepRegex)
            {
                if (item.Second.IsMatch(value))
                {
                    switch (item.First)
                    {
                        #region case
                        case Servicer.AsStep.As_e4:
                            this.PieceType = Enums.ToPieceType(this.GameSide);
                            this.TargetPosition = Position.Parse(value);
                            continue;
                        case Servicer.AsStep.As_Rd7:
                            this.PieceType = Enums.ToPieceType(value[0], this.GameSide);
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 2));
                            continue;
                        case Servicer.AsStep.As_Rxa2:
                            this.PieceType = Enums.ToPieceType(value[0], this.GameSide);
                            this.Action = Enums.Action.Capture;
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 2));
                            continue;
                        case Servicer.AsStep.As_Rbe1:
                            this.PieceType = Enums.ToPieceType(value[0], this.GameSide);
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 2));
                            continue;
                        case Servicer.AsStep.As_N1c3:
                            this.PieceType = Enums.ToPieceType(value[0], this.GameSide);
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 2));
                            continue;
                        case Servicer.AsStep.As_hxg6:
                            this.PieceType = Enums.ToPieceType(this.GameSide);
                            this.Action = Enums.Action.Capture;
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 2));
                            continue;
                        case Servicer.AsStep.As_Ngxf6:
                            this.PieceType = Enums.ToPieceType(value[0], this.GameSide);
                            this.Action = Enums.Action.Capture;
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 2));
                            continue;
                        case Servicer.AsStep.As_R8xf5://N1c3
                            this.PieceType = Enums.ToPieceType(value[0], this.GameSide);
                            this.Action = Enums.Action.Capture;
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 2));
                            continue;
                        case Servicer.AsStep.As_e8_Q:
                            this.PieceType = Enums.ToPieceType(this.GameSide);
                            this.Action = Enums.ToPromoteAction(value[value.Length - 1]);
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 4, 2));
                            continue;
                        case Servicer.AsStep.As_exf8_Q:
                            this.PieceType = Enums.ToPieceType(this.GameSide);
                            this.Action = Enums.ToPromoteAction(value[value.Length - 1]);
                            this.TargetPosition = Position.Parse(value.Substring(value.Length - 4, 2));
                            continue;
                        case Servicer.AsStep.As_O_O:
                            this.PieceType = Enums.PieceType.None;
                            this.Action = Enums.Action.KingSideCastling;
                            continue;
                        case Servicer.AsStep.As_O_O_O:
                            this.PieceType = Enums.PieceType.None;
                            this.Action = Enums.Action.QueenSideCastling;
                            continue;
                        default:
                            break;
                        #endregion
                    }
                }
            }


        }

        #endregion

        #region IGenerator

        public string Generator()
        {
            StringBuilder sb = new StringBuilder(12);
            if (this.Action == Enums.Action.KingSideCastling)
                sb.Append("O-O");
            else if (this.Action == Enums.Action.QueenSideCastling)
                sb.Append("O-O-O");
            else
            {
                sb.Append(Enums.FromPieceType(this.PieceType)).Append(this.TargetPosition.ToString());
            }
            return sb.ToString();
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
            return this.Generator();
        }
        public override int GetHashCode()
        {
            return unchecked
                (3 * (
                this.Number.GetHashCode() +
                this.Action.GetHashCode() +
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
            if (!this.Number.Equals(step.Number))
                return false;
            if (!this.PieceType.Equals(step.PieceType))
                return false;
            if (!this.GameSide.Equals(step.GameSide))
                return false;
            if (!this.Action.Equals(step.Action))
                return false;
            if (!UtilityEquals.PairEquals(this.TargetPosition, step.TargetPosition))
                return false;
            if (!UtilityEquals.PairEquals(this.SourcePosition, step.SourcePosition))
                return false;
            if (!this.PromotionPieceType.Equals(step.PromotionPieceType))
                return false;
            if (!UtilityEquals.PairEquals(this.Parent, step.Parent))
                return false;
            if (!UtilityEquals.CollectionsEquals<IItem>(this.Items, step.Items))
                return false;
            return true;
        }

        #endregion

        /* static Parse
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
            return null;
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
        */
    }
}