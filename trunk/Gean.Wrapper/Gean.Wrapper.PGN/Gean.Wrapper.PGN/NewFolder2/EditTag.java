import java.util.*;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class EditTag extends JDialog implements WindowListener
{
  private Chess chess;

  LinkedList labelList;
  LinkedList textFieldList;

  public EditTag(Chess chess) {
    this.chess = chess;
    this.setModal(true);

    try {
      UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
    }
    catch (InstantiationException e) {}
    catch (ClassNotFoundException e) {}
    catch (UnsupportedLookAndFeelException e) {}
    catch (IllegalAccessException e) {}

    String[] tagName = {
      "Event", "Site", "Date", "Round", "White", "Black", "Result" };

    labelList = new LinkedList();
    textFieldList = new LinkedList();

    this.setTitle("Tags");
    this.addWindowListener(this);
    this.getContentPane().setLayout(new GridBagLayout());
    GridBagConstraints c = new GridBagConstraints();
    c.fill = GridBagConstraints.VERTICAL;
    for (int i = 0; i < tagName.length; i++)
      addRow(c, i, tagName[i], chess.getTagValue(tagName[i]));

    String name;
    Enumeration enumeration = chess.tags.keys();
    int i = tagName.length;
    while (enumeration.hasMoreElements()) {
      name = (String)enumeration.nextElement();
      if (!chess.standardTag(name)) {
        addRow(c, i, name, chess.getTagValue(name));
        i++;
      }
    }
    this.pack();
    this.setLocation(new Point(400, 600));
    this.setVisible(true);

    this.requestFocusInWindow();
  }

  private void addRow(GridBagConstraints c, int i, String name, String value) {
    JLabel label;
    JTextField textField;
    c.gridx = 0;
    c.gridy = i;
    c.anchor = GridBagConstraints.WEST;
    label = new JLabel(name);
    labelList.add(label);
    this.getContentPane().add(label, c);
    c.gridx = 1;
    c.gridy = i;
    c.anchor = GridBagConstraints.EAST;
    textField = new JTextField(value, 35);
    textFieldList.add(textField);
    this.getContentPane().add(textField, c);
  }

  private void saveTags() {
    ListIterator nameIterator = labelList.listIterator();
    ListIterator valueIterator = textFieldList.listIterator();
    while (nameIterator.hasNext()) {
      String name = ((JLabel)nameIterator.next()).getText();
      String value = ((JTextField)valueIterator.next()).getText();
      chess.setTagValue(name, value);
    }
  }

  public void windowOpened(WindowEvent e) {
  }

  public void windowIconified(WindowEvent e) {
  }

  public void windowDeiconified(WindowEvent e) {
  }

  public void windowActivated(WindowEvent e) {
  }

  public void windowDeactivated(WindowEvent e) {
  }

  public void windowClosing(WindowEvent e) {
    saveTags();
  }

  public void windowClosed(WindowEvent e) {
  }
}
