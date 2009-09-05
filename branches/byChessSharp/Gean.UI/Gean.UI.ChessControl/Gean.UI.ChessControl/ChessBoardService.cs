﻿using System;
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
            ChessBoardService.ChessmanImages = new Dictionary<ChessmanState, Image>(12);
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
        private static Dictionary<ChessmanState, Image> ChessmanImages { get; set; }
        /// <summary>
        /// 初始化默认棋子背景图片集合
        /// </summary>
        private static void InitializeChessmanImages()
        {
            #region Initialize Dictionary<ChessmanState, Image>

            ChessmanState state;

            state = new ChessmanState(Enums.ChessmanSide.Black, Enums.ChessmanType.Bishop);
            ChessBoardService.ChessmanImages.Add(state, ChessResource.black_bishop);

            state = new ChessmanState(Enums.ChessmanSide.Black, Enums.ChessmanType.King);
            ChessBoardService.ChessmanImages.Add(state, ChessResource.black_king);

            state = new ChessmanState(Enums.ChessmanSide.Black, Enums.ChessmanType.Knight);
            ChessBoardService.ChessmanImages.Add(state, ChessResource.black_knight);

            state = new ChessmanState(Enums.ChessmanSide.Black, Enums.ChessmanType.Pawn);
            ChessBoardService.ChessmanImages.Add(state, ChessResource.black_pawn);

            state = new ChessmanState(Enums.ChessmanSide.Black, Enums.ChessmanType.Queen);
            ChessBoardService.ChessmanImages.Add(state, ChessResource.black_queen);

            state = new ChessmanState(Enums.ChessmanSide.Black, Enums.ChessmanType.Rook);
            ChessBoardService.ChessmanImages.Add(state, ChessResource.black_rook);

            state = new ChessmanState(Enums.ChessmanSide.White, Enums.ChessmanType.Bishop);
            ChessBoardService.ChessmanImages.Add(state, ChessResource.white_bishop);

            state = new ChessmanState(Enums.ChessmanSide.White, Enums.ChessmanType.King);
            ChessBoardService.ChessmanImages.Add(state, ChessResource.white_king);

            state = new ChessmanState(Enums.ChessmanSide.White, Enums.ChessmanType.Knight);
            ChessBoardService.ChessmanImages.Add(state, ChessResource.white_knight);

            state = new ChessmanState(Enums.ChessmanSide.White, Enums.ChessmanType.Pawn);
            ChessBoardService.ChessmanImages.Add(state, ChessResource.white_pawn);

            state = new ChessmanState(Enums.ChessmanSide.White, Enums.ChessmanType.Queen);
            ChessBoardService.ChessmanImages.Add(state, ChessResource.white_queen);

            state = new ChessmanState(Enums.ChessmanSide.White, Enums.ChessmanType.Rook);
            ChessBoardService.ChessmanImages.Add(state, ChessResource.white_rook);

            #endregion

            //注册棋子背景图片更换事件
            OnChessmanImagesChanged(new ChessmanImagesChangedEventArgs(ChessBoardService.ChessmanImages));
        }

        internal static Image GetChessmanImage(Enums.ChessmanSide chessmanSide, Enums.ChessmanType chessmanType)
        {
            ChessmanState state = new ChessmanState(chessmanSide, chessmanType);
            return ChessmanImages[state];
        }

        /// <summary>
        /// 更换棋子的背景图片集合
        /// </summary>
        /// <param name="images">棋子的背景图片集合</param>
        public static void ChangeChessmanImages(Dictionary<string, Image> images)
        {
            ChessBoardService.ChessmanImages = null;
            //ChessBoardHelper.ChessmanImages = images;

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
            public Dictionary<ChessmanState, Image> Images { get; private set; }
            public ChessmanImagesChangedEventArgs(Dictionary<ChessmanState, Image> images)
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