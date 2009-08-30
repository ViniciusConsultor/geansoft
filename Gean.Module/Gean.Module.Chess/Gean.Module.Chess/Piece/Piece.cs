using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    public abstract class Piece : IPiece
    {
        #region ctor

        /// <summary>
        /// 构造函数
        /// </summary>
        protected Piece(Position position) 
        {
            _position = this.InitPosition(position);
            this.IsCaptured = false;
        }

        protected virtual Position InitPosition(Position position)
        {
            return position;
        }

        #endregion

        #region Property

        /// <summary>
        /// 棋子的类型
        /// </summary>
        public virtual Enums.PieceType PieceType { get; protected set; }
        /// <summary>
        /// 棋子的战方
        /// </summary>
        public virtual Enums.GameSide GameSide { get; protected set; }
        /// <summary>
        /// 获取或设置该棋子是否已被杀死
        /// </summary>
        public virtual bool IsCaptured { get; internal set; }
        /// <summary>
        /// 棋子所在位置
        /// </summary>
        public Position Position
        {
            get { return this._position; }
        }
        protected Position _position;

        #endregion

        #region abstract

        public abstract Position[] GetEnablePositions();

        #endregion

        #region Override

        /// <summary>
        /// 重写。生成该棋子的字符表示。
        /// </summary>
        /// <returns>大写表示为白棋，小写表示黑棋</returns>
        public override string ToString()
        {
            return Enums.FromPieceType(this.PieceType);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            Piece man = obj as Piece;
            if (this.PieceType.Equals(man.PieceType))
                return false;
            if (this.GameSide.Equals(man.GameSide))
                return false;
            if (this.IsCaptured.Equals(man.IsCaptured))
                return false;
            if (this.Position.Equals(man.Position))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked(3 * (
                this.GameSide.GetHashCode() ^
                this.PieceType.GetHashCode() ^
                this.IsCaptured.GetHashCode() ^
                this.Position.GetHashCode()
                ));
        }

        #endregion

        #region static

        /// <summary>
        /// 表示棋子为空(null)时。此变量为只读。
        /// </summary>
        public static readonly Piece Empty = null;
        /// <summary>
        /// 指示指定的 Piece 对象是 null 还是 Piece.Empty。
        /// </summary>
        /// <param name="chessman">指定的 Piece 对象</param>
        public static bool IsNullOrEmpty(Piece chessman)
        {
            if (chessman == null)
                return true;
            if (chessman == Piece.Empty)
                return true;
            return false;
        }

        /// <summary>
        /// 根据指定的棋子战方与指定的棋子X坐标获取开局的棋子坐标
        /// </summary>
        /// <param name="side">指定的棋子战方</param>
        /// <param name="x">指定的棋子X坐标</param>
        protected static Position GetOpenningsPosition(Enums.GameSide side, int x)
        {
            Position point = Position.Empty;
            switch (side)
            {
                case Enums.GameSide.White:
                    point = new Position(x, 1);
                    break;
                case Enums.GameSide.Black:
                    point = new Position(x, 8);
                    break;
            }
            return point;
        }

        #endregion

        #region PositionChangedEvent

        public event PositionChangedEventHandler PositionChangedEvent;
        protected virtual void OnPositionChanged(PositionChangedEventArgs e)
        {
            if (PositionChangedEvent != null)
                PositionChangedEvent(this, e);
        }
        public delegate void PositionChangedEventHandler(object sender, PositionChangedEventArgs e);
        public class PositionChangedEventArgs : ChangedEventArgs<Position>
        {
            public Enums.Action Action { get; private set; }
            public Piece MovedPiece { get; private set; }
            public Piece CaptruedPiece { get; private set; }
            public PositionChangedEventArgs(Enums.Action action, Piece movedPiece, Piece captruedPiece, Position srcPosition, Position tgtPosition)
                : base(srcPosition, tgtPosition)
            {
                this.Action = action;
                this.MovedPiece = movedPiece;
                this.CaptruedPiece = captruedPiece;
            }
        }

        #endregion
    }

}