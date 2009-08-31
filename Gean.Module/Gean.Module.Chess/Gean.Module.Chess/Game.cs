using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    public class Game : Situation, IGame
    {

        #region IGame 成员

        public Pieces ActivedPieces
        {
            get { throw new NotImplementedException(); }
        }

        public Pieces MovedPieces
        {
            get { throw new NotImplementedException(); }
        }

        public bool TryGetPiece(int dot, out Piece piece)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IPieceMove 成员

        public Enums.Action PieceMoveIn(Pair<Position, Position> step)
        {
            Enums.Action action = Enums.Action.General;
            //第一步，从源棋格找出棋子。
            Piece piece;
            if (!this.TryGetPiece(step.First.Dot, out piece))
            {
                return Enums.Action.Invalid;
            }
            //第二步，判断目标棋格中是否有棋子。
            //如果有棋子，更改Action，调用PieceMoveOut方法。
            if (!this.ContainsPiece(step.Second.Dot))
            {
                action = Enums.Action.Capture;
                this.PieceMoveOut(step.Second);
            }
            //第三步，调用内部方法PieceMoveIn，移动棋子，并更改FEN记录
            this.PieceMoveIn(piece, step.First, step.Second);
            //第四步，移动棋子后，根据移动棋子后的局面生成Action，主要是判断另一战方的“王”是否被将军
            action = IsCheckPieceKing(action, piece);
            //第五步，注册棋子移动后事件
            OnPieceMoveInEvent(piece, action, step);
            return action;
        }

        /// <summary>
        /// 返回另一战方的“王”是否被将军
        /// </summary>
        /// <param name="action">动作</param>
        /// <param name="piece">可能产生将军动作的棋子</param>
        private Enums.Action IsCheckPieceKing(Enums.Action action, Piece piece)
        {
            Position[] postions = piece.GetEnablePositions();
            Position kingPos = Enums.GetOtherGameSideKing(this.GameSide, this.ActivedPieces).Position;
            foreach (Position pos in postions)
            {
                if (pos.Equals(kingPos))
                {
                    if (action == Enums.Action.General)
                        action = Enums.Action.Check;
                    else
                        action = Enums.Action.CaptureCheck;
                    break;
                }
            }
            return action;
        }

        /// <summary>
        /// 内部方法：移动棋子，并更改FEN记录
        /// </summary>
        /// <param name="piece">被移动的棋子</param>
        /// <param name="srcPos">源棋格</param>
        /// <param name="tgtPos">目标棋格</param>
        private void PieceMoveIn(Piece piece, Position srcPos, Position tgtPos)
        {
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

        /// <summary>
        /// 移除指定位置棋格的棋子
        /// </summary>
        /// <param name="pos">指定位置</param>
        public void PieceMoveOut(Position pos)
        {
            Piece piece;
            this.TryGetPiece(pos.Dot, out piece);
            piece.IsCaptured = true;
            this.ActivedPieces.Remove(piece);
            this.MovedPieces.Add(piece);
            this.HalfMoveClock = 0;
            OnPieceMoveOutEvent(piece, pos);//注册棋子被俘事件
        }

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

        #endregion

        #region IEnumerable<Piece> 成员

        public IEnumerator<Piece> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
