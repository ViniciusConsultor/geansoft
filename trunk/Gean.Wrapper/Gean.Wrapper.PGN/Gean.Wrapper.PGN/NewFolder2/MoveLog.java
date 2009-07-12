import java.io.*;
import javax.swing.*;

class MoveLog implements Serializable
{
  public MoveLog nextMove;
  public MoveLog previousMove;

  public String sourcePiece;
  public int sourceX;
  public int sourceY;
  public String destinationPiece;
  public int destinationX;
  public int destinationY;
  public String gameResult;
  public String promotionPiece;

  private String color;
  private int moveNumber;

  private SquareCoordinate multiplePiece;
  private String check;

  public MoveLog(
    MoveLog currentMove,
    String sourcePiece, int sourceX, int sourceY,
    String destinationPiece, int destinationX, int destinationY,
    SquareCoordinate multiplePiece, String check, String promotionPiece) {

    this.sourcePiece = sourcePiece;
    this.sourceX = sourceX;
    this.sourceY = sourceY;
    this.destinationPiece = destinationPiece;
    this.destinationX = destinationX;
    this.destinationY = destinationY;
    this.multiplePiece = multiplePiece;
    this.check = check;
    gameResult = null;
    this.promotionPiece = promotionPiece;

    if (currentMove != null) {
      currentMove.nextMove = this;
      if (currentMove.color.equals("w")) {
        color = "b";
	moveNumber = currentMove.moveNumber;
      }
      else {
        color = "w";
	moveNumber = currentMove.moveNumber + 1;
      }
    }
    else {
      color = "w";
      moveNumber = 1;
    }

    previousMove = currentMove;
    nextMove = null;
  }

  private String pieceNoColor(String piece) {
    return piece.substring(1, 2);
  }

  private String displayX(int x) {
    if (x == -1)
      return "";
    return new Character((char)((int)'a' + x)).toString();
  }

  private String displayY(int y) {
    if (y == -1)
      return "";
    return "" + (8 - y);
  }

  private boolean displayCastle(JTextArea textArea) {
    if (sourcePiece.equals("WK") || sourcePiece.equals("BK"))
      if (Math.abs(sourceX - destinationX) > 1) { // castled king
        if (sourceX < destinationX)
          textArea.append("O-O");
        else
          textArea.append("O-O-O");
        textArea.append(check);
        textArea.append(" ");
        return true;
      }
    return false;
  }

  public String currentMoveString() {
    String move;
    String promotion;

    if (promotionPiece != null)
      promotion = pieceNoColor(promotionPiece);
    else
      promotion = "";

    move =
      displayX(sourceX) + displayY(sourceY) +
      displayX(destinationX) + displayY(destinationY) +
      promotion;
    return move;
  }

  private String getNotatedMove(String sourcePiece, int sourceX, int sourceY,
    int destinationX, int destinationY) {
    String move;
    String piece = pieceNoColor(sourcePiece);
    String capture = "";
    String promotion;

    if (piece.equals("P")) {
      piece = "";
      if (sourceX != destinationX) {
        capture = "x";
        sourceY = -1;
      }
      else {
        sourceX = -1;
        sourceY = -1;
      }
    }
    else {
      if (multiplePiece.x == -1)
        sourceX = -1;
      if (multiplePiece.y == -1)
        sourceY = -1;
      if (!destinationPiece.equals("S"))
        capture = "x";
    }
    if (promotionPiece != null)
      promotion = "=" + pieceNoColor(promotionPiece);
    else
      promotion = "";

    move = piece +
      displayX(sourceX) + displayY(sourceY) + capture +
      displayX(destinationX) + displayY(destinationY) +
        promotion + check + " ";
    return move;
  }

  public void setGameResult(String result, Chess chess) {
    gameResult = result;
    chess.setTagValue("Result", result);
  }

  public void displayLog(int moveNumber, MoveLog currentMove, JTextArea textArea) {
    if (moveNumber == 1)
      textArea.setText(""); // clear out before displaying log
    if (currentMove == null)
      return;
    if (moveNumber % 2 == 1)
      textArea.append(((moveNumber + 1) / 2) + ". ");
    if (!displayCastle(textArea))
      textArea.append(
        getNotatedMove(
          sourcePiece,
          sourceX, sourceY, destinationX, destinationY));
    if (gameResult != null)
      textArea.append(gameResult + " ");
    if (this != currentMove)
      nextMove.displayLog(moveNumber + 1, currentMove, textArea);
  }

  public int getMoveNumber() {
    return moveNumber;
  }

  public String getColor() {
    return color;
  }
}
