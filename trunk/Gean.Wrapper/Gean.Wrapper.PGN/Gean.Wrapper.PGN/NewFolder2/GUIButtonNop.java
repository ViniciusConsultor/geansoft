import java.awt.event.*;
import javax.swing.*;
import javax.swing.border.*;

class GUIButtonNop extends GUIButton
{
  public GUIButtonNop(ChessEditor chessEditor, GUIBoard board) {
    button = new JButton();
    board.add(button);
    Border emptyBorder = BorderFactory.createEmptyBorder();
    button.setBorder(emptyBorder);
    button.addActionListener(new ButtonListener());
  }
 
  class ButtonListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      // Nop
    }
  } 
}
