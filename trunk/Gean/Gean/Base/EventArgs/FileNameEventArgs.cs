using System;

namespace Gean
{
	public delegate void FileNameEventHandler(object sender, FileNameEventArgs e);

    [Serializable]
	public class FileNameEventArgs : System.EventArgs
	{
        public string FileName { get; private set; }
		public FileNameEventArgs(string fileName)
		{
            this.FileName = fileName;
		}
	}
}
