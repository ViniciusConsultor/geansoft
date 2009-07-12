import java.awt.event.*;
import javax.swing.*;
import javax.swing.border.*;

class GUIButtonEnd extends GUIButton
{
  public GUIButtonEnd(ChessEditor chessEditor, GUIBoard board) {
    this.chessEditor = chessEditor;
    button = new JButton(" >>|");
    board.add(button);
    Border emptyBorder = BorderFactory.createEmptyBorder();
    button.setBorder(emptyBorder);
    button.addActionListener(new ButtonListener());
  }
 
  class ButtonListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      if (!isMoveInProgress()) {
        chessEditor.chess.stepToEnd();
        chessEditor.chess.displayLog();
      }
    }
  } 
}
