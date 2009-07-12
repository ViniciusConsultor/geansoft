import java.io.*;
import java.util.*;

class Chess {
  private ChessEditor chessEditor;

  public GUIBoard guiBoard;

  public boolean moveInProgress;
  private boolean whiteMove;

  private String sourcePiece;
  private int sourceX;
  private int sourceY;
  private String destinationPiece;
  private int destinationX;
  private int destinationY;

  private MoveLog moveLog;
  private MoveLog currentMove;
  public MoveLog startMove;

  public Hashtable tags;

  Chess(ChessEditor chessEditor) {
    this.chessEditor = chessEditor;
    guiBoard = chessEditor.board;
    newGame();
  }

  Chess(ChessEditor chessEditor, Hashtable oldTags) {
    this.chessEditor = chessEditor;
    guiBoard = chessEditor.board;
    newGameKeepTags(oldTags);
  }

  public boolean makeMove(int x, int y) {
    if (!moveInProgress) { // start move
      if (currentMove != null)
        if (currentMove.gameResult != null) // game already has result
          return false;
      sourceX = x;
      sourceY = y;
      sourcePiece = guiBoard.getPiece(sourceX, sourceY);
      if (sourcePiece.equals("S"))
        return false; // square has no piece
      char pieceColor = guiBoard.getPieceColor(sourceX, sourceY);
      if (whiteMove) {
        if (pieceColor != 'W')
          return false; // it is whites move
      }
      else
        if (pieceColor != 'B')
          return false; // it is blacks move

      guiBoard.markSourceReverse(sourcePiece, sourceX, sourceY);

      moveInProgress = true;
      return true;
    }
    else { // finish move
      destinationX = x;
      destinationY = y;
      destinationPiece = guiBoard.getPiece(destinationX, destinationY);

      if (sourceX == destinationX && sourceY == destinationY) { // no move
        guiBoard.markPiece(sourcePiece, sourceX, sourceY); // clear marked
        moveInProgress = false;
        return true;
      }

      char pieceType = guiBoard.getPieceType(sourceX, sourceY);
      char pieceColor = guiBoard.getPieceColor(sourceX, sourceY);
      Promotion.getInstance().clearPiece();
      if (guiBoard.makeMove(
        "S", sourceX, sourceY,
        sourcePiece, destinationX, destinationY, null, currentMove)) {
	String checkValue = getCheckValue(pieceColor);
        logMove(
	  guiBoard.getMultiplePieceStatus(
            pieceType, pieceColor, sourceX, sourceY,
	    destinationX, destinationY),
          checkValue,
	  Promotion.getInstance().getPiece());
	autoWin(checkValue, whiteMove);
	displayLog();
	engineMove(currentMove.currentMoveString());
        moveInProgress = false;
        whiteMove = !whiteMove;
        return true;
      }
      else
        return false;
    }
  }

  public void engineMove(String move) {
    if (chessEditor.engineProcess != null)
      chessEditor.engineProcess.talkToEngine(move);
//  System.out.println(move);
  }

  private String getCheckValue(char pieceColor) {
    return guiBoard.checkValue(pieceColor);
  }

  private void autoWin(String checkValue, boolean whiteMove) {
    if (checkValue.equals("#")) // checkmate
      if (whiteMove)
        gameResult("1-0");
      else
        gameResult("0-1");
  }

  private void logMove(
    SquareCoordinate multiplePiece, String check, String promotionPiece) {
    moveLog =
      new MoveLog(currentMove,
        sourcePiece, sourceX, sourceY,
        destinationPiece, destinationX, destinationY,
        multiplePiece, check, promotionPiece);
    if (currentMove == null)
      startMove = moveLog;
    currentMove = moveLog;
  }

  public void displayLog() {
    if (startMove != null)
      startMove.displayLog(1, currentMove, chessEditor.textArea);
  }

  public void stepBackOneMove(boolean skip) {
    if (currentMove != null) {
      sourcePiece = currentMove.destinationPiece;
      sourceX = currentMove.destinationX;
      sourceY = currentMove.destinationY;
      destinationPiece = currentMove.sourcePiece;
      destinationX = currentMove.sourceX;
      destinationY = currentMove.sourceY;
      guiBoard.makeMoveNoCheck(
        sourcePiece, sourceX, sourceY,
        destinationPiece, destinationX, destinationY,
	currentMove.promotionPiece);

      whiteMove = !whiteMove;
      currentMove = currentMove.previousMove;
      if (!skip) {
	if (currentMove == null)
	  engineMove("new");
	else
	  engineMove("undo");
      }
    }
  }

  public void stepForwardOneMove() {
    if (currentMove == null) {
      currentMove = startMove;
      if (currentMove != null) {
        stepForwardOneMoveNow();
      }
    }
    else
    if (currentMove.nextMove != null) {
      currentMove = currentMove.nextMove;
      stepForwardOneMoveNow();
    }
  }

  private void stepForwardOneMoveNow() {
    sourcePiece = currentMove.sourcePiece;
    sourceX = currentMove.sourceX;
    sourceY = currentMove.sourceY;
    destinationPiece = currentMove.destinationPiece;
    destinationX = currentMove.destinationX;
    destinationY = currentMove.destinationY;
    guiBoard.makeMoveNoCheck(
      "S", sourceX, sourceY,
      sourcePiece, destinationX, destinationY,
      currentMove.promotionPiece);
    whiteMove = !whiteMove;
    engineMove(currentMove.currentMoveString());
  }

  public void stepToStart() {
    while (currentMove != null)
      stepBackOneMove(true);
    engineMove("new");
  }

  public void stepToEnd() {
    if (currentMove == null) {
      currentMove = startMove;
      if (currentMove != null) {
        stepForwardOneMoveNow();
	stepToEnd();
      }
    }
    else
    if (currentMove.nextMove != null) {
      currentMove = currentMove.nextMove;
      stepForwardOneMoveNow();
      stepToEnd();
    }
  }

  public void syncEngine(MoveLog syncMove) {
    if (currentMove == null)
      return;
    engineMove(syncMove.currentMoveString());
    if (syncMove == currentMove)
      return;
    syncEngine(syncMove.nextMove);
  }

  public void newGame() {
    guiBoard.initializeBoardPieces();
    moveLog = null;
    currentMove = null;
    startMove = null;
    moveInProgress = false;
    whiteMove = true;
    chessEditor.textArea.setText(""); // clear text
    setDefaultTags();
    addUserTags(chessEditor.userTags);
    displayTags();
    engineMove("new");
  }

  public void newGameKeepTags(Hashtable oldTags) {
    guiBoard.initializeBoardPieces();
    moveLog = null;
    currentMove = null;
    startMove = null;
    moveInProgress = false;
    whiteMove = true;
    chessEditor.textArea.setText(""); // clear text
    copyTags(oldTags);
    addUserTags(chessEditor.userTags);
    displayTags();
    engineMove("new");
  }

  public void gameResult(String result) {
    if (currentMove != null) {
      currentMove.setGameResult(result, this);
      currentMove.nextMove = null;
      displayLog();
    }
    else
      setTagValue("Result", result);
  }

  public boolean playPGNGame(PGNLex pgnLex) {
    boolean returnCode = true;
    newGame();
    String token = pgnLex.getToken(whiteMove);
    while (!pgnLex.eofToken())  {
      if (pgnLex.tokenType == PGNLex.TT_IDENTIFIER) {
        if (pgnLex.validParameters) {
	  char pieceType = pgnLex.piece;
	  sourceX = pgnLex.sourceX;
	  sourceY = pgnLex.sourceY;
	  destinationX = pgnLex.destX;
	  destinationY = pgnLex.destY;
          destinationPiece = guiBoard.getPiece(destinationX, destinationY);

          char pieceColor;
          if (whiteMove)
            pieceColor = 'W';
          else
            pieceColor = 'B';
          sourcePiece =
	    String.valueOf(pieceColor) + String.valueOf(pieceType);
	  if (sourceX == -1 || sourceY == -1) {
            SquareCoordinate squareCoordinate =
              guiBoard.selectOnePieceFromMany(
                pieceType, pieceColor,
	        sourceX, sourceY, destinationX, destinationY);
	    if (squareCoordinate == null) {
	      displayLog();
	      badMove(token);
	      returnCode = false;
              break;
	    }
	    sourceX = squareCoordinate.x;
	    sourceY = squareCoordinate.y;
	  }
          Promotion.getInstance().clearPiece();
          if (guiBoard.makeMove(
            "S", sourceX, sourceY,
            sourcePiece, destinationX, destinationY, pgnLex.promotionPiece,
	    null)) {
	    String checkValue = getCheckValue(pieceColor);
            logMove(
	      guiBoard.getMultiplePieceStatus(
                pieceType, pieceColor,
	        sourceX, sourceY, destinationX, destinationY),
	      checkValue,
	      Promotion.getInstance().getPiece());
	    autoWin(checkValue, whiteMove);
            whiteMove = !whiteMove;
          }
          else {
	    displayLog();
	    badMove(token);
	    returnCode = false;
            break;
          }
	}
        else {
	  displayLog();
	  invalidToken(token);
	  returnCode = false;
          break;
        }
      }
      else
      if (pgnLex.tokenType == PGNLex.TT_OUTCOME) {
        gameResult(token);
	break;
      }
      else
      if (pgnLex.tokenType == PGNLex.TT_OPEN_TAG) {
        token = pgnLex.getToken(whiteMove);
        if (pgnLex.tokenType != PGNLex.TT_IDENTIFIER) {
          badAttribute(token);
	  returnCode = false;
	  break;
	}
	String tagName = token;
        token = pgnLex.getToken(whiteMove);
        if (pgnLex.tokenType != PGNLex.TT_STRING) {
          badAttribute(token);
	  returnCode = false;
	  break;
	}
	String tagValue = token;
        token = pgnLex.getToken(whiteMove);
        if (pgnLex.tokenType != PGNLex.TT_CLOSE_TAG) {
          badAttribute(token);
	  returnCode = false;
	  break;
	}
	setTagValue(tagName, tagValue);
      }

      token = pgnLex.getToken(whiteMove);
    }
    displayLog();
    displayTags();
    return returnCode;
  }

  public String getTagValue(String name) {
    if (tags.containsKey(name))
      return (String)tags.get(name);
    else
      return "";
  }

  public void setTagValue(String name, String value) {
    tags.put(name, value);
  }

  private void setDefaultTags() {
    tags = new Hashtable();
    setTagValue("Event", "");
    setTagValue("Site", "");
    setTagValue("Date", "");
    setTagValue("Round", "");
    setTagValue("White", "");
    setTagValue("Black", "");
    setTagValue("Result", "");
  }

  private void copyTags(Hashtable oldTags) {
    // deep copy clone the tags hashtable
    tags = new Hashtable();
    String name;
    String value;
    Enumeration enumeration = oldTags.keys();
    while (enumeration.hasMoreElements()) {
      name = (String)enumeration.nextElement();
      value = (String)oldTags.get(name);
      setTagValue(name, value);
    }
  }

  public void displayTags() {
    String value;
    value = getTagValue("Event");
    chessEditor.eventLabel.setText(value);
    value = getTagValue("White");
    chessEditor.whiteLabel.setText("White: " + value);
    value = getTagValue("Black");
    chessEditor.blackLabel.setText("Black: " + value);
  }

  public boolean standardTag(String name) {
    if (name.equals("Event")) return true;
    if (name.equals("Site")) return true;
    if (name.equals("Date")) return true;
    if (name.equals("Round")) return true;
    if (name.equals("White")) return true;
    if (name.equals("Black")) return true;
    if (name.equals("Result")) return true;
    return false;
  }

  private void outputTag(PrintWriter out, String name, String value) {
    if (!standardTag(name) && value.equals(""))
      return;
    out.println("[" + name + " " + "\"" + value + "\"" + "]");
  }

  public void outputTags(PrintWriter out) {
    // first output standard tags
    outputTag(out, "Event",  getTagValue("Event"));
    outputTag(out, "Site",   getTagValue("Site"));
    outputTag(out, "Date",   getTagValue("Date"));
    outputTag(out, "Round",  getTagValue("Round"));
    outputTag(out, "White",  getTagValue("White"));
    outputTag(out, "Black",  getTagValue("Black"));
    outputTag(out, "Result", getTagValue("Result"));

    // now output non-standard tags
    String name;
    Enumeration enumeration = tags.keys();
    while (enumeration.hasMoreElements()) {
      name = (String)enumeration.nextElement();
      if (!standardTag(name))
        outputTag(out, name, getTagValue(name));
    }
  }

  public void addUserTags(HashSet userTags) {
    Iterator tagIterator = userTags.iterator();
    while (tagIterator.hasNext()) {
      String userTag = (String)tagIterator.next();
      if (getTagValue(userTag).equals(""))
        setTagValue(userTag, "");
    }
  }

  private void badMove(String token) {
    FileIO.getInstance().alert(chessEditor, "bad PGN move " + token);
  }

  private void invalidToken(String token) {
    FileIO.getInstance().alert(chessEditor, "invalid token " + token);
  }

  private void badAttribute(String token) {
    FileIO.getInstance().alert(chessEditor, "bad PGN attribute " + token);
  }
}
