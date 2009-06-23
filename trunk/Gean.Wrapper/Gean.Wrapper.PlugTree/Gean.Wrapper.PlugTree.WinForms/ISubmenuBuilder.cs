// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision: 3702 $</version>
// </file>

using System;
using System.Windows.Forms;

namespace Gean.Wrapper.PlugTree.WinForms
{
	public interface ISubmenuBuilder
	{
		ToolStripItem[] BuildSubmenu(Plug Plug, object owner);
	}
}
