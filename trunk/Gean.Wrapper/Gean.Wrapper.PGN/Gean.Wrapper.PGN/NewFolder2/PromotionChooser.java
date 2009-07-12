import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.border.*;

public class PromotionChooser
{
  public ImageFrame pieceImage;

  private JDialog thisDialog;
    private JButton rookButton;
    private JButton knightButton;
    private JButton bishopButton;
    private JButton queenButton;

  private Promotion promotion;
  private String color;
  
  public PromotionChooser(JFrame frame, String color, Promotion promotion) {
    this.promotion = promotion;
    this.color = color;
    thisDialog = new JDialog(frame, "Choose Piece",  true);

    promotion.piece = color + "Q"; // default

    Border emptyBorder = BorderFactory.createEmptyBorder();

    thisDialog.getContentPane().setLayout(new GridLayout(0, 4));
    rookButton = new JButton();
    rookButton.setIcon(
      new ImageIcon(
        ImageFrame.getInstance().getImage(false, "W" + color + "R")));
    rookButton.addActionListener(new RookListener());
    thisDialog.getContentPane().add(rookButton);
    rookButton.setBorder(emptyBorder);
    knightButton = new JButton();
    knightButton.setIcon(
      new ImageIcon(
        ImageFrame.getInstance().getImage(false, "W" + color + "N")));
    knightButton.addActionListener(new KnightListener());
    thisDialog.getContentPane().add(knightButton);
    knightButton.setBorder(emptyBorder);
    bishopButton = new JButton();
    bishopButton.setIcon(
      new ImageIcon(
        ImageFrame.getInstance().getImage(false, "W" + color + "B")));
    bishopButton.addActionListener(new BishopListener());
    thisDialog.getContentPane().add(bishopButton);
    bishopButton.setBorder(emptyBorder);
    queenButton = new JButton();
    queenButton.setIcon(
      new ImageIcon(
        ImageFrame.getInstance().getImage(false, "W" + color + "Q")));
    queenButton.addActionListener(new QueenListener());
    thisDialog.getContentPane().add(queenButton);
    queenButton.setBorder(emptyBorder);
    thisDialog.pack();
    thisDialog.setSize(135, 65);
    thisDialog.setResizable(false);
    thisDialog.setLocation(new Point(300, 150));
    thisDialog.setVisible(true);
  }
   
  class RookListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      promotion.piece = color + "R";
      thisDialog.dispose();
    }
  } 
   
  class KnightListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      promotion.piece = color + "N";
      thisDialog.dispose();
    }
  } 
   
  class BishopListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      promotion.piece = color + "B";
      thisDialog.dispose();
    }
  } 
   
  class QueenListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      promotion.piece = color + "Q";
      thisDialog.dispose();
    }
  } 
}
