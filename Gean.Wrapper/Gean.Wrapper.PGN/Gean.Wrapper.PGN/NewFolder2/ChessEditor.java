import java.io.*;
import java.util.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.event.*;

public class ChessEditor extends JFrame implements WindowListener
{
  public Chess chess;
  public LinkedList gameList;
  private int gameNumber = 0;

  private Chess pasteGame = null;

  public EngineProcess engineProcess = null;

  public HashSet userTags;

  private String path;

  private int piecesType = 0;

  public ImageFrame pieceImage;

  private Integer textWidth;

  private ChessEditor thisFrame;
    private JMenuBar menuBar;
      private JMenu menuFile;
        private JMenuItem menuItemLoad;
        private JMenuItem menuItemSaveAs;
        private JMenuItem menuItemSave;
        private JMenu menuItemPieces;
          private ButtonGroup piecesGroup;
          private JRadioButtonMenuItem menuItemHandDrawn;
          private JRadioButtonMenuItem menuItemModelled;
        private JMenuItem menuItemLocateEngine;
        private JMenu menuItemEngine;
          private ButtonGroup engineGroup;
          public JRadioButtonMenuItem menuItemEngineOff;
          private JRadioButtonMenuItem menuItemEngineOn;
        private JMenuItem menuItemQuit;
      private JMenu menuEdit;
        private JMenuItem menuItemTags;
        private JMenuItem menuItemDefineTag;
        private JMenuItem menuItemRemoveTag;
        private JMenuItem menuItemTextWidth;
      private JMenu menuGame;
        private JMenuItem menuItemNew;
        private JMenuItem menuItemRemove;
        private JMenuItem menuItemPaste;
        private JMenuItem menuItemNextGame;
        private JMenuItem menuItemPrevGame;
        private JMenuItem menuItemWhiteWin;
        private JMenuItem menuItemDraw;
        private JMenuItem menuItemBlackWin;
        private JMenuItem menuItemIncomplete;
      private JMenu menuHelp;
        private JMenuItem menuItemAbout;
    private Box boardAndSelectionList;
      private Box boardAndText;
        private JPanel boardPanel;
          public GUIBoard board;
          public JLabel eventLabel;
          public JLabel whiteLabel;
          public JLabel blackLabel;
        private JScrollPane textScroll;
        public JTextArea textArea;
      private JScrollPane scroller;

  private DefaultListModel listModel;
  private JList selectionList;

  private FileIO file;

  private String saveFileName;

  public Hashtable parameter;

  public ChessEditor() {
    thisFrame = this;

    try {
      UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
    }
    catch (InstantiationException e) {}
    catch (ClassNotFoundException e) {}
    catch (UnsupportedLookAndFeelException e) {}
    catch (IllegalAccessException e) {}

    file = FileIO.getInstance();

    parameter = file.loadParameters("chesseditor.ini");

    userTags = (HashSet)parameter.get("userTags");

    textWidth = (Integer)parameter.get("textWidth");

    if (((String)parameter.get("piecesType")).equals("handDrawn")) {
      path = "Pieces/HandDrawn/";
      piecesType = 0;
    }
    else {
      path = "Pieces/Modelled/";
      piecesType = 1;
    }
    initializePieces();

    menuBar = new JMenuBar();
    menuFile = new JMenu("File");
    menuBar.add(menuFile);
      menuItemLoad = new JMenuItem("Load Game(s)");
      menuItemLoad.addActionListener(new LoadMenuListener());
      menuFile.add(menuItemLoad);
      menuItemSaveAs = new JMenuItem("Save Game(s) As");
      menuItemSaveAs.addActionListener(new SaveAsMenuListener());
      menuFile.add(menuItemSaveAs);
      menuItemSave = new JMenuItem("Save Game(s)");
      menuItemSave.addActionListener(new SaveMenuListener());
      menuFile.add(menuItemSave);
      menuItemPieces = new JMenu("Choose Pieces");
      menuFile.add(menuItemPieces);
        piecesGroup = new ButtonGroup();
        menuItemHandDrawn = new JRadioButtonMenuItem("Hand Drawn");
        if (piecesType == 0)
          menuItemHandDrawn.setSelected(true);
        menuItemHandDrawn.addActionListener(new HandDrawnMenuListener());
        piecesGroup.add(menuItemHandDrawn);
        menuItemPieces.add(menuItemHandDrawn);
        menuItemModelled = new JRadioButtonMenuItem("Modelled");
        if (piecesType == 1)
          menuItemModelled.setSelected(true);
        piecesGroup.add(menuItemModelled);
        menuItemModelled.addActionListener(new ModelledMenuListener());
        menuItemPieces.add(menuItemModelled);
      menuItemLocateEngine = new JMenuItem("Locate Chess Engine");
      menuItemLocateEngine.addActionListener(new LocateEngineListener());
      menuFile.add(menuItemLocateEngine);
      menuItemEngine = new JMenu("Chess Engine");
      menuFile.add(menuItemEngine);
        engineGroup = new ButtonGroup();
        menuItemEngineOff = new JRadioButtonMenuItem("Off");
        menuItemEngineOff.setSelected(true);
        menuItemEngineOff.addActionListener(new EngineOffListener());
        engineGroup.add(menuItemEngineOff);
        menuItemEngine.add(menuItemEngineOff);
        menuItemEngineOn = new JRadioButtonMenuItem("On");
        menuItemEngineOn.addActionListener(new EngineOnListener());
        engineGroup.add(menuItemEngineOn);
        menuItemEngine.add(menuItemEngineOn);
      menuItemQuit = new JMenuItem("Quit");
      menuItemQuit.addActionListener(new QuitMenuListener());
      menuFile.add(menuItemQuit);
    menuEdit = new JMenu("Edit");
    menuBar.add(menuEdit);
      menuItemTags = new JMenuItem("Tags");
      menuItemTags.addActionListener(new TagsMenuListener());
      menuEdit.add(menuItemTags);
      menuItemDefineTag = new JMenuItem("Define Tag");
      menuItemDefineTag.addActionListener(new DefineTagMenuListener());
      menuEdit.add(menuItemDefineTag);
      menuItemRemoveTag = new JMenuItem("Remove Tag");
      menuItemRemoveTag.addActionListener(new RemoveTagMenuListener());
      menuEdit.add(menuItemRemoveTag);
      menuItemTextWidth = new JMenuItem("Text Width");
      menuItemTextWidth.addActionListener(new TextWidthMenuListener());
      menuEdit.add(menuItemTextWidth);
    menuGame = new JMenu("Game");
    menuBar.add(menuGame);
      menuItemNew = new JMenuItem("New game");
      menuItemNew.addActionListener(new NewMenuListener());
      menuGame.add(menuItemNew);
      menuItemRemove = new JMenuItem("Remove game");
      menuItemRemove.addActionListener(new RemoveMenuListener());
      menuGame.add(menuItemRemove);
      menuItemPaste = new JMenuItem("Paste game");
      menuItemPaste.addActionListener(new PasteMenuListener());
      menuGame.add(menuItemPaste);
      menuItemNextGame = new JMenuItem("Next Game");
      menuItemNextGame.addActionListener(new NextGameListener());
      menuGame.add(menuItemNextGame);
      menuItemPrevGame = new JMenuItem("Previous Game");
      menuItemPrevGame.addActionListener(new PrevGameListener());
      menuGame.add(menuItemPrevGame);
      menuItemWhiteWin = new JMenuItem("White Win 1-0");
      menuItemWhiteWin.addActionListener(new WhiteWinMenuListener());
      menuGame.add(menuItemWhiteWin);
      menuItemDraw = new JMenuItem("Draw 1/2-1/2");
      menuItemDraw.addActionListener(new DrawMenuListener());
      menuGame.add(menuItemDraw);
      menuItemBlackWin = new JMenuItem("Black Win 0-1");
      menuItemBlackWin.addActionListener(new BlackWinMenuListener());
      menuGame.add(menuItemBlackWin);
      menuItemIncomplete = new JMenuItem("Incomplete Game *");
      menuItemIncomplete.addActionListener(new IncompleteMenuListener());
      menuGame.add(menuItemIncomplete);
    menuHelp = new JMenu("Help");
    menuBar.add(menuHelp);
      menuItemAbout = new JMenuItem("About");
      menuItemAbout.addActionListener(new AboutMenuListener());
      menuHelp.add(menuItemAbout);
    this.setJMenuBar(menuBar);

    textArea = new JTextArea("", 15, 45);
    textArea.setEditable(false);
//  textArea.setFont(new Font("Arial", Font.PLAIN, 10));
    textArea.setLineWrap(true);
    textArea.setWrapStyleWord(true);
    textScroll = new JScrollPane(textArea);

    eventLabel = new JLabel("xxx");
    eventLabel.setAlignmentX(Component.LEFT_ALIGNMENT);
    whiteLabel = new JLabel("xxx");
    whiteLabel.setAlignmentX(Component.LEFT_ALIGNMENT);
    blackLabel = new JLabel("xxx");
    blackLabel.setAlignmentX(Component.LEFT_ALIGNMENT);

    board = new GUIBoard(this);

    boardPanel = new JPanel();
    boardPanel.setLayout(new GridBagLayout());
    GridBagConstraints c = new GridBagConstraints();
    c.fill = GridBagConstraints.VERTICAL;
    
    c.gridx = 0;
    c.gridy = 0;
    boardPanel.add(board, c);
    c.gridx = 0;
    c.gridy = 1;
    c.anchor = GridBagConstraints.WEST;
    boardPanel.add(eventLabel, c);
    c.gridx = 0;
    c.gridy = 2;
    c.anchor = GridBagConstraints.WEST;
    boardPanel.add(whiteLabel, c);
    c.gridx = 0;
    c.gridy = 3;
    c.anchor = GridBagConstraints.WEST;
    boardPanel.add(blackLabel, c);

    boardAndText = Box.createHorizontalBox();
    boardAndText.add(boardPanel);
    boardAndText.add(Box.createHorizontalStrut(5));
    boardAndText.add(textScroll);

    listModel = new DefaultListModel();

    selectionList = new JList(listModel);
    selectionList.setSelectionMode(
      ListSelectionModel.SINGLE_INTERVAL_SELECTION);
    selectionList.addListSelectionListener(new ListListener());
    scroller = new JScrollPane(selectionList);

    boardAndSelectionList = Box.createVerticalBox();
    boardAndSelectionList.add(boardAndText);
    boardAndSelectionList.add(scroller);

    this.setTitle("Chess Editor");
    this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    this.addWindowListener(this);
    this.getContentPane().setLayout(new BorderLayout());
    this.getContentPane().add(boardAndSelectionList);
    this.pack();
    this.setSize(600, 491);
    this.setResizable(false);
    this.setLocation(new Point(100, 100));
    this.setVisible(true);

    gameList = new LinkedList();
    chess = new Chess(this);
    listModel.addElement(getGameString(gameNumber));
    setSelectionListIndexVisible();
    gameList.add(chess); // first game
  }

  private void initializePieces() {
    pieceImage = ImageFrame.getInstance();
    pieceImage.resetImages();

    pieceImage.setImage(false, path + "BS.gif", "BS");
    pieceImage.setImage(false, path + "BSWP.gif", "BWP");
    pieceImage.setImage(false, path + "BSBP.gif", "BBP");
    pieceImage.setImage(false, path + "BSWR.gif", "BWR");
    pieceImage.setImage(false, path + "BSBR.gif", "BBR");
    pieceImage.setImage(false, path + "BSWN.gif", "BWN");
    pieceImage.setImage(false, path + "BSBN.gif", "BBN");
    pieceImage.setImage(false, path + "BSWB.gif", "BWB");
    pieceImage.setImage(false, path + "BSBB.gif", "BBB");
    pieceImage.setImage(false, path + "BSWQ.gif", "BWQ");
    pieceImage.setImage(false, path + "BSBQ.gif", "BBQ");
    pieceImage.setImage(false, path + "BSWK.gif", "BWK");
    pieceImage.setImage(false, path + "BSBK.gif", "BBK");
    pieceImage.setImage(false, path + "WS.gif", "WS");
    pieceImage.setImage(false, path + "WSWP.gif", "WWP");
    pieceImage.setImage(false, path + "WSBP.gif", "WBP");
    pieceImage.setImage(false, path + "WSWR.gif", "WWR");
    pieceImage.setImage(false, path + "WSBR.gif", "WBR");
    pieceImage.setImage(false, path + "WSWN.gif", "WWN");
    pieceImage.setImage(false, path + "WSBN.gif", "WBN");
    pieceImage.setImage(false, path + "WSWB.gif", "WWB");
    pieceImage.setImage(false, path + "WSBB.gif", "WBB");
    pieceImage.setImage(false, path + "WSWQ.gif", "WWQ");
    pieceImage.setImage(false, path + "WSBQ.gif", "WBQ");
    pieceImage.setImage(false, path + "WSWK.gif", "WWK");
    pieceImage.setImage(false, path + "WSBK.gif", "WBK");

    // reverse piece images

    pieceImage.setImage(true, path + "RBS.gif", "BS");
    pieceImage.setImage(true, path + "RBSWP.gif", "BWP");
    pieceImage.setImage(true, path + "RBSBP.gif", "BBP");
    pieceImage.setImage(true, path + "RBSWR.gif", "BWR");
    pieceImage.setImage(true, path + "RBSBR.gif", "BBR");
    pieceImage.setImage(true, path + "RBSWN.gif", "BWN");
    pieceImage.setImage(true, path + "RBSBN.gif", "BBN");
    pieceImage.setImage(true, path + "RBSWB.gif", "BWB");
    pieceImage.setImage(true, path + "RBSBB.gif", "BBB");
    pieceImage.setImage(true, path + "RBSWQ.gif", "BWQ");
    pieceImage.setImage(true, path + "RBSBQ.gif", "BBQ");
    pieceImage.setImage(true, path + "RBSWK.gif", "BWK");
    pieceImage.setImage(true, path + "RBSBK.gif", "BBK");
    pieceImage.setImage(true, path + "RWS.gif", "WS");
    pieceImage.setImage(true, path + "RWSWP.gif", "WWP");
    pieceImage.setImage(true, path + "RWSBP.gif", "WBP");
    pieceImage.setImage(true, path + "RWSWR.gif", "WWR");
    pieceImage.setImage(true, path + "RWSBR.gif", "WBR");
    pieceImage.setImage(true, path + "RWSWN.gif", "WWN");
    pieceImage.setImage(true, path + "RWSBN.gif", "WBN");
    pieceImage.setImage(true, path + "RWSWB.gif", "WWB");
    pieceImage.setImage(true, path + "RWSBB.gif", "WBB");
    pieceImage.setImage(true, path + "RWSWQ.gif", "WWQ");
    pieceImage.setImage(true, path + "RWSBQ.gif", "WBQ");
    pieceImage.setImage(true, path + "RWSWK.gif", "WWK");
    pieceImage.setImage(true, path + "RWSBK.gif", "WBK");
  }

  private void setSelectionListIndexVisible() {
    selectionList.setSelectedIndex(gameNumber);
    selectionList.ensureIndexIsVisible(gameNumber);
  }

  private void findGame(int n) {
    if (n < gameList.size() && n >= 0) {
      gameNumber = n;
      textArea.setText("");
      chess = (Chess)gameList.get(gameNumber);
      chess.stepToStart();
      board.initializeBoardPieces();
      chess.engineMove("new");
      chess.stepToEnd();
      chess.displayLog();
      chess.addUserTags(userTags);
      chess.displayTags();
      setSelectionListIndexVisible();
    }
  }

  private String getGameString(int gameNumber) {
    String round = chess.getTagValue("Round");
    if (!round.equals(""))
      round = "round" + round + " ";
    return
      (gameNumber + 1) + ". " +
      chess.getTagValue("Event") + " " +
      chess.getTagValue("Date") + " " +
      round +
      chess.getTagValue("White") + " vs. " +
      chess.getTagValue("Black") + " " +
      chess.getTagValue("Result");
  }
   
  class LoadMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      if (fileChooserParameter(
	"loadFileName", "Load Games(s)", new PGNFilter())) {
        file.saveParameters("chesseditor.ini", parameter);
	  saveFileName = null;
        FileInputStream stream =
          file.openInputStream((String)parameter.get("loadFileName"));
        listModel.clear();
        PGNLex pgnLex = new PGNLex(stream);
        boolean returnCode;
        gameList = new LinkedList();
	int i = 0;
        while (!pgnLex.eofToken()) {
          returnCode = chess.playPGNGame(pgnLex);
          gameList.add(chess);
          if (!returnCode)
            break; // report game # ????
          listModel.addElement(getGameString(i));
	  if (pgnLex.lookaheadCh() == PGNLex.EOF_CHAR) // eof
	    break;
	  chess = new Chess(thisFrame);
          i++;
        }
        gameNumber = gameList.size() - 1;
        findGame(gameNumber);
	setSelectionListIndexVisible();
        file.closeInput(stream, (String)parameter.get("loadFileName"));
        chess.stepToStart();
        chess.stepToEnd();
      }
    }
  } 
   
  class NextGameListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      gameNumber++;
      if (gameNumber >= gameList.size())
        gameNumber = gameList.size() - 1;
      findGame(gameNumber);
    }
  } 
   
  class PrevGameListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      gameNumber--;
      if (gameNumber < 0)
        gameNumber = 0;
      findGame(gameNumber);
    }
  } 

  private int textGetEndOfLine(String text, int start) {
    int end = start + textWidth.intValue() - 1;
    if (end >= text.length())
      end = text.length();
    else {
      while (text.charAt(end) != ' ')
        end--;
      end++;
    }
    return end;
  }

  private String checkGameResult(String text) {
    if (!(text.endsWith("1-0 ") || text.endsWith("0-1 ") ||
	text.endsWith("1/2-1/2 ") || text.endsWith("* "))) {
      String result = chess.getTagValue("Result");
      if (result.equals("1-0") || result.equals("0-1") ||
	  result.equals("1/2-1/2") || result.equals("*"))
        text = text + " " + result + " ";
      else
        text = text + " * ";
    }
    return text;
  }

  private void saveGames(String fileName) {
    int saveGameNumber = gameNumber;
    FileOutputStream handle = file.openOutputStream(fileName);
    PrintWriter out = file.openPrintWriter(fileName, handle);
    for (int i = 0; i < gameList.size(); i++) {
      findGame(i);
      chess.outputTags(out);
      out.println();
      String text = new String(textArea.getText());
      text = checkGameResult(text);
      int j = 0;
      int endOfLine;
      while (j < text.length()) {
        endOfLine = textGetEndOfLine(text, j);
        while (j < endOfLine) {
          out.print(text.charAt(j));
          j++;
        }
        out.println();
      }
      out.println();
    }
    out.close();
    file.saveParameters("chesseditor.ini", parameter);
    gameNumber = saveGameNumber;
    findGame(gameNumber);
  }

  private void saveAs() {
    if (fileChooserParameter("saveFileName", "Save Game(s)", new PGNFilter())) {
      saveFileName = (String)parameter.get("saveFileName");
      saveFileName = PGNFilter.addSuffix(saveFileName);
      saveGames(saveFileName);
    }
  }

  class SaveAsMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      saveAs();
    }
  }

  class SaveMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      if (saveFileName != null)
        saveGames(saveFileName);
      else
        saveAs();
    }
  }

  private void displayTagsAndUpdateListModel() {
    chess.displayTags();
    if (gameNumber >= 0 && gameNumber < gameList.size())
      listModel.set(gameNumber, getGameString(gameNumber));
  }
  
  class TagsMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      new EditTag(chess);
      chess.addUserTags(userTags);
      displayTagsAndUpdateListModel();
    }
  }

  class DefineTagMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      String tag =
        JOptionPane.showInputDialog(thisFrame,
          "Add user defined tag", "",
          JOptionPane.PLAIN_MESSAGE);
      if (tag != null) // result of a cancel
        if (!tag.equals("")) {
          userTags.add(tag);
          file.saveParameters("chesseditor.ini", parameter);
        }
    }
  }

  class RemoveTagMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      new RemoveTag(userTags);
      file.saveParameters("chesseditor.ini", parameter);
    }
  }

  class TextWidthMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      try {
        Integer getTextWidth = new Integer(
          (String)JOptionPane.showInputDialog(thisFrame,
            "Enter Text Width", "Text Width",
            JOptionPane.PLAIN_MESSAGE,
	    null,
	    null,
	    String.valueOf(textWidth.intValue())));
        if (getTextWidth != null) { // if null its a cancel
          if (getTextWidth.intValue() >= 20 && getTextWidth.intValue() <= 60) {
            textWidth = getTextWidth;
            parameter.put("textWidth", textWidth);
	  }
        }
        file.saveParameters("chesseditor.ini", parameter);
      }
      catch (NumberFormatException exception) {
      }
    }
  }
  
  class NewMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      gameNumber = gameList.size();
      listModel.addElement(getGameString(gameNumber));
      setSelectionListIndexVisible();
      chess = new Chess(thisFrame, chess.tags);
      gameList.add(chess);
    }
  }

  private void refreshListModel() {
    listModel.clear();
    for (int i = 0; i < gameList.size(); i++) {
      chess = (Chess)gameList.get(i);
      listModel.addElement(getGameString(i));
    }
  }
  
  class RemoveMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      Object[] options = { "Yes", "No" };
      int n = JOptionPane.showOptionDialog(
        thisFrame,
        "Do you really want to remove the current game?",
        "Remove Current Game",
        JOptionPane.YES_NO_OPTION,
        JOptionPane.QUESTION_MESSAGE,
        null, // don't use a custom Icon
        options, // the titles of buttons
        options[0]); // default button title
      if (n == 0) { // yes
        int saveGameNumber = gameNumber;
	pasteGame = (Chess)gameList.get(gameNumber);
	gameList.remove(gameNumber);
        refreshListModel();
        gameNumber = saveGameNumber - 1;
	if (gameNumber >= gameList.size())
          gameNumber = gameList.size() - 1;
	findGame(gameNumber);
      }
    }
  }
  
  class PasteMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      if (pasteGame != null) {
	gameNumber++;
        int saveGameNumber = gameNumber;
        gameList.add(gameNumber, pasteGame);
	pasteGame = null;
        refreshListModel();
        gameNumber = saveGameNumber;
	findGame(gameNumber);
      }
    }
  }
  
  class HandDrawnMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      path = "Pieces/HandDrawn/";
      initializePieces();
      board.refreshBoard();
      piecesType = 0;
      parameter.put("piecesType", "handDrawn");
      file.saveParameters("chesseditor.ini", parameter);
    }
  }
  
  class ModelledMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      path = "Pieces/Modelled/";
      initializePieces();
      board.refreshBoard();
      piecesType = 1;
      parameter.put("piecesType", "modelled");
      file.saveParameters("chesseditor.ini", parameter);
    }
  }

  private void locateEngine() {
    if (fileChooserParameter("engineFileName", "Locate Engine", null)) {
      String fileName = (String)parameter.get("engineFileName");
      if (!fileName.endsWith(".exe")) { // ???? not OS agnostic
        FileIO.getInstance().alert(this, fileName + " not executable");
	parameter.put("engineFileName", "");
      }
      file.saveParameters("chesseditor.ini", parameter);
    }
  }
  
  class LocateEngineListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      locateEngine();
    }
  }

  private void stopEngine() {
    if (engineProcess != null) {
      engineProcess.stopEngine();
      engineProcess = null;
    }
  }
  
  class EngineOffListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      stopEngine();
    }
  }
  
  class EngineOnListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      if (engineProcess == null) {
	if (((String)parameter.get("engineFileName")).equals(""))
	  locateEngine();
	if (((String)parameter.get("engineFileName")).equals(""))
	  return;
        engineProcess =
	  new EngineProcess(thisFrame, (String)parameter.get("engineFileName"));
        engineProcess.runEngine();
        engineProcess.talkToEngine("log off");
        engineProcess.talkToEngine("noise 1000");
        engineProcess.talkToEngine("xboard");
        engineProcess.talkToEngine("force");
        engineProcess.talkToEngine("post");
        engineProcess.talkToEngine("analyze");
	chess.syncEngine(chess.startMove);
      }
    }
  }

  class QuitMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      System.exit(0);
    }
  }

  class WhiteWinMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      chess.gameResult("1-0");
      displayTagsAndUpdateListModel();
    }
  }

  class DrawMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      chess.gameResult("1/2-1/2");
      displayTagsAndUpdateListModel();
    }
  }

  class BlackWinMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      chess.gameResult("0-1");
      displayTagsAndUpdateListModel();
    }
  }

  class IncompleteMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      chess.gameResult("*");
      displayTagsAndUpdateListModel();
    }
  }

  class ListListener implements ListSelectionListener {
    public void valueChanged(ListSelectionEvent e) {
      gameNumber = selectionList.getSelectedIndex();
      findGame(gameNumber);
    }
  }

  class AboutMenuListener implements ActionListener {
    public void actionPerformed(ActionEvent e) {
      JOptionPane.showConfirmDialog(thisFrame,
	"Version 1.0\n" +
	"Author Rick Miskowski\n" +
	"daru11@cox.net",
	"About",
        JOptionPane.DEFAULT_OPTION, JOptionPane.PLAIN_MESSAGE);
    }
  }

  private String fileChooser(
    String fileName, String title, javax.swing.filechooser.FileFilter filter) {
    JFileChooser fileChooser = new JFileChooser(fileName);
    fileChooser.setFileFilter(filter);
    fileChooser.setDialogTitle(title);
    int returnVal = fileChooser.showOpenDialog(this);
    if (returnVal == JFileChooser.APPROVE_OPTION) {
      return fileChooser.getSelectedFile().getPath();
    }
    return "";
  }

  private boolean fileChooserParameter(
    String name, String title, javax.swing.filechooser.FileFilter filter) {
    String fileName = fileChooser((String)parameter.get(name), title, filter);
    if (!fileName.equals("")) { // found a file
      parameter.put(name, fileName);
      return true;
    }
    return false;
  }

  public void beepUser() {
    Toolkit.getDefaultToolkit().beep();
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
    stopEngine();
  }

  public void windowClosed(WindowEvent e) {
  }
}
