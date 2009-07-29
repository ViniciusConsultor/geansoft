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
            ChessBoardHelper.Chessmans = new List<Chessman>(32);
            ChessBoardHelper.InitializeBoardImage();
            ChessBoardHelper.InitializeGridImages();
            ChessBoardHelper.InitializeChessmanImages();
            ChessBoardHelper.InitializeChessmans();
            ChessBoardHelper.Chessmans.TrimExcess();
        }

        #region Board Image

        private static Image BoardImage { get; set; }

        private static void InitializeBoardImage()
        {
            ChessBoardHelper.WhiteGridImage = ChessResource.board_02;
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

        private static Image WhiteGridImage { get; set; }
        private static Image BlackGridImage { get; set; }

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

        #region Chessman List

        private static List<Chessman> Chessmans { get; set; }

        private static void InitializeChessmans()
        {
            //兵
            for (int i = 1; i <= 8; i++)
            {
                ChessBoardHelper.Chessmans.Add(new ChessmanPawn(Enums.ChessmanSide.White, i));//白兵
                ChessBoardHelper.Chessmans.Add(new ChessmanPawn(Enums.ChessmanSide.Black, i));//黑兵
            }
            //王
            ChessBoardHelper.Chessmans.Add(new ChessmanKing(Enums.ChessmanSide.White));
            ChessBoardHelper.Chessmans.Add(new ChessmanKing(Enums.ChessmanSide.Black));
            //后
            ChessBoardHelper.Chessmans.Add(new ChessmanQueen(Enums.ChessmanSide.White));
            ChessBoardHelper.Chessmans.Add(new ChessmanQueen(Enums.ChessmanSide.Black));
            //车
            ChessBoardHelper.Chessmans.Add(new ChessmanRook(Enums.ChessmanSide.White, Enums.ChessGridSide.White));
            ChessBoardHelper.Chessmans.Add(new ChessmanRook(Enums.ChessmanSide.White, Enums.ChessGridSide.Black));
            ChessBoardHelper.Chessmans.Add(new ChessmanRook(Enums.ChessmanSide.Black, Enums.ChessGridSide.White));
            ChessBoardHelper.Chessmans.Add(new ChessmanRook(Enums.ChessmanSide.Black, Enums.ChessGridSide.Black));
            //马
            ChessBoardHelper.Chessmans.Add(new ChessmanKnight(Enums.ChessmanSide.White, Enums.ChessGridSide.White));
            ChessBoardHelper.Chessmans.Add(new ChessmanKnight(Enums.ChessmanSide.White, Enums.ChessGridSide.Black));
            ChessBoardHelper.Chessmans.Add(new ChessmanKnight(Enums.ChessmanSide.Black, Enums.ChessGridSide.White));
            ChessBoardHelper.Chessmans.Add(new ChessmanKnight(Enums.ChessmanSide.Black, Enums.ChessGridSide.Black));
            //象
            ChessBoardHelper.Chessmans.Add(new ChessmanBishop(Enums.ChessmanSide.White, Enums.ChessGridSide.White));
            ChessBoardHelper.Chessmans.Add(new ChessmanBishop(Enums.ChessmanSide.White, Enums.ChessGridSide.Black));
            ChessBoardHelper.Chessmans.Add(new ChessmanBishop(Enums.ChessmanSide.Black, Enums.ChessGridSide.White));
            ChessBoardHelper.Chessmans.Add(new ChessmanBishop(Enums.ChessmanSide.Black, Enums.ChessGridSide.Black));

            //注册棋子集合更换事件
            OnChessmansChanged(new ChessmansChangedEventArgs(ChessBoardHelper.Chessmans));
        }

        /// <summary>
        /// 更换棋子集合
        /// </summary>
        /// <param name="images">棋子集合</param>
        public static void ChangeChessmans(IEnumerable<Chessman> chessmans)
        {
            ChessBoardHelper.Chessmans.Clear();
            ChessBoardHelper.Chessmans.AddRange(chessmans);

            //注册棋子集合更换事件
            OnChessmansChanged(new ChessmansChangedEventArgs(ChessBoardHelper.Chessmans));
        }

        /// <summary>
        /// 在棋子集合发生更换时发生
        /// </summary>
        public static event ChessmansChangedEventHandler ChessmansChangedEvent;
        private static void OnChessmansChanged(ChessmansChangedEventArgs e)
        {
            if (ChessmansChangedEvent != null)
                ChessmansChangedEvent(e);
        }
        public delegate void ChessmansChangedEventHandler(ChessmansChangedEventArgs e);
        public class ChessmansChangedEventArgs : EventArgs
        {
            public List<Chessman> Chessmans { get; private set; }
            public ChessmansChangedEventArgs(List<Chessman> chessmans)
            {
                this.Chessmans = chessmans;
            }
        }

        #endregion

    }
}
