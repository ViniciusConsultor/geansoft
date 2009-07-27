using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Gean.Wrapper.Chess;

namespace Gean.UI.ChessControl
{
    public class ChessmanImages
    {
        protected ChessmanImage[] Images { get; private set; }

        public ChessmanImages()
        {
            this.Images = new ChessmanImage[12];
        }

        public void SetImage(Enums.ChessmanSide side, Enums.ChessmanType type, Image image)
        {
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    switch (type)
                    {
                        case Enums.ChessmanType.Rook:
                            this.Images[0] = new ChessmanImage(side, type, image);
                            break;
                        case Enums.ChessmanType.Knight:
                            break;
                        case Enums.ChessmanType.Bishop:
                            break;
                        case Enums.ChessmanType.Queen:
                            break;
                        case Enums.ChessmanType.King:
                            break;
                        case Enums.ChessmanType.Pawn:
                            break;
                        case Enums.ChessmanType.None:
                        default:
                            break;
                    }
                    break;
                case Enums.ChessmanSide.Black:
                    break;
                case Enums.ChessmanSide.None:
                default:
                    break;
            }
        }

        public Image GetImage(Enums.ChessmanSide side, Enums.ChessmanType type)
        {
            return null;
        }

        public class ChessmanImage
        {
            public Image Image { get; set; }
            public Enums.ChessmanSide Side { get; set; }
            public Enums.ChessmanType Type { get; set; }

            public ChessmanImage(Enums.ChessmanSide side, Enums.ChessmanType type, Image image)
            {
                this.Side = side;
                this.Type = type;
                this.Image = image;
            }
        }
    }
}
