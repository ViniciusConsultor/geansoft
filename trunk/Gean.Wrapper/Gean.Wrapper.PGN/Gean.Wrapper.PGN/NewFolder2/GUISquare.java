import java.awt.event.*;
import javax.swing.*;
import javax.swing.border.*;

class GUISquare extends GUIButton
{
  private int x;

  private int y;

  public String value;

  public GUISquare(
    ChessEditor chessEditor, GUIBoard board, int x, int y, String value) {
    this.chessEditor = chessEditor;
    this.x = x;
    this.y = y;
    this.value = value;
    button = new JButton();
    board.add(button);
    Border emptyBorder = BorderFactory.createEmptyBorder();
    button.setBorder(emptyBorder);
    button.addActionListener(new ButtonListener());
  }

  public String getGUISquare() {
    return value;
  }

  public void setGUISquare(boolean reverse, String value) {
    String squareColor;
    if ((x % 2 + y) % 2 == 0)
      squareColor = "W";
    else
      squareColor = "B";
    this.value = value;
    button.setIcon(
      new ImageIcon(ImageFrame.getInstance().getImage(reverse, squareColor + value)));
  }
 
  class ButtonListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      if (!chessEditor.chess.makeMove(x, y))
        chessEditor.beepUser();
    }
  } 
}
