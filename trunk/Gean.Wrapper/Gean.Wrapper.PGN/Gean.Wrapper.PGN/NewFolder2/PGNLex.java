import java.io.*;

class PGNLex
{
  FileInputStream stream;

  public final static int EOF_CHAR = '&';

  private char ch;

  private int lineCount;

  private int charInLine;
 
  private final static int TOKENSIZE = 49;

  private int tokenIdx;

  private char[] token;

  public final static int TT_NONE            = 0;
  public final static int TT_IDENTIFIER      = 1;
  public final static int TT_NUMBER          = 2;
  public final static int TT_SEPARATOR       = 3;
  public final static int TT_STRING          = 4;
  public final static int TT_OUTCOME         = 5;
  public final static int TT_OPEN_TAG        = 6;
  public final static int TT_CLOSE_TAG       = 7;

  public int tokenType;

  public int nval;

  // move identifier parameters

  public boolean validParameters;
  public char piece;
  public int sourceX;
  public int sourceY;
  public int destX;
  public int destY;

  public String promotionPiece;

  public PGNLex(FileInputStream stream) {
    this.stream = stream;
    token = new char[TOKENSIZE];
    lineCount = 1;
    charInLine = 0;
    ch = ' ';
  }

  private boolean isDigit() {
    return (ch >= '0' && ch <= '9');
  }

  private boolean isAlpha() {
    return
      (ch >= 'a' && ch <= 'z') ||
      (ch >= 'A' && ch <= 'Z');
  }

  private void atoi() {
    int i, start;
    boolean negative;

    if (token[0] == '-') {
      negative = true;
      start = 1;
    } else {
      negative = false;
      start = 0;
    }

    nval = 0;
    for (i = start; i < tokenIdx; i++)
      nval = 10 * nval + (token[i] - '0');

    if (negative)
      nval = nval * -1;
  }

  private void readCh() {
    try {
      ch = (char)stream.read();
      charInLine++;
    }
    catch (IOException e) {
      System.out.println("I/O error");
      e.printStackTrace();
    }
    if (ch == '\n') {
      ch = ' ';
      lineCount++;
      charInLine = 0;
    }
  }

  private int getLineNumber() {
    return lineCount;
  }

  public char lookaheadCh() {
    skipBlanks();
    return ch;
  }

  private boolean eof() {
    return (ch == (char)(-1));
  }

  public boolean eofToken() {
    return (ch == EOF_CHAR);
  }

  private void skipLine() {
    try {
      while (ch != '\n')
        ch = (char)stream.read();
      charInLine = 0;
    }
    catch (IOException e) {
      System.out.println("I/O error");
      e.printStackTrace();
    }
    lineCount++;
    readCh();
  }

  private void skipBlanks() {
    if (ch == EOF_CHAR) // already at end of file
      return;

    while (true) {
      if (eof()) {
        ch = EOF_CHAR;
        return;
      }
      if (ch == ';') // comment
        skipLine();
      else
      if (ch == '{')
        skipComment();
      else
        if (ch == ' ' || ch == 13 || ch == 9 || ch == '\n')
          readCh();
        else
          return;
    }
  }

  private void skipComment() {
    boolean done = false;
    readCh();
    while (!done) {
      if (eof()) {
        ch = EOF_CHAR;
        done = true;
      }
      else {
        if (ch == '}') {
          readCh();
          done = true;
        }
        else
          readCh();
      }
    }
  }

  private void getWinLossDraw() {
    tokenType = TT_IDENTIFIER;
    while (isDigit() || ch == '-' || ch == '/') {
      token[tokenIdx++] = ch;
      readCh();
    }
    String testOutcome = new String(token, 0, tokenIdx);
    if (testOutcome.equals("1-0")) {
      tokenType = TT_OUTCOME;
    }
    else
    if (testOutcome.equals("1/2-1/2")) {
      tokenType = TT_OUTCOME;
    }
    else
    if (testOutcome.equals("1/2")) {
      tokenType = TT_OUTCOME;
      token[3] = '-';
      token[4] = '1';
      token[5] = '/';
      token[6] = '2';
      tokenIdx = 7;
    }
    else
    if (testOutcome.equals("0-1")) {
      tokenType = TT_OUTCOME;
    }
  }

  public String getToken(boolean whiteMove) {
    promotionPiece = null;
    skipBlanks();
    if (ch == EOF_CHAR)
      return "";

    tokenIdx = 0;

    if (ch == '(' ||
        ch == ')' ||
        ch == ',' ||
        ch == '.' ||
        ch == '+') {
      tokenType = TT_SEPARATOR;
      token[tokenIdx++] = ch;
      readCh();
    }

    else
    if (ch == '[') {
      tokenType = TT_OPEN_TAG;
      token[tokenIdx++] = ch;
      readCh();
    }

    else
    if (ch == ']') {
      tokenType = TT_CLOSE_TAG;
      token[tokenIdx++] = ch;
      readCh();
    }

    else
    if (ch == '*') {
      tokenType = TT_OUTCOME;
      token[tokenIdx++] = ch;
      readCh();
    }

    else
    if (isDigit() || ch == '-') {
      tokenType = TT_NUMBER;
      if (ch == '-') {
        token[tokenIdx++] = ch;
        readCh();
      }
      while (isDigit()) {
        token[tokenIdx++] = ch;
        readCh();
	if (ch == '-' || ch == '/')
	  getWinLossDraw();
      }

      if (tokenType == TT_NUMBER) {
        if (tokenIdx == 0)
          token[tokenIdx++] = '0';
        else
        if (tokenIdx == 1 && token[0] == '-')
          token[0] = '0';

        atoi();
      }
    } 

    else // id
    if (isAlpha())
    {
      tokenType = TT_IDENTIFIER;
      token[tokenIdx++] = ch;
      readCh();
      while (!(ch == ' ' || ch == 13 || ch == 9 || ch == '\n')) {
        if (tokenIdx < TOKENSIZE) {
          token[tokenIdx++] = ch;
        }
        readCh();
      }
      getMoveParameters(whiteMove);
    }

    else
    if (ch == '\"') {
      tokenType = TT_STRING;
      readCh();
      while (ch != '\"' && tokenIdx < TOKENSIZE) {
        token[tokenIdx++] = ch;
        readCh();
      }
      readCh();
    }

    else {
      tokenType = TT_NONE;
      token[tokenIdx++] = ch;
      String stringCh = "";
      stringCh += ch;
      System.out.println(getLineNumber() + " bad character " + stringCh);
      readCh();
    }

    return new String(token, 0, tokenIdx);
  }

  private boolean isRank(char ch) {
    return (ch >= '1' && ch <= '8');
  }

  private boolean isFile(char ch) {
    return (ch >= 'a' && ch <= 'h');
  }

  private boolean isPiece(char ch) {
    return
      ch == 'P' || ch == 'R' || ch == 'N' ||
      ch == 'B' || ch == 'Q' || ch == 'K';
  }

  private int getRank(char ch) {
    return 7 - (ch - '1');
  }

  private int getFile(char ch) {
    return ch - 'a';
  }

  private void getMoveParameters(boolean whiteMove) {
    int file[];
    int rank[];
    file = new int[2];
    rank = new int[2];
    piece = ' ';
    sourceX = -1;
    sourceY = -1;
    destX = -1;
    destY = -1;

    if (checkCastle(whiteMove))
      return;

    int fileCount = 0;
    int rankCount = 0;
    for (int i = 0; i < tokenIdx; i++) {
      if (isFile(token[i])) {
        if (fileCount == 2)
          break;
        file[fileCount] = getFile(token[i]);
        fileCount++;
      }
      else
      if (isRank(token[i])) {
        if (rankCount == 2)
          break;
        rank[rankCount] = getRank(token[i]);
        rankCount++;
      }
    }

    if (isFile(token[0]))
      piece = 'P';
    else {
      piece = token[0];
      if (!isPiece(piece))
        piece = ' ';
    }

    // check promotion piece
    promotionPiece = null;
    if (piece == 'P') {
      for (int i = 1; i < tokenIdx; i++) {
        if (isPiece(token[i])) {
          char color;
          if (whiteMove)
            color = 'W';
          else
            color = 'B';
          promotionPiece =
	    new Character(color).toString() +
	    new Character(token[i]).toString(); 
          break;
        }
      }
    }

    if (fileCount == 1)
      destX = file[0];
    else {
      sourceX = file[0];
      destX = file[1];
    }

    if (rankCount == 1)
      destY = rank[0];
    else {
      sourceY = rank[0];
      destY = rank[1];
    }

    if (destX == -1 || destY == -1 || piece == ' ')
      validParameters = false;
    else
      validParameters = true;
  }

  private boolean checkCastle(boolean whiteMove) {
    if (tokenIdx >= 3 &&
        token[0] == 'O' && token[1] == '-' && token[2] == 'O') {
      piece = 'K';
      if (tokenIdx >= 5 && token[3] == '-' && token[4] == 'O') {
        if (whiteMove) {
          sourceX = 4;
          sourceY = 7;
          destX = 2;
          destY = 7;
        }
        else {
          sourceX = 4;
          sourceY = 0;
          destX = 2;
          destY = 0;
        }
        validParameters = true;
        tokenType = TT_IDENTIFIER;
        return true;
      }
      else {
        if (whiteMove) {
          sourceX = 4;
          sourceY = 7;
          destX = 6;
          destY = 7;
        }
        else {
          sourceX = 4;
          sourceY = 0;
          destX = 6;
          destY = 0;
        }
        validParameters = true;
        tokenType = TT_IDENTIFIER;
        return true;
      }
    }
    else
      return false;
  }
}
