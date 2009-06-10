using System;
using Gean.Framework;

namespace Gean.Wrapper.PlugTree
{
	public interface IMenuCommand : ICommand
	{
		bool IsEnabled {
			get;
			set;
		}
	}
}
