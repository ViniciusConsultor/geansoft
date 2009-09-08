using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    [Serializable]
    public class Game : Situation, IGame
    {

        #region ctor

        public Game()
        {
            this.ActivedPieces = new Pieces();
            this.MovedPieces = new Pieces();
            this.PlayMode = Enums.PlayMode.ReplayGame;
        }

        #endregion

        public void StartGame()
        {
            this.Parse(NewGameFENString);
            this._isInit = false;
        }

        private bool _isInit = true;
        protected override void SetByPosition(int dot, char value)
        {
            base.SetByPosition(dot, value);
            if (!value.Equals('1'))
            {
                if (_isInit)
                    this.ActivedPieces.Add(Piece.Creator(Enums.ToPieceType(value), Position.GetPositionByDot(dot)));
                else
                    this.ActivedPieces[dot].Position = Position.GetPositionByDot(dot);
            }
        }
        
        #region IGame

        /// <summary>
        /// 可以使用的棋子(未被俘获)
        /// </summary>
        public Pieces ActivedPieces { get; private set; }

        /// <summary>
        /// 已被俘获的棋子
        /// </summary>
        public Pieces MovedPieces { get; private set; }

        /// <summary>
        /// 当前棋局的模式
        /// </summary>
        public Enums.PlayMode PlayMode { get; set; }

        /// <summary>
        /// 从“可以使用的棋子集合”中获取与指定棋格的位置相关联的棋子。
        /// </summary>
        /// <param name="dot">指定棋格的位置</param>
        /// <param name="piece">关联的棋子</param>
        public bool TryGetPiece(int dot, out Piece piece)
        {
            return this.ActivedPieces.TryGetPiece(dot, out piece);
        }

        /// <summary>
        /// 从“可以使用的棋子集合”中尝试从棋子集合中判断是否包含指定的棋子，并获取棋盘位置
        /// </summary>
        /// <param name="Piece">指定的棋子</param>
        /// <param name="dot">棋盘位置</param>
        public bool TryContains(Piece piece, out int dot)
        {
            return this.ActivedPieces.TryContains(piece, out dot);
        }

        #endregion

        #region IPieceMove

        #region MoveIn

        /// <summary>
        /// 移动指定位置棋格的棋子
        /// </summary>
        /// <param name="step">指定的成对的位置,First值为源棋格,Second值是目标棋格</param>
        public void PieceMoveIn(PositionPair step)
        {
            Enums.Action action = Enums.Action.General;
            Piece piece;
            if (this.TryGetPiece(step.First.Dot, out piece))
            {
                //第一步，判断目标棋格中是否有棋子。
                //如果有棋子，更改Action，调用PieceMoveOut方法。
                if (this.ContainsPiece(step.Second.Dot))
                {
                    action = Enums.Action.Capture;
                    this.PieceMoveOut(step.Second);
                }


                //第二步，调用内部方法PieceMoveIn，移动棋子，并更改FEN记录
                this.PieceMoveIn(piece, step.First, step.Second);


                //第三步，移动棋子后，根据移动棋子后的局面生成Action，主要是判断另一战方的“王”是否被将军
                action = this.IsCheckPieceKing(action, piece);


                //第四步，注册棋子移动后事件
                OnPieceMoveInEvent(piece, action, step);
            }
        }

        #region PieceMoveIn Sub Method

        /// <summary>
        /// 内部方法：移动棋子，并更改FEN记录
        /// </summary>
        /// <param name="piece">被移动的棋子</param>
        /// <param name="srcPos">源棋格</param>
        /// <param name="tgtPos">目标棋格</param>
        private void PieceMoveIn(Piece piece, Position srcPos, Position tgtPos)
        {
            switch (piece.PieceType)
            {
                #region case
                case Enums.PieceType.WhiteKing:
                case Enums.PieceType.BlackKing:
                    {
                        //TODO:在这里实现王车易位
                        break;
                    }
                case Enums.PieceType.WhitePawn:
                case Enums.PieceType.BlackPawn:
                    {
                        //TODO:在这里实现“吃过路兵”、“升变”
                        break;
                    }
                case Enums.PieceType.WhiteQueen:
                case Enums.PieceType.WhiteRook:
                case Enums.PieceType.WhiteBishop:
                case Enums.PieceType.WhiteKnight:
                case Enums.PieceType.BlackQueen:
                case Enums.PieceType.BlackRook:
                case Enums.PieceType.BlackBishop:
                case Enums.PieceType.BlackKnight:
                case Enums.PieceType.AllKings:
                case Enums.PieceType.AllQueens:
                case Enums.PieceType.AllRooks:
                case Enums.PieceType.AllBishops:
                case Enums.PieceType.AllKnights:
                case Enums.PieceType.AllPawns:
                case Enums.PieceType.All:
                case Enums.PieceType.None:
                default:
                    break;
                #endregion
            }
            piece.Position = tgtPos;
            this.SetByPosition(srcPos.Dot, '1');
            this.SetByPosition(tgtPos.Dot, Enums.FromPieceType(piece.PieceType)[0]);
            this.GameSide = Enums.GetOtherGameSide(this.GameSide);
            if (this.GameSide == Enums.GameSide.White)
            {
                this.FullMoveNumber++;
            }
            this.HalfMoveClock++;
        }

        /// <summary>
        /// 返回另一战方的“王”是否被将军
        /// </summary>
        /// <param name="action">动作</param>
        /// <param name="piece">可能产生将军动作的棋子</param>
        private Enums.Action IsCheckPieceKing(Enums.Action action, Piece piece)
        {
            //Positions postions = piece.GetEnablePositions(this);
            //Position kingPos = this.ActivedPieces.GetOtherGameSideKing(this.GameSide).Position;
            //foreach (Position pos in postions)
            //{
            //    if (pos.Equals(kingPos))
            //    {
            //        if (action == Enums.Action.General)
            //            action = Enums.Action.Check;
            //        else
            //            action = Enums.Action.CaptureCheck;
            //        break;
            //    }
            //}
            return action;
        }

        #endregion

        #endregion

        #region MoveOut
        /// <summary>
        /// 移除指定位置棋格的棋子
        /// </summary>
        /// <param name="pos">指定位置</param>
        public void PieceMoveOut(Position pos)
        {
            Piece piece = this.ActivedPieces[pos.Dot];
            piece.IsCaptured = true;
            this.ActivedPieces.Remove(piece);
            this.MovedPieces.Add(piece);
            this.HalfMoveClock = 0;
            OnPieceMoveOutEvent(piece, pos);//注册棋子被俘事件
        }
        #endregion

        #region Event

        /// <summary>
        /// 在棋子被俘后发生
        /// </summary>
        public event PieceMoveOut PieceMoveOutEvent;
        protected virtual void OnPieceMoveOutEvent(Piece piece, Position pos)
        {
            if (PieceMoveOutEvent != null)
            {
                PieceMoveOutEvent(piece, pos);
            }
        }

        /// <summary>
        /// 在棋子移动后发生
        /// </summary>
        public event PieceMoveIn PieceMoveInEvent;
        protected virtual void OnPieceMoveInEvent(Piece piece, Enums.Action action, Pair<Position, Position> positions)
        {
            if (PieceMoveInEvent != null)
            {
                PieceMoveInEvent(piece, action, positions);
            }
        }

        #endregion

        #endregion

        #region IEnumerable<Piece> 成员

        public IEnumerator<Piece> GetEnumerator()
        {
            return this.ActivedPieces.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.ActivedPieces.GetEnumerator();
        }

        #endregion

    }
}
