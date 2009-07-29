using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Gean.Wrapper.Chess;

namespace Gean.UI.ChessControl
{
    public static class ChessSerivce
    {
        public static void Initialize()
        {
            ChessSerivce.ChessmanImages = new Dictionary<KeyValuePair<Enums.ChessmanType, Enums.ChessmanSide>, Image>(12);
        }

        public static Dictionary<KeyValuePair<Enums.ChessmanType, Enums.ChessmanSide>, Image> ChessmanImages { get; private set; }

        public static void InitializeChessmanImage()
        {
            KeyValuePair<Enums.ChessmanType, Enums.ChessmanSide> pair;
            Image image;

            pair = new KeyValuePair<Enums.ChessmanType, Enums.ChessmanSide>(Enums.ChessmanType.Bishop, Enums.ChessmanSide.Black);
            image = ChessmanResource.black_bishop;
            ChessSerivce.ChessmanImages.Add(pair, image);

            pair = new KeyValuePair<Enums.ChessmanType, Enums.ChessmanSide>(Enums.ChessmanType.King, Enums.ChessmanSide.Black);
            ChessSerivce.ChessmanImages.Add(pair, ChessmanResource.black_king);

            pair = new KeyValuePair<Enums.ChessmanType, Enums.ChessmanSide>(Enums.ChessmanType.Knight, Enums.ChessmanSide.Black);
            ChessSerivce.ChessmanImages.Add(pair, ChessmanResource.black_knight);

            pair = new KeyValuePair<Enums.ChessmanType, Enums.ChessmanSide>(Enums.ChessmanType.Pawn, Enums.ChessmanSide.Black);
            ChessSerivce.ChessmanImages.Add(pair, ChessmanResource.black_pawn);

            pair = new KeyValuePair<Enums.ChessmanType, Enums.ChessmanSide>(Enums.ChessmanType.Queen, Enums.ChessmanSide.Black);
            ChessSerivce.ChessmanImages.Add(pair, ChessmanResource.black_queen);

            pair = new KeyValuePair<Enums.ChessmanType, Enums.ChessmanSide>(Enums.ChessmanType.Rook, Enums.ChessmanSide.Black);
            ChessSerivce.ChessmanImages.Add(pair, ChessmanResource.black_rook);

            pair = new KeyValuePair<Enums.ChessmanType, Enums.ChessmanSide>(Enums.ChessmanType.Bishop, Enums.ChessmanSide.White);
            ChessSerivce.ChessmanImages.Add(pair, ChessmanResource.white_bishop);

            pair = new KeyValuePair<Enums.ChessmanType, Enums.ChessmanSide>(Enums.ChessmanType.King, Enums.ChessmanSide.White);
            ChessSerivce.ChessmanImages.Add(pair, ChessmanResource.white_king);

            pair = new KeyValuePair<Enums.ChessmanType, Enums.ChessmanSide>(Enums.ChessmanType.Knight, Enums.ChessmanSide.White);
            ChessSerivce.ChessmanImages.Add(pair, ChessmanResource.white_knight);

            pair = new KeyValuePair<Enums.ChessmanType, Enums.ChessmanSide>(Enums.ChessmanType.Pawn, Enums.ChessmanSide.White);
            ChessSerivce.ChessmanImages.Add(pair, ChessmanResource.white_pawn);

            pair = new KeyValuePair<Enums.ChessmanType, Enums.ChessmanSide>(Enums.ChessmanType.Queen, Enums.ChessmanSide.White);
            ChessSerivce.ChessmanImages.Add(pair, ChessmanResource.white_queen);

            pair = new KeyValuePair<Enums.ChessmanType, Enums.ChessmanSide>(Enums.ChessmanType.Rook, Enums.ChessmanSide.White);
            ChessSerivce.ChessmanImages.Add(pair, ChessmanResource.white_rook);
        }

        public static void InitializeChessmanImage(Dictionary<string, Image> images)
        {
            foreach (var item in images)
            {

            }
        }
    }
}
