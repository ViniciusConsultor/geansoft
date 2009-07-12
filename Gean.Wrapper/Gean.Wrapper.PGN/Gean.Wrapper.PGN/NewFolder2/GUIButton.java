import javax.swing.*;

abstract class GUIButton
{
  public ChessEditor chessEditor;

  public JButton button;

  public GUIButton() {
  }

  public boolean isMoveInProgress() {
    boolean moveInProgress = chessEditor.chess.moveInProgress;
    if (moveInProgress)
      chessEditor.beepUser();
    return moveInProgress;
  }
}
