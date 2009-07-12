import java.awt.event.*;
import javax.swing.*;
import javax.swing.border.*;

class GUIButtonForward extends GUIButton
{
  public GUIButtonForward(ChessEditor chessEditor, GUIBoard board) {
    this.chessEditor = chessEditor;
    button = new JButton(" > ");
    board.add(button);
    Border emptyBorder = BorderFactory.createEmptyBorder();
    button.setBorder(emptyBorder);
    button.addActionListener(new ButtonListener());
  }
 
  class ButtonListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      if (!isMoveInProgress()) {
        chessEditor.chess.stepForwardOneMove();
        chessEditor.chess.displayLog();
      }
    }
  } 
}
