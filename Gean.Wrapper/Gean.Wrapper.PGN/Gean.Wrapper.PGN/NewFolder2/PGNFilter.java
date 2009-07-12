class PGNFilter extends javax.swing.filechooser.FileFilter
{
  public boolean accept(java.io.File f) {
    return f.isDirectory() || f.getName().toLowerCase().endsWith(".pgn");
  }
    
  public String getDescription() {
    return "PGN files";
  }

  public static String addSuffix(String string) {
    if (!string.endsWith(".pgn"))
      string += ".pgn";
    return string;
  }
}

