import javax.swing.*;

public class Promotion
{
  static public Promotion instance;

  static public Promotion getInstance() {
    if (instance == null)
      instance = new Promotion();
    return instance;
  }

  public String piece;

  public String getPiece() {
    return piece;
  }

  public void setPiece(JFrame frame, String destinationPawn, String piece) {
    if (piece == null) {
      if (destinationPawn.charAt(0) == 'W')
        new PromotionChooser(frame, "W", this);
      else
        new PromotionChooser(frame, "B", this);
    }
    else
      this.piece = piece;
  }

  public void clearPiece() {
    piece = null;
  }
}
