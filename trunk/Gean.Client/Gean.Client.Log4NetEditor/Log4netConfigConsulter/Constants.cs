using System;

namespace Gean.Client.Log4NetEditor
{
	/// <summary>
	/// Constants ªººK­n´y­z¡C
	/// </summary>
	public class Constants
	{
		public const string msCONST_APPENDER_INTERFACE_NAME = "IAppender";
		public const string msCONST_LAYOUT_INTERFACE_NAME = "ILayout";
		public const string msCONST_LOG4NET_APPENDER_NAMESPACE_PATH = "log4net.Appender.";
		public const string msCONST_LOG4NET_ASSEMBLY_NAME = "log4net";
		public const string msCONST_LOG4NET_NAMESPACE_PATH = "log4net.Layout.";
		public const string msCONST_LOG4NET_DEFAULT_PATTERNLAYOUT = "%p %d{yyyy/MM/dd HH:mm:ss,fff} %t %c %X{rquid}- %m%n";
		public const string msCONST_LOG4NET_DEFAULT_LAYOUT = "log4net.Layout.PatternLayout";
		public const string msCONST_NOLAYOUT_APPENDER = "AdoNetAppender";

		public class ArgName
		{
			public const string msCONST_LOG4NET_CONVPATTERN = "conversionPattern";
			public const string msCONST_LOG4NET_LAYOUT = "layout";
		}

		public class ArgInfoFieldName
		{
			public const string NameField = "Name";
			public const string ValueField = "Value";
			public const string DataTypeField = "DataType";
			public const string DescriptionField = "Description";
			public const string EnumValuesField = "EnumValues";
			public const string IsTagNameField = "IsTagName";
			public const string ValueAttriNameField = "ValueAttriName";
			public const string UITypeField = "UIType";
		}
	}
}
