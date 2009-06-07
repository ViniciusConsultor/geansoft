using System;

namespace Gean
{
	/// <summary>
	/// The IPadContent interface is the basic interface to all "tool" windows
	/// in SharpDevelop.
	/// </summary>
	public interface IPadContent : IDisposable
	{
		/// <summary>
		/// This is the UI element for the view.
		/// You can use both Windows.Forms and WPF controls.
		/// </summary>
		object Content {
			get;
		}
	}
}
