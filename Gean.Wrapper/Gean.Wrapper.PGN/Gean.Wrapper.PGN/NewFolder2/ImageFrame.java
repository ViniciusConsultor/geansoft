import java.util.*;
import java.awt.*;
import java.net.*;

class ImageFrame
{
  private Hashtable images;
  private Hashtable reverseImages;

  private static ImageFrame instance;

  public static ImageFrame getInstance() {
    if (instance == null)
      instance = new ImageFrame();
    return instance;
  }

  public ImageFrame() {
  }

  public void resetImages() {
    images = new Hashtable();
    reverseImages = new Hashtable();
  }

  public void setImage(boolean reverse, String fileName, String key) {
    Image image = getImageFromJAR(fileName);
    if (reverse) {
      if (!reverseImages.containsKey(key))
        reverseImages.put(key, image);
    }
    else
    if (!images.containsKey(key)) {
      images.put(key, image);
    }
  }

  public Image getImage(boolean reverse, String key) {
    if (reverse) {
      if (reverseImages.containsKey(key)) {
        return (Image)reverseImages.get(key);
      }
      else
        return null; 
    }
    else
    if (images.containsKey(key)) {
      return (Image)images.get(key);
    }
    else
      return null; 
  }

  private Image getImageFromJAR(String fileName) {
    URL imageURL = getClass().getResource(fileName);
    Toolkit tk = Toolkit.getDefaultToolkit();
    Image img = null;
    try {
      img = tk.createImage(
        (java.awt.image.ImageProducer)imageURL.getContent());
    }
    catch (java.io.IOException e) {
      System.out.println(e);
    }
    return img;
  }
}
