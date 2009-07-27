using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Gean.Wrapper.Chess;

namespace Gean.UI.ChessControl
{
    public class ChessmanImages : IEnumerable<ChessmanImages.ChessmanImage>
    {
        private ChessmanImage[] Images { get; set; }

        public ChessmanImages()
        {
            this.Images = new ChessmanImage[12];
        }

        public void SetImage(Enums.ChessmanSide side, Enums.ChessmanType type, Image image)
        {
            switch (side)
            {
                #region case
                case Enums.ChessmanSide.White:
                    #region switch (type)
                    switch (type)
                    {
                        case Enums.ChessmanType.Rook:
                            this.Images[0] = new ChessmanImage(side, type, image);
                            break;
                        case Enums.ChessmanType.Knight:
                            this.Images[1] = new ChessmanImage(side, type, image);
                            break;
                        case Enums.ChessmanType.Bishop:
                            this.Images[2] = new ChessmanImage(side, type, image);
                            break;
                        case Enums.ChessmanType.Queen:
                            this.Images[3] = new ChessmanImage(side, type, image);
                            break;
                        case Enums.ChessmanType.King:
                            this.Images[4] = new ChessmanImage(side, type, image);
                            break;
                        case Enums.ChessmanType.Pawn:
                            this.Images[5] = new ChessmanImage(side, type, image);
                            break;
                        case Enums.ChessmanType.None:
                        default:
                            break;
                    }
                    #endregion
                    break;
                case Enums.ChessmanSide.Black:
                    #region switch (type)
                    switch (type)
                    {
                        case Enums.ChessmanType.Rook:
                            this.Images[6] = new ChessmanImage(side, type, image);
                            break;
                        case Enums.ChessmanType.Knight:
                            this.Images[7] = new ChessmanImage(side, type, image);
                            break;
                        case Enums.ChessmanType.Bishop:
                            this.Images[8] = new ChessmanImage(side, type, image);
                            break;
                        case Enums.ChessmanType.Queen:
                            this.Images[9] = new ChessmanImage(side, type, image);
                            break;
                        case Enums.ChessmanType.King:
                            this.Images[10] = new ChessmanImage(side, type, image);
                            break;
                        case Enums.ChessmanType.Pawn:
                            this.Images[11] = new ChessmanImage(side, type, image);
                            break;
                        case Enums.ChessmanType.None:
                        default:
                            break;
                    }
                    #endregion
                    break;
                case Enums.ChessmanSide.None:
                default:
                    break;
                #endregion
            }
        }

        public Image GetImage(Enums.ChessmanSide side, Enums.ChessmanType type)
        {
            foreach (ChessmanImage chessmanImage in Images)
            {
                if ((chessmanImage.Side == side) && (chessmanImage.Type == type))
                {
                    return chessmanImage.Image;
                }
            }
            return null;
        }

        public Image this[Enums.ChessmanSide side, Enums.ChessmanType type]
        {
            get { return GetImage(side, type); }
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

        #region IEnumerable<ChessmanImage> 成员

        public IEnumerator<ChessmanImage> GetEnumerator()
        {
            return (IEnumerator<ChessmanImage>)this.Images.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Images.GetEnumerator();
        }

        #endregion
    }
}
