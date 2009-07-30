using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Gean.Wrapper.Chess;

namespace Gean.UI.ChessControl
{
    public static class ChessBoardHelper
    {
        /// <summary>
        /// 静态类型:ChessBoardHelper的初始化
        /// </summary>
        public static void Initialize()
        {
            ChessBoardHelper.ChessmanImages = new Dictionary<ChessmanState, Image>(12);
            ChessBoardHelper.InitializeBoardImage();
            ChessBoardHelper.InitializeGridImages();
            ChessBoardHelper.InitializeChessmanImages();
        }

        #region Board Image

        internal static Image BoardImage { get; private set; }

        private static void InitializeBoardImage()
        {
            ChessBoardHelper.BoardImage = ChessResource.board_01;
            OnBoardImageChanged(new BoardImageChangedEventArgs(ChessBoardHelper.BoardImage));
        }

        /// <summary>
        /// 更换棋盘所在桌面的背景图片
        /// </summary>
        /// <param name="boardImage">棋盘所在桌面的背景图片</param>
        public static void ChangeBoardImage(Image boardImage)
        {
            ChessBoardHelper.BoardImage = boardImage;
            OnBoardImageChanged(new BoardImageChangedEventArgs(ChessBoardHelper.BoardImage));
        }

        /// <summary>
        /// 在更换棋盘所在桌面的背景图片时发生
        /// </summary>
        public static event BoardImageChangedEventHandler BoardImageChangedEvent;
        private static void OnBoardImageChanged(BoardImageChangedEventArgs e)
        {
            if (BoardImageChangedEvent != null)
                BoardImageChangedEvent(e);
        }
        public delegate void BoardImageChangedEventHandler(BoardImageChangedEventArgs e);
        public class BoardImageChangedEventArgs : EventArgs
        {
            public Image BoardImage { get; private set; }
            public BoardImageChangedEventArgs(Image boardImage)
            {
                this.BoardImage = boardImage;
            }
        }

        #endregion

        #region Grid Image

        internal static Image WhiteGridImage { get; private set; }
        internal static Image BlackGridImage { get; private set; }

        private static void InitializeGridImages()
        {
            ChessBoardHelper.WhiteGridImage = ChessResource.white_grid_01;
            ChessBoardHelper.BlackGridImage = ChessResource.black_grid_01;

            OnGridImagesChanged(new GridImagesChangedEventArgs(ChessBoardHelper.WhiteGridImage, ChessBoardHelper.BlackGridImage));
        }

        /// <summary>
        /// 更换棋格的背景图片
        /// </summary>
        /// <param name="white">白棋格的背景图片</param>
        /// <param name="black">黑棋格的背景图片</param>
        public static void ChangeGridImages(Image white, Image black)
        {
            if (white == null || black == null)
                Debug.Fail("Image cannot NULL.");
                return;
            ChessBoardHelper.WhiteGridImage = white;
            ChessBoardHelper.BlackGridImage = black;

            OnGridImagesChanged(new GridImagesChangedEventArgs(ChessBoardHelper.WhiteGridImage, ChessBoardHelper.BlackGridImage));
        }

        /// <summary>
        /// 在更换棋格的背景图片时发生
        /// </summary>
        public static event GridImagesChangedEventHandler GridImagesChangedEvent;
        private static void OnGridImagesChanged(GridImagesChangedEventArgs e)
        {
            if (GridImagesChangedEvent != null)
                GridImagesChangedEvent(e);
        }
        public delegate void GridImagesChangedEventHandler(GridImagesChangedEventArgs e);
        public class GridImagesChangedEventArgs : EventArgs
        {
            public Image WhiteImage { get; private set; }
            public Image BlackImage { get; private set; }
            public GridImagesChangedEventArgs(Image white, Image black)
            {
                this.WhiteImage = white;
                this.BlackImage = black;
            }
        }

        #endregion

        #region Chessman Images

        /// <summary>
        /// 棋子背景图片集合
        /// </summary>
        private static Dictionary<ChessmanState, Image> ChessmanImages { get; set; }
        /// <summary>
        /// 初始化默认棋子背景图片集合
        /// </summary>
        private static void InitializeChessmanImages()
        {
            #region Initialize Dictionary<ChessmanState, Image>

            ChessmanState state;

            state = new ChessmanState(Enums.ChessmanSide.Black, Enums.ChessmanType.Bishop);
            ChessBoardHelper.ChessmanImages.Add(state, ChessResource.black_bishop);

            state = new ChessmanState(Enums.ChessmanSide.Black, Enums.ChessmanType.King);
            ChessBoardHelper.ChessmanImages.Add(state, ChessResource.black_king);

            state = new ChessmanState(Enums.ChessmanSide.Black, Enums.ChessmanType.Knight);
            ChessBoardHelper.ChessmanImages.Add(state, ChessResource.black_knight);

            state = new ChessmanState(Enums.ChessmanSide.Black, Enums.ChessmanType.Pawn);
            ChessBoardHelper.ChessmanImages.Add(state, ChessResource.black_pawn);

            state = new ChessmanState(Enums.ChessmanSide.Black, Enums.ChessmanType.Queen);
            ChessBoardHelper.ChessmanImages.Add(state, ChessResource.black_queen);

            state = new ChessmanState(Enums.ChessmanSide.Black, Enums.ChessmanType.Rook);
            ChessBoardHelper.ChessmanImages.Add(state, ChessResource.black_rook);

            state = new ChessmanState(Enums.ChessmanSide.White, Enums.ChessmanType.Bishop);
            ChessBoardHelper.ChessmanImages.Add(state, ChessResource.white_bishop);

            state = new ChessmanState(Enums.ChessmanSide.White, Enums.ChessmanType.King);
            ChessBoardHelper.ChessmanImages.Add(state, ChessResource.white_king);

            state = new ChessmanState(Enums.ChessmanSide.White, Enums.ChessmanType.Knight);
            ChessBoardHelper.ChessmanImages.Add(state, ChessResource.white_knight);

            state = new ChessmanState(Enums.ChessmanSide.White, Enums.ChessmanType.Pawn);
            ChessBoardHelper.ChessmanImages.Add(state, ChessResource.white_pawn);

            state = new ChessmanState(Enums.ChessmanSide.White, Enums.ChessmanType.Queen);
            ChessBoardHelper.ChessmanImages.Add(state, ChessResource.white_queen);

            state = new ChessmanState(Enums.ChessmanSide.White, Enums.ChessmanType.Rook);
            ChessBoardHelper.ChessmanImages.Add(state, ChessResource.white_rook);

            #endregion

            //注册棋子背景图片更换事件
            OnChessmanImagesChanged(new ChessmanImagesChangedEventArgs(ChessBoardHelper.ChessmanImages));
        }

        /// <summary>
        /// 更换棋子的背景图片集合
        /// </summary>
        /// <param name="images">棋子的背景图片集合</param>
        public static void ChangeChessmanImages(Dictionary<string, Image> images)
        {
            ChessBoardHelper.ChessmanImages = null;
            //ChessBoardHelper.ChessmanImages = images;

            //注册棋子背景图片更换事件
            OnChessmanImagesChanged(new ChessmanImagesChangedEventArgs(ChessBoardHelper.ChessmanImages));
        }

        /// <summary>
        /// 在棋子背景图片发生更换时发生
        /// </summary>
        public static event ChessmanImagesChangedEventHandler ChessmanImagesChangedEvent;
        private static void OnChessmanImagesChanged(ChessmanImagesChangedEventArgs e)
        {
            if (ChessmanImagesChangedEvent != null)
                ChessmanImagesChangedEvent(e);
        }
        public delegate void ChessmanImagesChangedEventHandler(ChessmanImagesChangedEventArgs e);
        public class ChessmanImagesChangedEventArgs : EventArgs
        {
            public Dictionary<ChessmanState, Image> Images { get; private set; }
            public ChessmanImagesChangedEventArgs(Dictionary<ChessmanState, Image> images)
            {
                this.Images = images;
            }
        }

        #endregion


    }
}
