using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Gean.Wrapper.Chess;

namespace Gean.UI.ChessControl
{
    public static class ChessBoardService
    {
        /// <summary>
        /// 静态类型:ChessBoardHelper的初始化
        /// </summary>
        public static void Initialize()
        {
            ChessBoardService.ChessmanImages = new Dictionary<Enums.ChessmanType, Image>(12);
            ChessBoardService.InitializeBoardImage();
            ChessBoardService.InitializeGridImages();
            ChessBoardService.InitializeChessmanImages();
        }

        #region Board Image

        internal static Image BoardImage { get; private set; }

        private static void InitializeBoardImage()
        {
            ChessBoardService.BoardImage = ChessResource.board_4;
            OnBoardImageChanged(new BoardImageChangedEventArgs(ChessBoardService.BoardImage));
        }

        /// <summary>
        /// 更换棋盘所在桌面的背景图片
        /// </summary>
        /// <param name="boardImage">棋盘所在桌面的背景图片</param>
        public static void ChangeBoardImage(Image boardImage)
        {
            ChessBoardService.BoardImage = boardImage;
            OnBoardImageChanged(new BoardImageChangedEventArgs(ChessBoardService.BoardImage));
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
            ChessBoardService.WhiteGridImage = ChessResource.white_grid_01;
            ChessBoardService.BlackGridImage = ChessResource.black_grid_01;

            OnGridImagesChanged(new GridImagesChangedEventArgs(ChessBoardService.WhiteGridImage, ChessBoardService.BlackGridImage));
        }

        /// <summary>
        /// 更换棋格的背景图片
        /// </summary>
        /// <param name="white">白棋格的背景图片</param>
        /// <param name="black">黑棋格的背景图片</param>
        public static void ChangeGridImages(Image white, Image black)
        {
            if (white == null || black == null)
            {
                Debug.Fail("Image cannot NULL.");
                return;
            }
            ChessBoardService.WhiteGridImage = white;
            ChessBoardService.BlackGridImage = black;

            OnGridImagesChanged(new GridImagesChangedEventArgs(ChessBoardService.WhiteGridImage, ChessBoardService.BlackGridImage));
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
        private static Dictionary<Enums.ChessmanType, Image> ChessmanImages { get; set; }
        /// <summary>
        /// 初始化默认棋子背景图片集合
        /// </summary>
        private static void InitializeChessmanImages()
        {
            #region Initialize Dictionary<Enums.ChessmanType, Image>

            ChessmanState state;

            ChessBoardService.ChessmanImages.Add(Enums.ChessmanType.BlackBishop, ChessResource.black_bishop);
            ChessBoardService.ChessmanImages.Add(Enums.ChessmanType.BlackKing, ChessResource.black_king);
            ChessBoardService.ChessmanImages.Add(Enums.ChessmanType.BlackKnight, ChessResource.black_knight);
            ChessBoardService.ChessmanImages.Add(Enums.ChessmanType.BlackPawn, ChessResource.black_pawn);
            ChessBoardService.ChessmanImages.Add(Enums.ChessmanType.BlackQueen, ChessResource.black_queen);
            ChessBoardService.ChessmanImages.Add(Enums.ChessmanType.BlackRook, ChessResource.black_rook);
            ChessBoardService.ChessmanImages.Add(Enums.ChessmanType.WhiteBishop, ChessResource.white_bishop);
            ChessBoardService.ChessmanImages.Add(Enums.ChessmanType.WhiteKing, ChessResource.white_king);
            ChessBoardService.ChessmanImages.Add(Enums.ChessmanType.WhiteKnight, ChessResource.white_knight);
            ChessBoardService.ChessmanImages.Add(Enums.ChessmanType.WhitePawn, ChessResource.white_pawn);
            ChessBoardService.ChessmanImages.Add(Enums.ChessmanType.WhiteQueen, ChessResource.white_queen);
            ChessBoardService.ChessmanImages.Add(Enums.ChessmanType.WhiteRook, ChessResource.white_rook);

            #endregion

            //注册棋子背景图片更换事件
            OnChessmanImagesChanged(new ChessmanImagesChangedEventArgs(ChessBoardService.ChessmanImages));
        }

        internal static Image GetChessmanImage(Enums.ChessmanType chessmanType)
        {
            return ChessmanImages[chessmanType];
        }

        /// <summary>
        /// 更换棋子的背景图片集合
        /// </summary>
        /// <param name="images">棋子的背景图片集合</param>
        public static void ChangeChessmanImages(Dictionary<Enums.ChessmanType, Image> images)
        {
            ChessBoardService.ChessmanImages = images;

            //注册棋子背景图片更换事件
            OnChessmanImagesChanged(new ChessmanImagesChangedEventArgs(ChessBoardService.ChessmanImages));
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
            public Dictionary<Enums.ChessmanType, Image> Images { get; private set; }
            public ChessmanImagesChangedEventArgs(Dictionary<Enums.ChessmanType, Image> images)
            {
                this.Images = images;
            }
        }

        #endregion

        #region Option

        /// <summary>
        /// 自动演示(摆棋)的间隔时间(毫秒)
        /// </summary>
        public static int AutoForwardTime
        {
            get { return _autoForwardTime; }
        }
        private static int _autoForwardTime = 2000;

        #endregion

    }
}
