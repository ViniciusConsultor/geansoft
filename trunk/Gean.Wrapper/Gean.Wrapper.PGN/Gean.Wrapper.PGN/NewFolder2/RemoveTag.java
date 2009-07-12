import java.util.*;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class RemoveTag extends JDialog
{
  private JComboBox comboBox;

  private HashSet userTags;
  private Vector userTagsVector;

  public RemoveTag(HashSet userTags) {
    this.userTags = userTags;
    this.setModal(true);

    try {
      UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
    }
    catch (InstantiationException e) {}
    catch (ClassNotFoundException e) {}
    catch (UnsupportedLookAndFeelException e) {}
    catch (IllegalAccessException e) {}

    userTagsVector = new Vector(userTags);
    comboBox = new JComboBox(userTagsVector);
    comboBox.addActionListener(new RemoveTagListener());

    this.setTitle("Remove user defined tag");
    this.getContentPane().setLayout(new FlowLayout());
    this.getContentPane().add(comboBox);
    this.pack();
    this.setLocation(new Point(400, 600));
    this.setVisible(true);

    this.requestFocusInWindow();
  }
   
  class RemoveTagListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      JComboBox cb = (JComboBox)e.getSource();
      String tag = (String)cb.getSelectedItem();
      userTags.remove(tag);
      dispose();
    }
  }
}
