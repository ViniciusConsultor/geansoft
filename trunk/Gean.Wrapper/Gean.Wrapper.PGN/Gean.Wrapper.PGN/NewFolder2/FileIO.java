import java.io.*;
import java.util.*;
import javax.swing.*;

public class FileIO
{
  static public FileIO instance;

  static public FileIO getInstance() {
    if (instance == null)
      instance = new FileIO();
    return instance;
  }

  public FileInputStream openInputStream(String fileName) {
    FileInputStream in = null;
    try {
      in = new FileInputStream(fileName);
    }
    catch (FileNotFoundException e) {
      System.out.println("file not found " + fileName);
    }
    return in;
  }

  public byte readByte(BufferedInputStream stream) {
    int b;
    try {
      b = stream.read();
    }
    catch (IOException e) {
      b = 0;
      System.out.println("I/O error reading byte");
    }
    return (byte)b;
  }

  public void closeInput(FileInputStream in, String fileName) {
    if (in == null) return;
    try {
      in.close();
    }
    catch (IOException e) {
      System.out.println("I/O error closing " + fileName);
    }
  }

  public FileOutputStream openOutputStream(String fileName) {
    FileOutputStream out = null;
    try {
      out = new FileOutputStream(fileName);
    }
    catch (IOException e) {
      System.out.println("error accessing file " + fileName);
    }
    return out;
  } 

  public PrintWriter openPrintWriter(String fileName, FileOutputStream stream) {
    return new PrintWriter(stream);
  } 

  public void writeByte(BufferedOutputStream stream, int value) {
    try {
      stream.write(value);
    }
    catch (IOException e) {
      System.out.println("I/O error writing byte");
    }
  }

  public void flushOutput(BufferedOutputStream stream) {
    try {
      stream.flush();
    }
    catch (IOException e) {
      System.out.println("I/O error flushing output");
    }
  }

  public void closeOutput(FileOutputStream out, String fileName) {
    if (out == null) return;
    try {
      out.close();
    }
    catch (IOException e) {
      System.out.println("I/O error closing " + fileName); // ???? alerts
    }
  }

  public void writeBinary(String fileName, byte[] binary) {
    FileOutputStream outputStream = openOutputStream(fileName);
    BufferedOutputStream outputBufferStream =
      new BufferedOutputStream(outputStream, 100000);
    for (int i = 0; i < binary.length; i++)
      writeByte(outputBufferStream, (int)binary[i]);
    flushOutput(outputBufferStream);
    closeOutput(outputStream, fileName);
  }

  private Hashtable initParameterHash() {
    Hashtable hashTable = new Hashtable(50);
    hashTable.put("loadFileName", "");
    hashTable.put("saveFileName", "");
    hashTable.put("engineFileName", "");
    hashTable.put("userTags", new HashSet());
    hashTable.put("textWidth", new Integer(60));
    hashTable.put("piecesType", "handDrawn");
    return hashTable;
  }

  public Hashtable loadParameters(String fileName) {
    FileInputStream inputStream = openInputStream(fileName);
    if (inputStream == null) {
      closeInput(inputStream, fileName);
      return initParameterHash();
    }
    try {
      ObjectInputStream objectStream = new ObjectInputStream(inputStream);
      try {
        Object object = objectStream.readObject();
        closeInput(inputStream, fileName);
	return (Hashtable)object;
      }
      catch (IOException e) {
        System.out.println("IO exception");
        closeInput(inputStream, fileName);
        return initParameterHash();
      }
      catch (ClassNotFoundException e) {
        System.out.println("Class not found");
        closeInput(inputStream, fileName);
        return initParameterHash();
      }
    }
    catch (IOException e) {
      System.out.println("IO exception");
      closeInput(inputStream, fileName);
      return initParameterHash();
    }
  }
  // ???? frame: use exceptions to return back to Frame code.

  public void saveParameters(String fileName, Hashtable parameter) {
    FileOutputStream outputStream = openOutputStream(fileName);
    try {
      ObjectOutputStream objectStream = new ObjectOutputStream(outputStream);
      objectStream.writeObject(parameter);
      objectStream.flush();
    }
    catch (IOException e) {
      System.out.println("IO exception");
    }
    closeOutput(outputStream, fileName);
  }

  public void alert(JFrame frame, String message){
    JOptionPane.showConfirmDialog(frame,
      message, "Alert", JOptionPane.DEFAULT_OPTION);
  }
}
