using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Gean.Utility
{
    public static class UtilityDrawing
    {
        public static bool Invert(Bitmap bitmap)
        {
            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmpData.Stride;
            System.IntPtr Scan0 = bmpData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - bitmap.Width * 3;
                int nWidth = bitmap.Width * 3;

                for (int y = 0; y < bitmap.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        p[0] = (byte)(255 - p[0]);
                        ++p;
                    }
                    p += nOffset;
                }
            }

            bitmap.UnlockBits(bmpData);

            return true;
        }

        public static bool GrayScale(Bitmap b)
        {
            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;

                byte red, green, blue;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        p[0] = p[1] = p[2] = (byte)(.299 * red + .587 * green + .114 * blue);

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        public static bool Brightness(Bitmap b, int nBrightness)
        {
            if (nBrightness < -255 || nBrightness > 255)
                return false;

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            int nVal = 0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width * 3;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nVal = (int)(p[0] + nBrightness);

                        if (nVal < 0) nVal = 0;
                        if (nVal > 255) nVal = 255;

                        p[0] = (byte)nVal;

                        ++p;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        /// <summary>
        /// 对指定的图片进行指定数量的对比度调节
        /// </summary>
        /// <param name="b">指定的图片</param>
        /// <param name="nContrast">指定数量</param>
        /// <returns></returns>
        public static bool Contrast(Bitmap b, sbyte nContrast)
        {
            if (nContrast < -100) return false;
            if (nContrast > 100) return false;

            double pixel = 0, contrast = (100.0 + nContrast) / 100.0;

            contrast *= contrast;

            int red, green, blue;

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        pixel = red / 255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[2] = (byte)pixel;

                        pixel = green / 255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[1] = (byte)pixel;

                        pixel = blue / 255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[0] = (byte)pixel;

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        public static bool Gamma(Bitmap b, double red, double green, double blue)
        {
            if (red < .2 || red > 5) return false;
            if (green < .2 || green > 5) return false;
            if (blue < .2 || blue > 5) return false;

            byte[] redGamma = new byte[256];
            byte[] greenGamma = new byte[256];
            byte[] blueGamma = new byte[256];

            for (int i = 0; i < 256; ++i)
            {
                redGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / red)) + 0.5));
                greenGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / green)) + 0.5));
                blueGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / blue)) + 0.5));
            }

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        p[2] = redGamma[p[2]];
                        p[1] = greenGamma[p[1]];
                        p[0] = blueGamma[p[0]];

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        public static bool Color(Bitmap b, int red, int green, int blue)
        {
            if (red < -255 || red > 255) return false;
            if (green < -255 || green > 255) return false;
            if (blue < -255 || blue > 255) return false;

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;
                int nPixel;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        nPixel = p[2] + red;
                        nPixel = Math.Max(nPixel, 0);
                        p[2] = (byte)Math.Min(255, nPixel);

                        nPixel = p[1] + green;
                        nPixel = Math.Max(nPixel, 0);
                        p[1] = (byte)Math.Min(255, nPixel);

                        nPixel = p[0] + blue;
                        nPixel = Math.Max(nPixel, 0);
                        p[0] = (byte)Math.Min(255, nPixel);

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }
    }
}

/* 图像的Gamma变换:一种提高图像亮度的方法，但是是非直线的变换，更加适合人眼的观察方式。
 　
 * 我们在处理RGB 的图像时经常遭遇到一个非常令人讨厌的问题，那就是色彩的准确度问题。
 * RGB 的图像往往会因为搭配的硬件有所不同而出现不一致的结果。所以经常出现的问题就
 * 是：在某一操作平台所制作的图像到了另外一台机器上看就不是那么回事了。例如，一张
 * 在 PC 上制作出的杰作移到了MAC上浏览就变得灰灰白白的甚至有点褪色的样子。
 * 这个问题是因为并非所有的显示器都是一个样的，常常会因为显示器摆放位置周围的以及
 * 亮度的调整值不同而无法一致。但是RGB 各数值与实际屏幕屏幕上所显示的色彩几乎是一
 * 模一样的。例如当我们将红色频设置为 200 时，理论上应该就会比红色频设置为 100 时
 * 看来明亮 2 倍，但实际上并非如此。而实际影响这种结果的因素，我们称他为gamma。
 
  (gamma为参数，r，g,   b为原图某象素，r',g',b'为目的像素)  
      r'=   max(0,min(255,(r/255)^gamma   *   255))  
      g'=   max(0,min(255,(g/255)^gamma   *   255))  
      b'=   max(0,min(255,(b/255)^gamma   *   255))   
  
  利用以上公式可用来矫正显示器亮度的非线性.定性关系可由下面的推导得出:  
   
  看看gamma值的变化对函数关系(r,g,b)->(r',g',b')值的变化的影响:  
   
  当gamma=1时,  
  r'=   max(0,min(255,(r/255)^gamma   *   255))  
      =   max(0,min(255,(r/255)   *   255))  
      =   max(0,min(255,r))  
      =   max(0,r)  
      =   r  
  同样可知   g'=   g,   b'=   b,也即r,g,b成份都不变,因而亮度也不变  
   
  当gamma>1时,  
  r'=   max(0,min(255,(r/255)^gamma   *   255))  
      >   max(0,min(255,(r/255)   *   255))  
      =   max(0,min(255,r))  
      =   max(0,r)  
      =   r  
  同样可知   g'>   g,   b'>   b,也即r,g,b成份都变大,因而亮度也变大  
   
  当gamma<1时,  
  r'=   max(0,min(255,(r/255)^gamma   *   255))  
      <   max(0,min(255,(r/255)   *   255))  
      =   max(0,min(255,r))  
      =   max(0,r)  
      =   r  
  同样可知   g'<   g,   b'<   b,也即r,g,b成份都变小,因而亮度也变小  
   
  可以根据gamma值的变化来定量地画出(r,b,b)->(r',g',b')亮度变化的曲线,详见  
   
  http://www.zzwu.net/free/zzwu/gamma.htm   
 */

/* Demo
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace CSharpFilters
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Drawing.Bitmap m_Bitmap;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem FileLoad;
		private System.Windows.Forms.MenuItem FileSave;
		private System.Windows.Forms.MenuItem FileExit;
		private System.Windows.Forms.MenuItem FilterInvert;
		private System.Windows.Forms.MenuItem FilterGrayScale;
		private System.Windows.Forms.MenuItem FilterBrightness;
		private System.Windows.Forms.MenuItem FilterContrast;
		private System.Windows.Forms.MenuItem FilterGamma;
		private System.Windows.Forms.MenuItem FilterColor;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem Zoom25;
		private double Zoom = 1.0;
		private System.Windows.Forms.MenuItem Zoom50;
		private System.Windows.Forms.MenuItem Zoom100;
		private System.Windows.Forms.MenuItem Zoom150;
		private System.Windows.Forms.MenuItem Zoom200;
		private System.Windows.Forms.MenuItem Zoom300;
		private System.Windows.Forms.MenuItem Zoom500;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();

			m_Bitmap= new Bitmap(2, 2);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.FileLoad = new System.Windows.Forms.MenuItem();
			this.FileSave = new System.Windows.Forms.MenuItem();
			this.FileExit = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.FilterInvert = new System.Windows.Forms.MenuItem();
			this.FilterGrayScale = new System.Windows.Forms.MenuItem();
			this.FilterBrightness = new System.Windows.Forms.MenuItem();
			this.FilterContrast = new System.Windows.Forms.MenuItem();
			this.FilterGamma = new System.Windows.Forms.MenuItem();
			this.FilterColor = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.Zoom25 = new System.Windows.Forms.MenuItem();
			this.Zoom50 = new System.Windows.Forms.MenuItem();
			this.Zoom100 = new System.Windows.Forms.MenuItem();
			this.Zoom150 = new System.Windows.Forms.MenuItem();
			this.Zoom200 = new System.Windows.Forms.MenuItem();
			this.Zoom300 = new System.Windows.Forms.MenuItem();
			this.Zoom500 = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem4,
																					  this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.FileLoad,
																					  this.FileSave,
																					  this.FileExit});
			this.menuItem1.Text = "File";
			// 
			// FileLoad
			// 
			this.FileLoad.Index = 0;
			this.FileLoad.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
			this.FileLoad.Text = "Load";
			this.FileLoad.Click += new System.EventHandler(this.File_Load);
			// 
			// FileSave
			// 
			this.FileSave.Index = 1;
			this.FileSave.Text = "Save";
			this.FileSave.Click += new System.EventHandler(this.File_Save);
			// 
			// FileExit
			// 
			this.FileExit.Index = 2;
			this.FileExit.Text = "Exit";
			this.FileExit.Click += new System.EventHandler(this.File_Exit);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.FilterInvert,
																					  this.FilterGrayScale,
																					  this.FilterBrightness,
																					  this.FilterContrast,
																					  this.FilterGamma,
																					  this.FilterColor});
			this.menuItem4.Text = "Filter";
			// 
			// FilterInvert
			// 
			this.FilterInvert.Index = 0;
			this.FilterInvert.Text = "Invert";
			this.FilterInvert.Click += new System.EventHandler(this.Filter_Invert);
			// 
			// FilterGrayScale
			// 
			this.FilterGrayScale.Index = 1;
			this.FilterGrayScale.Text = "GrayScale";
			this.FilterGrayScale.Click += new System.EventHandler(this.Filter_GrayScale);
			// 
			// FilterBrightness
			// 
			this.FilterBrightness.Index = 2;
			this.FilterBrightness.Text = "Brightness";
			this.FilterBrightness.Click += new System.EventHandler(this.Filter_Brightness);
			// 
			// FilterContrast
			// 
			this.FilterContrast.Index = 3;
			this.FilterContrast.Text = "Contrast";
			this.FilterContrast.Click += new System.EventHandler(this.Filter_Contrast);
			// 
			// FilterGamma
			// 
			this.FilterGamma.Index = 4;
			this.FilterGamma.Text = "Gamma";
			this.FilterGamma.Click += new System.EventHandler(this.Filter_Gamma);
			// 
			// FilterColor
			// 
			this.FilterColor.Index = 5;
			this.FilterColor.Text = "Color";
			this.FilterColor.Click += new System.EventHandler(this.Filter_Color);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.Zoom25,
																					  this.Zoom50,
																					  this.Zoom100,
																					  this.Zoom150,
																					  this.Zoom200,
																					  this.Zoom300,
																					  this.Zoom500});
			this.menuItem2.Text = "Zoom";
			// 
			// Zoom25
			// 
			this.Zoom25.Index = 0;
			this.Zoom25.Text = "25%";
			this.Zoom25.Click += new System.EventHandler(this.OnZoom25);
			// 
			// Zoom50
			// 
			this.Zoom50.Index = 1;
			this.Zoom50.Text = "50%";
			this.Zoom50.Click += new System.EventHandler(this.OnZoom50);
			// 
			// Zoom100
			// 
			this.Zoom100.Index = 2;
			this.Zoom100.Text = "100%";
			this.Zoom100.Click += new System.EventHandler(this.OnZoom100);
			// 
			// Zoom150
			// 
			this.Zoom150.Index = 3;
			this.Zoom150.Text = "150%";
			this.Zoom150.Click += new System.EventHandler(this.OnZoom150);
			// 
			// Zoom200
			// 
			this.Zoom200.Index = 4;
			this.Zoom200.Text = "200%";
			this.Zoom200.Click += new System.EventHandler(this.OnZoom200);
			// 
			// Zoom300
			// 
			this.Zoom300.Index = 5;
			this.Zoom300.Text = "300%";
			this.Zoom300.Click += new System.EventHandler(this.OnZoom300);
			// 
			// Zoom500
			// 
			this.Zoom500.Index = 6;
			this.Zoom500.Text = "500%";
			this.Zoom500.Click += new System.EventHandler(this.OnZoom500);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.ClientSize = new System.Drawing.Size(488, 421);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Graphic Filters For Dummies";
			this.Load += new System.EventHandler(this.Form1_Load);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		protected override void OnPaint (PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			g.DrawImage(m_Bitmap, new Rectangle(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, (int)(m_Bitmap.Width*Zoom), (int)(m_Bitmap.Height * Zoom)));
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		}

		private void File_Load(object sender, System.EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			openFileDialog.InitialDirectory = "c:\\" ;
			openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg";
			openFileDialog.FilterIndex = 2 ;
			openFileDialog.RestoreDirectory = true ;

			if(DialogResult.OK == openFileDialog.ShowDialog())
			{
				m_Bitmap = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false);
				this.AutoScroll = true;
				this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
				this.Invalidate();
			}
		}

		private void File_Save(object sender, System.EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			saveFileDialog.InitialDirectory = "c:\\" ;
			saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg" ;
			saveFileDialog.FilterIndex = 1 ;
			saveFileDialog.RestoreDirectory = true ;

			if(DialogResult.OK == saveFileDialog.ShowDialog())
			{
				m_Bitmap.Save(saveFileDialog.FileName);
			}
		}

		private void File_Exit(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void Filter_Invert(object sender, System.EventArgs e)
		{
			if(BitmapFilter.Invert(m_Bitmap))
				this.Invalidate();
		}

		private void Filter_GrayScale(object sender, System.EventArgs e)
		{
			if(BitmapFilter.GrayScale(m_Bitmap))
				this.Invalidate();
		}

		private void Filter_Brightness(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				if(BitmapFilter.Brightness(m_Bitmap, dlg.nValue))
					this.Invalidate();
			}
		}

		private void Filter_Contrast(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				if(BitmapFilter.Contrast(m_Bitmap, (sbyte)dlg.nValue))
					this.Invalidate();
			}
		
		}

		private void Filter_Gamma(object sender, System.EventArgs e)
		{
			GammaInput dlg = new GammaInput();
			dlg.red = dlg.green = dlg.blue = 1;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				if(BitmapFilter.Gamma(m_Bitmap, dlg.red, dlg.green, dlg.blue))
					this.Invalidate();
			}
		}

		private void Filter_Color(object sender, System.EventArgs e)
		{
			ColorInput dlg = new ColorInput();
			dlg.red = dlg.green = dlg.blue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				if(BitmapFilter.Color(m_Bitmap, dlg.red, dlg.green, dlg.blue))
					this.Invalidate();
			}
		
		}

		private void OnZoom25(object sender, System.EventArgs e)
		{
			Zoom = .25;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom50(object sender, System.EventArgs e)
		{
			Zoom = .5;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom100(object sender, System.EventArgs e)
		{
			Zoom = 1.0;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom150(object sender, System.EventArgs e)
		{
			Zoom = 1.5;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom200(object sender, System.EventArgs e)
		{
			Zoom = 2.0;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom300(object sender, System.EventArgs e)
		{
			Zoom = 3.0;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom500(object sender, System.EventArgs e)
		{
			Zoom = 5;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}
	}
}
*/