using System;
using System.Configuration;

namespace Gean.Setting
{
	public abstract class ValueSettingBase : Setting
	{
		protected ValueSettingBase( string name ) :
			this( name, null )
		{
		}
		
		protected ValueSettingBase( string name, object defaultValue )
		{
			if ( string.IsNullOrEmpty( name ) )
			{
				throw new ArgumentNullException( "name" );
			}

			this.name = name;
			DefaultValue = defaultValue;
			SerializeAs = SettingsSerializeAs.String;
		}
		
		public string Name
		{
			get { return this.name; }
		}
		
		public object DefaultValue { get; set; }
		
		public SettingsSerializeAs SerializeAs { get; set; }
		
		public bool LoadUndefinedValue { get; set; }
		
		public bool SaveUndefinedValue { get; set; }
		
		public bool HasValue
		{
			get { return Value != null; }
		} // HasValue
		
		public override bool HasChanged
		{
			get
			{
				object originalValue = OriginalValue;
				object value = Value;

				if ( originalValue == value )
				{
					return false;
				}
				if ( originalValue != null && originalValue.Equals( value ) )
				{
					return false;
				}
				return true;
			}
		}
		
		public abstract object OriginalValue
		{
			get;
		}
		
		public abstract object Value
		{
			get;
			set;
		}
		
		public override string ToString()
		{
			return string.Concat( name, "=", Value );
		}
		
		private readonly string name;

	}
}