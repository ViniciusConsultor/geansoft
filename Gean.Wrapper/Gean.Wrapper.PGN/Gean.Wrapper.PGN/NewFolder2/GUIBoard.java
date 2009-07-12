import java.util.*;
//import java.awt.*;
//import java.awt.event.*;
import javax.swing.*;

public class GUIBoard extends JPanel
{
  private ChessEditor chessEditor;

  private GUISquare[][] guiBoard;

  public GUIBoard(ChessEditor chessEditor) {

    // create board with its squares

    this.chessEditor = chessEditor;

    this.setLayout(new java.awt.GridLayout(9, 8));

    guiBoard = new GUISquare[8][8];

    int x;
    int y;
    for (y = 0; y < 8; y++)
      for (x = 0; x < 8; x++)
        guiBoard[x][y] =
          new GUISquare(chessEditor, this, x, y, "S");

    initializeBoardPieces();

    // initialize commands

    new GUIButtonNop(chessEditor, this);
    new GUIButtonNop(chessEditor, this);
    new GUIButtonStart(chessEditor, this);
    new GUIButtonBack(chessEditor, this);
    new GUIButtonForward(chessEditor, this);
    new GUIButtonEnd(chessEditor, this);
    new GUIButtonNop(chessEditor, this);
    new GUIButtonNop(chessEditor, this);
  }

  public void initializeBoardPieces() {
    int x;
    int y;

    guiBoard[0][0].setGUISquare(false, "BR");
    guiBoard[1][0].setGUISquare(false, "BN");
    guiBoard[2][0].setGUISquare(false, "BB");
    guiBoard[3][0].setGUISquare(false, "BQ");
    guiBoard[4][0].setGUISquare(false, "BK");
    guiBoard[5][0].setGUISquare(false, "BB");
    guiBoard[6][0].setGUISquare(false, "BN");
    guiBoard[7][0].setGUISquare(false, "BR");
    for (x = 0; x < 8; x++)
      guiBoard[x][1].setGUISquare(false, "BP");
    for (x = 2; x < 6; x++)
      for (y = 0; y < 8; y++)
        guiBoard[y][x].setGUISquare(false, "S");
    for (x = 0; x < 8; x++)
      guiBoard[x][6].setGUISquare(false, "WP");
    guiBoard[0][7].setGUISquare(false, "WR");
    guiBoard[1][7].setGUISquare(false, "WN");
    guiBoard[2][7].setGUISquare(false, "WB");
    guiBoard[3][7].setGUISquare(false, "WQ");
    guiBoard[4][7].setGUISquare(false, "WK");
    guiBoard[5][7].setGUISquare(false, "WB");
    guiBoard[6][7].setGUISquare(false, "WN");
    guiBoard[7][7].setGUISquare(false, "WR");
  }

  public void refreshBoard() {
    for (int x = 0; x < 8; x++)
      for (int y = 0; y < 8; y++)
        guiBoard[x][y].setGUISquare(false, getPiece(x, y));
  }

  private void removePawnIfEnPassant(
    String sourcePiece, int sourceX, int sourceY,
    String destinationPiece, int destinationX, int destinationY) {
    if (sourceX != destinationX) // capture
      if (destinationPiece.equals("WP")) {
        if (sourceY > destinationY) {
          if (noPiece(destinationX, destinationY))
            markPiece("S", destinationX, sourceY);
        }
        else { // undo
          if (sourcePiece.equals("S"))
            markPiece("BP", sourceX, destinationY);
        }
      }
      else
      if (destinationPiece.equals("BP")) {
        if (sourceY < destinationY) {
          if (noPiece(destinationX, destinationY))
            markPiece("S", destinationX, sourceY);
        }
        else { // undo
          if (sourcePiece.equals("S"))
            markPiece("WP", sourceX, destinationY);
        }
      }
  }

  private void pawnPromotion(
    String sourcePiece, int sourceX, int sourceY,
    String destinationPiece, int destinationX, int destinationY,
    String promotionPiece) {
    if (destinationPiece.equals("WP")) {
      if (destinationY == 0) {
        Promotion.getInstance().setPiece(
	  chessEditor, destinationPiece, promotionPiece);
        promotionPiece = Promotion.getInstance().getPiece();
        markPiece(promotionPiece, destinationX, destinationY);
      }
    }
    else
    if (destinationPiece.equals("BP"))
      if (destinationY == 7) {
        Promotion.getInstance().setPiece(
	  chessEditor, destinationPiece, promotionPiece);
        promotionPiece = Promotion.getInstance().getPiece();
        markPiece(promotionPiece, destinationX, destinationY);
      }
  }

  private void moveRookIfCastleing(
    String sourcePiece, int sourceX, int sourceY,
    String destinationPiece, int destinationX, int destinationY) {
    if (destinationPiece.equals("WK")) {
      if (sourceX == 4 && sourceY == 7 &&
          destinationX == 6 && destinationY == 7) { // castle kingside
        markPiece("WR", 5, 7);
        markPiece("S", 7, 7);
      }
      else
      if (sourceX == 6 && sourceY == 7 &&
          destinationX == 4 && destinationY == 7) { // un-castle kingside
        markPiece("S", 5, 7);
        markPiece("WR", 7, 7);
      }
      else
      if (sourceX == 4 && sourceY == 7 &&
          destinationX == 2 && destinationY == 7) { // castle queenside
        markPiece("WR", 3, 7);
        markPiece("S", 0, 7);
      }
      else
      if (sourceX == 2 && sourceY == 7 &&
          destinationX == 4 && destinationY == 7) { // un-castle queenside
        markPiece("S", 3, 7);
        markPiece("WR", 0, 7);
      }
    }
    else
    if (destinationPiece.equals("BK")) {
      if (sourceX == 4 && sourceY == 0 &&
          destinationX == 6 && destinationY == 0) { // castle kingside
        markPiece("BR", 5, 0);
        markPiece("S", 7, 0);
      }
      else
      if (sourceX == 6 && sourceY == 0 &&
          destinationX == 4 && destinationY == 0) { // un-castle kingside
        markPiece("S", 5, 0);
        markPiece("BR", 7, 0);
      }
      else
      if (sourceX == 4 && sourceY == 0 &&
          destinationX == 2 && destinationY == 0) { // castle queenside
        markPiece("BR", 3, 0);
        markPiece("S", 0, 0);
      }
      else
      if (sourceX == 2 && sourceY == 0 &&
          destinationX == 4 && destinationY == 0) { // un-castle queenside
        markPiece("S", 3, 0);
        markPiece("BR", 0, 0);
      }
    }
  }

  public void makeMoveNoCheck(
    String sourcePiece, int sourceX, int sourceY,
    String destinationPiece, int destinationX, int destinationY,
    String promotionPiece) {
    moveRookIfCastleing(
      sourcePiece, sourceX, sourceY,
      destinationPiece, destinationX, destinationY);
    removePawnIfEnPassant(
      sourcePiece, sourceX, sourceY,
      destinationPiece, destinationX, destinationY);
    markPiece(sourcePiece, sourceX, sourceY);
    markPiece(destinationPiece, destinationX, destinationY);
    pawnPromotion(
      sourcePiece, sourceX, sourceY,
      destinationPiece, destinationX, destinationY, promotionPiece);
  }

  public boolean makeMove(
    String sourcePiece, int sourceX, int sourceY,
    String destinationPiece, int destinationX, int destinationY,
    String promotionPiece,
    MoveLog currentMove) {
    String oldSourcePiece, oldDestinationPiece;
    if (!legalMove(sourceX, sourceY, destinationX, destinationY))
      return false;
    moveRookIfCastleing(
      sourcePiece, sourceX, sourceY,
      destinationPiece, destinationX, destinationY);
    removePawnIfEnPassant(
      sourcePiece, sourceX, sourceY,
      destinationPiece, destinationX, destinationY);
    oldSourcePiece = getPiece(sourceX, sourceY);
    oldDestinationPiece = getPiece(destinationX, destinationY);
    markPiece(sourcePiece, sourceX, sourceY);
    markPiece(destinationPiece, destinationX, destinationY);
    pawnPromotion(
      sourcePiece, sourceX, sourceY,
      destinationPiece, destinationX, destinationY, promotionPiece);
    if (isCheck(oppositeColor(getPieceColor(destinationX, destinationY)))) {
      // must move king out of check first
      markSourceReverse(oldSourcePiece, sourceX, sourceY);
      markPiece(oldDestinationPiece, destinationX, destinationY);
      return false;
    }
    return true;
  }

  public void markPiece(
    String piece, int x, int y) {
    guiBoard[x][y].setGUISquare(false, piece);
  }

  public void markSourceReverse(
    String sourcePiece, int sourceX, int sourceY) {
    guiBoard[sourceX][sourceY].setGUISquare(true, sourcePiece);
  }

  private boolean badCoordinate(int x, int y) {
    return x < 0 || x > 7 || y < 0 || y > 7;
  }

  public String getPiece(int x, int y) {
    return guiBoard[x][y].getGUISquare();
  }

  public char getPieceType(int x, int y) {
    String piece = getPiece(x, y);
    if (piece.equals("S"))
      return ' ';
    return piece.charAt(1);
  }

  public char getPieceColor(int x, int y) {
    String piece = getPiece(x, y);
    if (piece.equals("S"))
      return ' ';
    return piece.charAt(0);
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

  private String getAlgebraic(int x, int y) {
    String piece = getPiece(x, y);
    if (piece.equals("S"))
      piece = "";
    return displayX(x) + displayY(y) + piece;
  }

  private void displayPieceList(List pieces) {
    for (int i = 0; i < pieces.size(); i++)
      System.out.print(
        getAlgebraic(
	  ((SquareCoordinate)pieces.get(i)).x,
	  ((SquareCoordinate)pieces.get(i)).y) + " ");
    System.out.println();
  }

  private char oppositeColor(char color) {
    if (color == 'W')
      return 'B';
    else
      return 'W';
  }

  private boolean noPiece(int x, int y) {
    return getPieceType(x, y) == ' ';
  }

  private boolean anyPieceFound(int x, int y) {
    return getPieceType(x, y) != ' ';
  }

  private boolean opponentPiece(char pieceColor, int x, int y) {
    if (badCoordinate(x, y))
      return false;
    return !noPiece(x, y) && getPieceColor(x, y) != pieceColor;
  }

  private boolean friendlyPiece(char pieceColor, int x, int y) {
    if (badCoordinate(x, y))
      return false;
    return !noPiece(x, y) && getPieceColor(x, y) == pieceColor;
  }

  private boolean opponentPawn(char pieceColor, int x, int y) {
    if (badCoordinate(x, y))
      return false;
    return (getPieceType(x, y) == 'P') && getPieceColor(x, y) != pieceColor;
  }

  private boolean friendlyPawn(char pieceColor, int x, int y) {
    if (badCoordinate(x, y))
      return false;
    return (getPieceType(x, y) == 'P') && getPieceColor(x, y) == pieceColor;
  }

  private boolean isKing(int x, int y, char pieceColor) {
    if (badCoordinate(x, y))
      return false;
    return (getPieceType(x, y) == 'K') && getPieceColor(x, y) == pieceColor;
  }

  private boolean opponentKing(int x, int y, char pieceColor) {
    if (badCoordinate(x, y))
      return false;
    return (getPieceType(x, y) == 'K') && getPieceColor(x, y) != pieceColor;
  }

  private boolean friendlyKing(int x, int y, char pieceColor) {
    if (badCoordinate(x, y))
      return false;
    return (getPieceType(x, y) == 'K') && getPieceColor(x, y) == pieceColor;
  }

  private boolean friendlyPieceFound(
    int x, int y, char piece, char pieceColor) {
    if (badCoordinate(x, y))
      return false;
    if (friendlyPiece(pieceColor, x, y) &&
       (getPieceType(x, y) == piece))
      return true;
    return false;
  }

  private boolean opponentPieceFound(
    int x, int y, char piece, char pieceColor) {
    if (badCoordinate(x, y))
      return false;
    if (opponentPiece(pieceColor, x, y) &&
       (getPieceType(x, y) == piece))
      return true;
    return false;
  }

  private boolean interposeBlocksCheck(
    int x, int y, String piece, char pieceType, char pieceColor) {
    String saveOldPiece = guiBoard[x][y].value;
    guiBoard[x][y].value = piece;
    boolean check = isCheck(oppositeColor(pieceColor));
    guiBoard[x][y].value = saveOldPiece;
    return !check;
  }

  private boolean legalPawnMove(
    int sourceX, int sourceY, int destinationX, int destinationY,
    char pieceColor) {
    if (pieceColor == 'W') {
      if (sourceX == destinationX) {
        if (sourceY == 6) {
          if (destinationY == 4)
            if (noPiece(sourceX, 5) && noPiece(sourceX, 4))
              return true;
            else
              return false;
        }
        if (sourceY - 1 == destinationY &&
            noPiece(destinationX, destinationY))
          return true;
      }
      else // try capture
      if (destinationX == sourceX - 1 ||
          destinationX == sourceX + 1) {
        if (destinationY == sourceY - 1 &&
            opponentPiece(pieceColor, destinationX, destinationY))
          return true;
        if (destinationY == sourceY - 1) // try en passant
          if (sourceY == 3)
            if (destinationX == sourceX - 1) {
              if (opponentPawn(pieceColor, sourceX - 1, sourceY))
                return true;
            }
            else // destinationX == sourceY + 1
            if (opponentPawn(pieceColor, sourceX + 1, sourceY))
              return true;
      }
      return false; 
    }
    else { // black pawn
      if (sourceX == destinationX) {
        if (sourceY == 1) {
          if (destinationY == 3)
            if (noPiece(sourceX, 2) && noPiece(sourceX, 3))
              return true;
            else
              return false;
        }
        if (sourceY + 1 == destinationY &&
            noPiece(destinationX, destinationY))
          return true;
      }
      else // try capture
      if (destinationX == sourceX - 1 ||
          destinationX == sourceX + 1) {
        if (destinationY == sourceY + 1 &&
            opponentPiece(pieceColor, destinationX, destinationY))
          return true;
        if (destinationY == sourceY + 1) // try en passant
          if (sourceY == 4)
            if (destinationX == sourceX - 1) {
              if (opponentPawn(pieceColor, sourceX - 1, sourceY))
                return true;
            }
            else // destinationX == sourceY + 1
            if (opponentPawn(pieceColor, sourceX + 1, sourceY))
              return true;
      }
      return false; 
    }
  }

  private boolean interposePawn(
    int x, int y, String piece, char pieceType, char pieceColor) {
    if (pieceColor == 'W') {
      if (y == 6)
        if (noPiece(x, 5) && noPiece(x, 4))
          if (interposeBlocksCheck(x, 4, piece, pieceType, pieceColor))
            return true;
      if (noPiece(x, y - 1))
        if (interposeBlocksCheck(x, y - 1, piece, pieceType, pieceColor))
          return true;
      // try capture
      if (opponentPiece(pieceColor, x - 1, y - 1))
        if (interposeBlocksCheck(x - 1, y - 1, piece, pieceType, pieceColor))
          return true;
      if (opponentPiece(pieceColor, x + 1, y - 1))
        if (interposeBlocksCheck(x + 1, y - 1, piece, pieceType, pieceColor))
          return true;
      // try en passant
      if (y == 3) {
        if (opponentPawn(pieceColor, x - 1, y)) {
          String savePawn = guiBoard[x - 1][y].value;
          guiBoard[x - 1][y].value = "S";
	  boolean blocks =
	    interposeBlocksCheck(x - 1, y - 1, piece, pieceType, pieceColor);
          guiBoard[x - 1][y].value = savePawn;
	  if (blocks)
            return true;
        }
        if (opponentPawn(pieceColor, x + 1, y)) {
          String savePawn = guiBoard[x + 1][y].value;
          guiBoard[x + 1][y].value = "S";
	  boolean blocks =
	    interposeBlocksCheck(x + 1, y - 1, piece, pieceType, pieceColor);
          guiBoard[x + 1][y].value = savePawn;
	  if (blocks)
            return true;
        }
      }
    }
    else { // black pawn
      if (y == 1)
        if (noPiece(x, 2) && noPiece(x, 3))
          if (interposeBlocksCheck(x, 3, piece, pieceType, pieceColor))
            return true;
      if (noPiece(x, y + 1))
        if (interposeBlocksCheck(x, y + 1, piece, pieceType, pieceColor))
          return true;
      // try capture
      if (opponentPiece(pieceColor, x - 1, y + 1))
        if (interposeBlocksCheck(x - 1, y + 1, piece, pieceType, pieceColor))
          return true;
      if (opponentPiece(pieceColor, x + 1, y + 1))
        if (interposeBlocksCheck(x + 1, y + 1, piece, pieceType, pieceColor))
          return true;
      // try en passant
      if (y == 4) {
        if (opponentPawn(pieceColor, x - 1, y)) {
          String savePawn = guiBoard[x - 1][y].value;
          guiBoard[x - 1][y].value = "S";
	  boolean blocks =
	    interposeBlocksCheck(x - 1, y + 1, piece, pieceType, pieceColor);
          guiBoard[x - 1][y].value = savePawn;
	  if (blocks)
            return true;
        }
        if (opponentPawn(pieceColor, x + 1, y)) {
          String savePawn = guiBoard[x + 1][y].value;
          guiBoard[x + 1][y].value = "S";
	  boolean blocks =
	    interposeBlocksCheck(x + 1, y + 1, piece, pieceType, pieceColor);
          guiBoard[x + 1][y].value = savePawn;
	  if (blocks)
            return true;
        }
      }
    }
    return false; 
  }

  private List multiplePawns(
    int sourceX, int destinationX, int destinationY, char pieceColor) {
    List list = new LinkedList();
    if (pieceColor == 'W') {
      if (anyPieceFound(destinationX, destinationY)) // pawn captures piece
        if (friendlyPieceFound(sourceX, destinationY + 1, 'P', pieceColor))
          list.add(new SquareCoordinate(sourceX, destinationY + 1));
      if (friendlyPieceFound(destinationX, destinationY + 1, 'P', pieceColor))
        list.add(new SquareCoordinate(destinationX, destinationY + 1));
      if (destinationY == 4 && noPiece(destinationX, destinationY + 1) &&
         friendlyPieceFound(destinationX, destinationY + 2, 'P', pieceColor))
        list.add(new SquareCoordinate(destinationX, destinationY + 2));
      // en passant
      if (destinationY == 2 && noPiece(destinationX, destinationY) &&
         friendlyPieceFound(sourceX, destinationY + 1, 'P', pieceColor) &&
         (opponentPieceFound(sourceX - 1, destinationY + 1, 'P', pieceColor) ||
          opponentPieceFound(sourceX + 1, destinationY + 1, 'P', pieceColor)))
	list.add(new SquareCoordinate(sourceX, destinationY + 1));
    }
    else { // black
      if (anyPieceFound(destinationX, destinationY)) // pawn captures piece
        if (friendlyPieceFound(sourceX, destinationY - 1, 'P', pieceColor))
          list.add(new SquareCoordinate(sourceX, destinationY - 1));
      if (friendlyPieceFound(destinationX, destinationY - 1, 'P', pieceColor))
        list.add(new SquareCoordinate(destinationX, destinationY - 1));
      if (destinationY == 3 && noPiece(destinationX, destinationY - 1) &&
         friendlyPieceFound(destinationX, destinationY - 2, 'P', pieceColor))
        list.add(new SquareCoordinate(destinationX, destinationY - 2));
      // en passant
      if (destinationY == 5 && noPiece(destinationX, destinationY) &&
         friendlyPieceFound(sourceX, destinationY - 1, 'P', pieceColor) &&
         (opponentPieceFound(sourceX - 1, destinationY - 1, 'P', pieceColor) ||
          opponentPieceFound(sourceX + 1, destinationY - 1, 'P', pieceColor)))
	list.add(new SquareCoordinate(sourceX, destinationY - 1));
    }
    return list;
  }

  private boolean legalKnightKingChoice(
    int sourceX, int sourceY, int destinationX, int destinationY,
    char pieceColor) {
    if (badCoordinate(sourceX, sourceY))
      return false;
    if (sourceX == destinationX && sourceY == destinationY) {
      if (noPiece(destinationX, destinationY))
        return true;
      if (opponentPiece(pieceColor, destinationX, destinationY))
        return true;
    }
    return false;
  }

  private boolean interposeKnightKing(
    int x, int y, String piece, char pieceType, char pieceColor) {
    if (badCoordinate(x, y))
      return false;
    if (opponentPiece(pieceColor, x, y) || noPiece(x, y))
      if (interposeBlocksCheck(x, y, piece, pieceType, pieceColor))
        return true;
    return false;
  }

  private boolean legalKnightMove(
    int sourceX, int sourceY, int destinationX, int destinationY,
    char pieceColor) {
    if (legalKnightKingChoice(
      sourceX + 1, sourceY + 2, destinationX, destinationY, pieceColor))
      return true;
    if (legalKnightKingChoice(
      sourceX + 2, sourceY + 1, destinationX, destinationY, pieceColor))
      return true;
    if (legalKnightKingChoice(
      sourceX + 2, sourceY - 1, destinationX, destinationY, pieceColor))
      return true;
    if (legalKnightKingChoice(
      sourceX + 1, sourceY - 2, destinationX, destinationY, pieceColor))
      return true;
    if (legalKnightKingChoice(
      sourceX - 1, sourceY - 2, destinationX, destinationY, pieceColor))
      return true;
    if (legalKnightKingChoice(
      sourceX - 2, sourceY - 1, destinationX, destinationY, pieceColor))
      return true;
    if (legalKnightKingChoice(
      sourceX - 2, sourceY + 1, destinationX, destinationY, pieceColor))
      return true;
    if (legalKnightKingChoice(
      sourceX - 1, sourceY + 2, destinationX, destinationY, pieceColor))
      return true;
    return false;
  }

  private boolean interposeKnight(
    int x, int y, String piece, char pieceType, char pieceColor) {
    if (interposeKnightKing(
      x + 1, y + 2, piece, pieceType, pieceColor))
      return true;
    if (interposeKnightKing(
      x + 2, y + 1, piece, pieceType, pieceColor))
      return true;
    if (interposeKnightKing(
      x + 2, y - 1, piece, pieceType, pieceColor))
      return true;
    if (interposeKnightKing(
      x + 1, y - 2, piece, pieceType, pieceColor))
      return true;
    if (interposeKnightKing(
      x - 1, y - 2, piece, pieceType, pieceColor))
      return true;
    if (interposeKnightKing(
      x - 2, y - 1, piece, pieceType, pieceColor))
      return true;
    if (interposeKnightKing(
      x - 2, y + 1, piece, pieceType, pieceColor))
      return true;
    if (interposeKnightKing(
      x - 1, y + 2, piece, pieceType, pieceColor))
      return true;
    return false;
  }

  private List multipleKnights(
    int destinationX, int destinationY, char pieceColor) {
    List list = new LinkedList();
    if (friendlyPieceFound(destinationX + 1, destinationY + 2, 'N', pieceColor))
      list.add(new SquareCoordinate(destinationX + 1, destinationY + 2));
    if (friendlyPieceFound(destinationX + 2, destinationY + 1, 'N', pieceColor))
      list.add(new SquareCoordinate(destinationX + 2, destinationY + 1));
    if (friendlyPieceFound(destinationX + 2, destinationY - 1, 'N', pieceColor))
      list.add(new SquareCoordinate(destinationX + 2, destinationY - 1));
    if (friendlyPieceFound(destinationX + 1, destinationY - 2, 'N', pieceColor))
      list.add(new SquareCoordinate(destinationX + 1, destinationY - 2));
    if (friendlyPieceFound(destinationX - 1, destinationY - 2, 'N', pieceColor))
      list.add(new SquareCoordinate(destinationX - 1, destinationY - 2));
    if (friendlyPieceFound(destinationX - 2, destinationY - 1, 'N', pieceColor))
      list.add(new SquareCoordinate(destinationX - 2, destinationY - 1));
    if (friendlyPieceFound(destinationX - 2, destinationY + 1, 'N', pieceColor))
      list.add(new SquareCoordinate(destinationX - 2, destinationY + 1));
    if (friendlyPieceFound(destinationX - 1, destinationY + 2, 'N', pieceColor))
      list.add(new SquareCoordinate(destinationX - 1, destinationY + 2));
    return list;
  }

  private boolean legalBishopRookBranchMove(
    int sourceX, int sourceY, int destinationX, int destinationY,
    int xDirection, int yDirection,
    char pieceColor) {
    if (badCoordinate(sourceX, sourceY))
      return false;
    if (sourceX == destinationX && sourceY == destinationY) {
      if (noPiece(destinationX, destinationY))
        return true;
      if (opponentPiece(pieceColor, destinationX, destinationY))
        return true;
      return false;
    }
    if (!noPiece(sourceX, sourceY))
      return false;
    return legalBishopRookBranchMove(
      sourceX + xDirection,
      sourceY + yDirection,
      destinationX, destinationY,
      xDirection, yDirection, pieceColor);
  }

  private boolean interposeBishopRook(
    int x, int y, int xDirection, int yDirection,
    String piece, char pieceType, char pieceColor) {
    if (badCoordinate(x, y))
      return false;
    if (opponentPiece(pieceColor, x, y))
      return interposeBlocksCheck(x, y, piece, pieceType, pieceColor);
    if (noPiece(x, y)) {
      if (interposeBlocksCheck(x, y, piece, pieceType, pieceColor))
        return true;
    }
    else // friendly piece
      return false;
    return interposeBishopRook(
      x + xDirection, y + yDirection,
      xDirection, yDirection, piece, pieceType, pieceColor);
  }

  private SquareCoordinate bishopRookBranchAttack(
    int sourceX, int sourceY, int xDirection, int yDirection) {
    if (badCoordinate(sourceX, sourceY))
      return null;
    else
    if (anyPieceFound(sourceX, sourceY))
      return new SquareCoordinate(sourceX, sourceY);
    else
    return bishopRookBranchAttack(
      sourceX + xDirection, sourceY + yDirection,
      xDirection, yDirection);
  }

  private boolean legalBishopMove(
    int sourceX, int sourceY, int destinationX, int destinationY,
    char pieceColor) {
    if (legalBishopRookBranchMove(
      sourceX + 1, sourceY + 1, destinationX, destinationY,
      1, 1, pieceColor))
      return true;
    if (legalBishopRookBranchMove(
      sourceX + 1, sourceY - 1, destinationX, destinationY,
      1, -1, pieceColor))
      return true;
    if (legalBishopRookBranchMove(
      sourceX - 1, sourceY + 1, destinationX, destinationY,
      -1, 1, pieceColor))
      return true;
    if (legalBishopRookBranchMove(
      sourceX - 1, sourceY - 1, destinationX, destinationY,
      -1, -1, pieceColor))
      return true;
    return false;
  }

  private boolean interposeBishop(
    int x, int y, String piece, char pieceType, char pieceColor) {
    if (interposeBishopRook(
      x + 1, y + 1, 1, 1, piece, pieceType, pieceColor))
      return true;
    if (interposeBishopRook(
      x + 1, y - 1, 1, -1, piece, pieceType, pieceColor))
      return true;
    if (interposeBishopRook(
      x - 1, y + 1, -1, 1, piece, pieceType, pieceColor))
      return true;
    if (interposeBishopRook(
      x - 1, y - 1, -1, -1, piece, pieceType, pieceColor))
      return true;
    return false;
  }

  private SquareCoordinate bishopRookBranchFound(
    int x, int y, int xDirection, int yDirection, char piece, char pieceColor) {
    if (badCoordinate(x, y))
      return null;
    if (friendlyPieceFound(x, y, piece, pieceColor))
      return new SquareCoordinate(x, y);
    if (anyPieceFound(x, y))
      return null;
    return bishopRookBranchFound(
      x + xDirection,
      y + yDirection,
      xDirection, yDirection, piece, pieceColor);
  }

  private List multipleBishops(
    int destinationX, int destinationY, char piece, char pieceColor) {
    List list = new LinkedList();
    SquareCoordinate squareCoordinate;
    squareCoordinate = bishopRookBranchFound(
      destinationX + 1, destinationY + 1, 1, 1, piece, pieceColor);
    if (squareCoordinate != null)
      list.add(squareCoordinate);
    squareCoordinate = bishopRookBranchFound(
      destinationX + 1, destinationY - 1, 1, -1, piece, pieceColor);
    if (squareCoordinate != null)
      list.add(squareCoordinate);
    squareCoordinate = bishopRookBranchFound(
      destinationX - 1, destinationY + 1, -1, 1, piece, pieceColor);
    if (squareCoordinate != null)
      list.add(squareCoordinate);
    squareCoordinate = bishopRookBranchFound(
      destinationX - 1, destinationY - 1, -1, -1, piece, pieceColor);
    if (squareCoordinate != null)
      list.add(squareCoordinate);
    return list;
  }

  private boolean legalRookMove(
    int sourceX, int sourceY, int destinationX, int destinationY,
    char pieceColor) {
    if (legalBishopRookBranchMove(
      sourceX, sourceY + 1, destinationX, destinationY,
      0, 1, pieceColor))
      return true;
    if (legalBishopRookBranchMove(
      sourceX + 1, sourceY, destinationX, destinationY,
      1, 0, pieceColor))
      return true;
    if (legalBishopRookBranchMove(
      sourceX, sourceY - 1, destinationX, destinationY,
      0, -1, pieceColor))
      return true;
    if (legalBishopRookBranchMove(
      sourceX - 1, sourceY, destinationX, destinationY,
      -1, 0, pieceColor))
      return true;
    return false;
  }

  private boolean interposeRook(
    int x, int y, String piece, char pieceType, char pieceColor) {
    if (interposeBishopRook(
      x, y + 1, 0, 1, piece, pieceType, pieceColor))
      return true;
    if (interposeBishopRook(
      x + 1, y, 1, 0, piece, pieceType, pieceColor))
      return true;
    if (interposeBishopRook(
      x, y - 1, 0, -1, piece, pieceType, pieceColor))
      return true;
    if (interposeBishopRook(
      x - 1, y, -1, 0, piece, pieceType, pieceColor))
      return true;
    return false;
  }

  private List multipleRooks(
    int destinationX, int destinationY, char piece, char pieceColor) {
    List list = new LinkedList();
    SquareCoordinate squareCoordinate;
    squareCoordinate = bishopRookBranchFound(
      destinationX, destinationY + 1, 0, 1, piece, pieceColor);
    if (squareCoordinate != null)
      list.add(squareCoordinate);
    squareCoordinate = bishopRookBranchFound(
      destinationX + 1, destinationY, 1, 0, piece, pieceColor);
    if (squareCoordinate != null)
      list.add(squareCoordinate);
    squareCoordinate = bishopRookBranchFound(
      destinationX, destinationY - 1, 0, -1, piece, pieceColor);
    if (squareCoordinate != null)
      list.add(squareCoordinate);
    squareCoordinate = bishopRookBranchFound(
      destinationX - 1, destinationY, -1, 0, piece, pieceColor);
    if (squareCoordinate != null)
      list.add(squareCoordinate);
    return list;
  }

  private boolean legalQueenMove(
    int sourceX, int sourceY, int destinationX, int destinationY,
    char pieceColor) {
    if (legalBishopMove(
      sourceX, sourceY, destinationX, destinationY, pieceColor))
      return true;
    if (legalRookMove(
      sourceX, sourceY, destinationX, destinationY, pieceColor))
      return true;
    return false;
  }

  private boolean interposeQueen(
    int x, int y, String piece, char pieceType, char pieceColor) {
    if (interposeBishop(x, y, piece, pieceType, pieceColor))
      return true;
    return interposeRook(x, y, piece, pieceType, pieceColor);
  }

  private void append(List list1, List list2) {
    for (int i = 0; i < list2.size(); i++)
      list1.add(list2.get(i));
  }

  private List multipleQueens(
    int destinationX, int destinationY, char piece, char pieceColor) {
    List list1 =
      multipleBishops(destinationX, destinationY, piece, pieceColor);
    List list2 =
      multipleRooks(destinationX, destinationY, piece, pieceColor);
    append(list1, list2);
    return list1;
  }

  private boolean legalKingMove(
    int sourceX, int sourceY, int destinationX, int destinationY,
    char pieceColor) {
    if (legalKnightKingChoice(
      sourceX + 1, sourceY + 1, destinationX, destinationY, pieceColor))
      return true;
    if (legalKnightKingChoice(
      sourceX + 1, sourceY, destinationX, destinationY, pieceColor))
      return true;
    if (legalKnightKingChoice(
      sourceX + 1, sourceY - 1, destinationX, destinationY, pieceColor))
      return true;
    if (legalKnightKingChoice(
      sourceX, sourceY - 1, destinationX, destinationY, pieceColor))
      return true;
    if (legalKnightKingChoice(
      sourceX - 1, sourceY - 1, destinationX, destinationY, pieceColor))
      return true;
    if (legalKnightKingChoice(
      sourceX - 1, sourceY, destinationX, destinationY, pieceColor))
      return true;
    if (legalKnightKingChoice(
      sourceX - 1, sourceY + 1, destinationX, destinationY, pieceColor))
      return true;
    if (legalKnightKingChoice(
      sourceX, sourceY + 1, destinationX, destinationY, pieceColor))
      return true;
    if (pieceColor == 'W') {
      if (sourceX == 4 && sourceY == 7 &&
          destinationX == 6 && destinationY == 7 &&
          noPiece(sourceX + 1, sourceY) && 
          noPiece(sourceX + 2, sourceY) && 
          getPiece(sourceX + 3, sourceY).equals("WR"))
        return true;
      if (sourceX == 4 && sourceY == 7 &&
          destinationX == 2 && destinationY == 7 &&
          noPiece(sourceX - 1, sourceY) && 
          noPiece(sourceX - 2, sourceY) && 
          noPiece(sourceX - 3, sourceY) && 
          getPiece(sourceX - 4, sourceY).equals("WR"))
        return true;
    }
    else {
      if (sourceX == 4 && sourceY == 0 &&
          destinationX == 6 && destinationY == 0 &&
          noPiece(sourceX + 1, sourceY) && 
          noPiece(sourceX + 2, sourceY) && 
          getPiece(sourceX + 3, sourceY).equals("BR"))
        return true;
      if (sourceX == 4 && sourceY == 0 &&
          destinationX == 2 && destinationY == 0 &&
          noPiece(sourceX - 1, sourceY) && 
          noPiece(sourceX - 2, sourceY) && 
          noPiece(sourceX - 3, sourceY) && 
          getPiece(sourceX - 4, sourceY).equals("BR"))
        return true;
    }
    return false;
  }

  private boolean interposeKing(
    int x, int y, String piece, char pieceType, char pieceColor) {
    if (interposeKnightKing(
      x + 1, y + 1, piece, pieceType, pieceColor))
      return true;
    if (interposeKnightKing(
      x + 1, y, piece, pieceType, pieceColor))
      return true;
    if (interposeKnightKing(
      x + 1, y - 1, piece, pieceType, pieceColor))
      return true;
    if (interposeKnightKing(
      x, y - 1, piece, pieceType, pieceColor))
      return true;
    if (interposeKnightKing(
      x - 1, y - 1, piece, pieceType, pieceColor))
      return true;
    if (interposeKnightKing(
      x - 1, y, piece, pieceType, pieceColor))
      return true;
    if (interposeKnightKing(
      x - 1, y + 1, piece, pieceType, pieceColor))
      return true;
    if (interposeKnightKing(
      x, y + 1, piece, pieceType, pieceColor))
      return true;
    return false;
  }

  private List multipleKings(
    int destinationX, int destinationY, char piece, char pieceColor) {
    List list = new LinkedList();
    if (friendlyPieceFound(destinationX - 1, destinationY - 1, piece, pieceColor))
      list.add(new SquareCoordinate(destinationX - 1, destinationY - 1));
    if (friendlyPieceFound(destinationX - 1, destinationY, piece, pieceColor))
      list.add(new SquareCoordinate(destinationX - 1, destinationY));
    if (friendlyPieceFound(destinationX - 1, destinationY + 1, piece, pieceColor))
      list.add(new SquareCoordinate(destinationX - 1, destinationY + 1));
    if (friendlyPieceFound(destinationX, destinationY - 1, piece, pieceColor))
      list.add(new SquareCoordinate(destinationX, destinationY - 1));
    if (friendlyPieceFound(destinationX, destinationY + 1, piece, pieceColor))
      list.add(new SquareCoordinate(destinationX, destinationY + 1));
    if (friendlyPieceFound(destinationX + 1, destinationY - 1, piece, pieceColor))
      list.add(new SquareCoordinate(destinationX + 1, destinationY - 1));
    if (friendlyPieceFound(destinationX + 1, destinationY, piece, pieceColor))
      list.add(new SquareCoordinate(destinationX + 1, destinationY));
    if (friendlyPieceFound(destinationX + 1, destinationY + 1, piece, pieceColor))
      list.add(new SquareCoordinate(destinationX + 1, destinationY + 1));
    return list;
  }

  private boolean legalMove(
    int sourceX, int sourceY, int destinationX, int destinationY) {
    char pieceType = getPieceType(sourceX, sourceY);
    char pieceColor = getPieceColor(sourceX, sourceY);
    if (pieceType == 'P')
      return legalPawnMove(
        sourceX, sourceY, destinationX, destinationY, pieceColor);
    else
    if (pieceType == 'N')
      return legalKnightMove(
        sourceX, sourceY, destinationX, destinationY, pieceColor);
    else
    if (pieceType == 'B')
      return legalBishopMove(
        sourceX, sourceY, destinationX, destinationY, pieceColor);
    else
    if (pieceType == 'R')
      return legalRookMove(
        sourceX, sourceY, destinationX, destinationY, pieceColor);
    else
    if (pieceType == 'Q')
      return legalQueenMove(
        sourceX, sourceY, destinationX, destinationY, pieceColor);
    else
    if (pieceType == 'K')
      return legalKingMove(
        sourceX, sourceY, destinationX, destinationY, pieceColor);
    else
      return false;
  }

  private List removePins(int destinationX, int destinationY, List list) {
    SquareCoordinate squareCoordinate; 
    boolean pinned;
    ListIterator listIterator = list.listIterator();
    while (listIterator.hasNext()) {
      squareCoordinate = (SquareCoordinate)listIterator.next();
      int x = squareCoordinate.x;
      int y = squareCoordinate.y;
      String sourcePiece = getPiece(x, y);
      char color = getPieceColor(x, y);
      if (!sourcePiece.equals("S")) {
	guiBoard[x][y].value = "S";
        pinned = isCheck(oppositeColor(color));
	guiBoard[x][y].value = sourcePiece;
	if (pinned)
	  listIterator.remove();
      }
    }

    return list;
  }

  private boolean removal(
    ListIterator listIterator,
    int kingX, int kingY,
    SquareCoordinate squareCoordinate, int destinationX, int destinationY,
    SquareCoordinate attackCoordinate, char pieceColor, int attackType) {
    SquareCoordinate pinningCoordinate = null;
    String pinningPiece = "";
    boolean removePiece = false;
    if (squareCoordinate.equals(attackCoordinate)) {
      String sourcePiece = getPiece(squareCoordinate.x, squareCoordinate.y);
      String destPiece = getPiece(destinationX, destinationY);
      guiBoard[squareCoordinate.x][squareCoordinate.y].value = "S";
      guiBoard[destinationX][destinationY].value = sourcePiece;
      if (attackType == 1)
        pinningCoordinate = bishopRookBranchAttack(kingX + 1, kingY + 1, 1, 1);
      else
      if (attackType == 2)
        pinningCoordinate = bishopRookBranchAttack(kingX + 1, kingY - 1, 1, -1);
      else
      if (attackType == 3)
        pinningCoordinate = bishopRookBranchAttack(kingX - 1, kingY + 1, -1, 1);
      else
      if (attackType == 4)
        pinningCoordinate = bishopRookBranchAttack(kingX - 1, kingY - 1, -1, -1);
      else
      if (attackType == 5)
        pinningCoordinate = bishopRookBranchAttack(kingX, kingY + 1, 0, 1);
      else
      if (attackType == 6)
        pinningCoordinate = bishopRookBranchAttack(kingX + 1, kingY, 1, 0);
      else
      if (attackType == 7)
        pinningCoordinate = bishopRookBranchAttack(kingX, kingY - 1, 0, -1);
      else
        pinningCoordinate = bishopRookBranchAttack(kingX - 1, kingY, -1, 0);
      if (pinningCoordinate != null) {
	pinningPiece = getPiece(pinningCoordinate.x, pinningCoordinate.y);
	if (attackType <= 4) { // diagonal
	  if (pieceColor == 'W') {
	    if (pinningPiece.equals("BQ") || pinningPiece.equals("BB"))
	      removePiece = true;
	  }
	  else {
	    if (pinningPiece.equals("WQ") || pinningPiece.equals("WB"))
	      removePiece = true;
	  }
	}
	else { // rank or file
	  if (pieceColor == 'W') {
	    if (pinningPiece.equals("BQ") || pinningPiece.equals("BR"))
	      removePiece = true;
	  }
	  else {
	    if (pinningPiece.equals("WQ") || pinningPiece.equals("WR"))
	      removePiece = true;
	  }
	}
      }
      guiBoard[squareCoordinate.x][squareCoordinate.y].value = sourcePiece;
      guiBoard[destinationX][destinationY].value = destPiece;
    }
    if (removePiece)
      listIterator.remove();
    return removePiece;
  }

  List removePins2(
    int destinationX, int destinationY, List list) {
    SquareCoordinate kingCoordinate;
    SquareCoordinate attackPieceCoordinate;
    SquareCoordinate squareCoordinate;

    int size = list.size();
    if (size == 0)
      return list;

    char pieceColor =
      getPieceColor(
        ((SquareCoordinate)list.get(0)).x,
        ((SquareCoordinate)list.get(0)).y);

    kingCoordinate = findKing(pieceColor);
    int kingX = kingCoordinate.x;
    int kingY = kingCoordinate.y;

    ListIterator listIterator = list.listIterator();
    while (listIterator.hasNext()) {
      squareCoordinate = (SquareCoordinate)listIterator.next();
      attackPieceCoordinate = bishopRookBranchAttack(
        kingX + 1, kingY + 1,
        1, 1);
      if (!removal(listIterator,
	kingX, kingY,
	squareCoordinate, destinationX, destinationY,
	attackPieceCoordinate, pieceColor, 1)) {
        attackPieceCoordinate = bishopRookBranchAttack(
          kingX + 1, kingY - 1,
          1, -1);
        if (!removal(listIterator,
	  kingX, kingY,
	  squareCoordinate, destinationX, destinationY,
	  attackPieceCoordinate, pieceColor, 2)) {
          attackPieceCoordinate = bishopRookBranchAttack(
            kingX - 1, kingY + 1,
            -1, 1);
          if (!removal(listIterator,
	    kingX, kingY,
	    squareCoordinate, destinationX, destinationY,
	    attackPieceCoordinate, pieceColor, 3)) {
            attackPieceCoordinate = bishopRookBranchAttack(
              kingX - 1, kingY - 1,
              -1, -1);
            if (!removal(listIterator,
	      kingX, kingY,
	      squareCoordinate, destinationX, destinationY,
	      attackPieceCoordinate, pieceColor, 4)) {
              attackPieceCoordinate = bishopRookBranchAttack(
                kingX, kingY + 1,
                0, 1);
              if (!removal(listIterator,
	        kingX, kingY,
		squareCoordinate, destinationX, destinationY,
		attackPieceCoordinate, pieceColor, 5)) {
                attackPieceCoordinate = bishopRookBranchAttack(
                  kingX + 1, kingY,
                  1, 0);
                if (!removal(listIterator,
	          kingX, kingY,
		  squareCoordinate, destinationX, destinationY,
		  attackPieceCoordinate, pieceColor, 6)) {
                  attackPieceCoordinate = bishopRookBranchAttack(
                    kingX, kingY - 1,
                    0, -1);
                  if (!removal(listIterator,
	            kingX, kingY,
		    squareCoordinate, destinationX, destinationY,
		    attackPieceCoordinate, pieceColor, 7)) {
                    attackPieceCoordinate = bishopRookBranchAttack(
                      kingX - 1, kingY,
                      -1, 0);
                    removal(listIterator,
	              kingX, kingY,
		      squareCoordinate, destinationX, destinationY,
		      attackPieceCoordinate, pieceColor, 8);
                  }
                }
              }
            }
	  }
	}
      }
    }

    return list;
  }

  private SquareCoordinate selectOnePiece(
    int sourceX, int sourceY, int destinationX, int destinationY, List list) {
    list = removePins2(destinationX, destinationY, list);

    int size = list.size();

    if (size == 0)
      return null;
    if (sourceX == -1 && sourceY == -1) {
      if (size != 1)
        return null;
      return (SquareCoordinate)list.get(0);
    }
    if (sourceX != -1 && sourceY != -1) {
      if (size != 1)
        return null;
      SquareCoordinate squareCoordinate = (SquareCoordinate)list.get(0);
      if (sourceX == squareCoordinate.x && sourceY == squareCoordinate.y)
        return squareCoordinate;
      return null;
    }
    if (sourceX == -1) {
      int count = 0;
      int match = 0;
      for (int i = 0; i < size; i++)
	if (sourceY == ((SquareCoordinate)list.get(i)).y) {
          count++;
	  match = i;
	}
      if (count == 1)
        return (SquareCoordinate)list.get(match);
      return null;
    }
    else {
      int count = 0;
      int match = 0;
      for (int i = 0; i < size; i++)
	if (sourceX == ((SquareCoordinate)list.get(i)).x) {
          count++;
	  match = i;
	}
      if (count == 1)
        return (SquareCoordinate)list.get(match);
      return null;
    }
  }

  private SquareCoordinate selectStatus(
    int sourceX, int sourceY,
    int destinationX, int destinationY, List list) {
    list = removePins(destinationX, destinationY, list);
    int size = list.size();
    if (size == 1)
      return new SquareCoordinate(-1, -1);
    int count;
    count = 0;
    for (int i = 0; i < size; i++)
      if (sourceX == ((SquareCoordinate)list.get(i)).x)
        count++;
    if (count == 1)
      return new SquareCoordinate(destinationX, -1);
    count = 0;
    for (int i = 0; i < size; i++)
      if (sourceY == ((SquareCoordinate)list.get(i)).y)
        count++;
    if (count == 1)
      return new SquareCoordinate(-1, destinationY);
    return new SquareCoordinate(destinationX, destinationY);
  }

  public SquareCoordinate getMultiplePieceStatus(
    char pieceType, char pieceColor,
    int sourceX, int sourceY, int destinationX, int destinationY) {
    SquareCoordinate status;
    guiBoard[sourceX][sourceY].value =
      String.valueOf(pieceColor) + String.valueOf(pieceType); 
    if (pieceType == 'N')
      status = selectStatus(sourceX, sourceY, destinationX, destinationY, 
	multipleKnights(destinationX, destinationY, pieceColor));
    else
    if (pieceType == 'B')
      status = selectStatus(sourceX, sourceY, destinationX, destinationY,
        multipleBishops(destinationX, destinationY, pieceType, pieceColor));
    else
    if (pieceType == 'R')
      status = selectStatus(sourceX, sourceY, destinationX, destinationY,
        multipleRooks(destinationX, destinationY, pieceType, pieceColor));
    else
    if (pieceType == 'Q')
      status = selectStatus(sourceX, sourceY, destinationX, destinationY,
        multipleQueens(destinationX, destinationY, pieceType, pieceColor));
    else
    if (pieceType == 'K')
      status = selectStatus(sourceX, sourceY, destinationX, destinationY,
        multipleKings(destinationX, destinationY, pieceType, pieceColor));
    else
    if (pieceType == 'P')
      status = selectStatus(sourceX, sourceY, destinationX, destinationY,
        multiplePawns(sourceX, destinationX, destinationY, pieceColor));
    else
      status = null;
    guiBoard[sourceX][sourceY].value = "S"; 
    return status;
  }

  public SquareCoordinate selectOnePieceFromMany(
    char pieceType, char pieceColor,
    int sourceX, int sourceY, int destinationX, int destinationY) {
    SquareCoordinate squareCoordinate;
    if (pieceType == 'N')
      squareCoordinate = selectOnePiece(
	sourceX, sourceY, destinationX, destinationY,
	multipleKnights(destinationX, destinationY, pieceColor));
    else
    if (pieceType == 'B')
      squareCoordinate = selectOnePiece(
	sourceX, sourceY, destinationX, destinationY,
        multipleBishops(destinationX, destinationY, pieceType, pieceColor));
    else
    if (pieceType == 'R')
      squareCoordinate = selectOnePiece(
	sourceX, sourceY, destinationX, destinationY,
        multipleRooks(destinationX, destinationY, pieceType, pieceColor));
    else
    if (pieceType == 'Q')
      squareCoordinate = selectOnePiece(
	sourceX, sourceY, destinationX, destinationY,
        multipleQueens(destinationX, destinationY, pieceType, pieceColor));
    else
    if (pieceType == 'K')
      squareCoordinate = selectOnePiece(
	sourceX, sourceY, destinationX, destinationY,
        multipleKings(destinationX, destinationY, pieceType, pieceColor));
    else
    if (pieceType == 'P')
      squareCoordinate = selectOnePiece(
	sourceX, sourceY, destinationX, destinationY,
        multiplePawns(sourceX, destinationX, destinationY, pieceColor));
    else
      squareCoordinate = null;
    return squareCoordinate;
  }

  private boolean pawnCheck(int x, int y, char pieceColor) {
    if (pieceColor == 'W')
      return friendlyPawn(pieceColor, x + 1, y + 1) ||
             friendlyPawn(pieceColor, x - 1, y + 1);
    else
      return friendlyPawn(pieceColor, x + 1, y - 1) ||
             friendlyPawn(pieceColor, x - 1, y - 1);
  }

  private SquareCoordinate findKing(char pieceColor) {
    for (int x = 0; x < 8; x++)
      for (int y = 0; y < 8; y++)
        if (isKing(x, y, pieceColor))
          return new SquareCoordinate(x, y);
    return null;
  }

  private SquareCoordinate findOpponentKing(char pieceColor) {
    for (int x = 0; x < 8; x++)
      for (int y = 0; y < 8; y++)
        if (opponentKing(x, y, pieceColor))
	  return new SquareCoordinate(x, y);
    return null;
  }

  public boolean isCheck(char pieceColor) {
    SquareCoordinate king = findOpponentKing(pieceColor);
    if (king == null)
      return false;
    int kingX = king.x;
    int kingY = king.y;
    if (multipleKnights(kingX, kingY, pieceColor).size() != 0 ||
        multipleBishops(kingX, kingY, 'B', pieceColor).size() != 0 ||
        multipleRooks(kingX, kingY, 'R', pieceColor).size() != 0 ||
        multipleQueens(kingX, kingY, 'Q', pieceColor).size() != 0 ||
        multipleKings(kingX, kingY, 'K', pieceColor).size() != 0)
      return true;
    if (pawnCheck(kingX, kingY, pieceColor))
      return true;
    return false;
  }

  private boolean kingInterposed(int x, int y) {
    char pieceType, pieceColor;
    String piece;
    boolean interposed = false;
    pieceColor = getPieceColor(x, y);
    pieceType = getPieceType(x, y);
    piece = getPiece(x, y);
    guiBoard[x][y].value = "S";
    if (pieceType == 'K')
      interposed = interposeKing(x, y, piece, 'K', pieceColor);
    else
    if (pieceType == 'N')
      interposed = interposeKnight(x, y, piece, 'N', pieceColor);
    else
    if (pieceType == 'B')
      interposed = interposeBishop(x, y, piece, 'B', pieceColor);
    else
    if (pieceType == 'R')
      interposed = interposeRook(x, y, piece, 'R', pieceColor);
    else
    if (pieceType == 'Q')
      interposed = interposeQueen(x, y, piece, 'Q', pieceColor);
    else
    if (pieceType == 'P')
      interposed = interposePawn(x, y, piece, 'P', pieceColor);
    guiBoard[x][y].value = piece;
    return interposed;
  }

  public boolean isCheckmate(char moveColor) {
    char pieceColor;
    char oppositeColor = oppositeColor(moveColor);
    for (int y = 0; y < 8; y++)
      for (int x = 0; x < 8; x++) {
        pieceColor = getPieceColor(x, y);
	if (oppositeColor == pieceColor) {
	  if (kingInterposed(x, y))
	    return false;
	}
      }
    return true;
  }

  public String checkValue(char pieceColor) {
    if (isCheck(pieceColor)) {
      if (isCheckmate(pieceColor))
        return "#";
      else
        return "+";
    }
    else
      return "";
  }
}
