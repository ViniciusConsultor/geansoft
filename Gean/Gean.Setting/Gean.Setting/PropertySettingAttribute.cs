//  FILE 
// name       : PropertySettingAttribute.cs
// created    : Jani Giannoudis - 2009.01.14
// language   : c#
// environment: .NET 2.0

using System;

namespace Gean.Setting
{

	
	[AttributeUsage( AttributeTargets.Property, AllowMultiple = false )]
	public class PropertySettingAttribute : Attribute
	{

		
		public string Name { get; set; }

		
		public object DefaultValue { get; set; }

	} // class PropertySettingAttribute

} // namespace Gean.Setting
//  EOF -
