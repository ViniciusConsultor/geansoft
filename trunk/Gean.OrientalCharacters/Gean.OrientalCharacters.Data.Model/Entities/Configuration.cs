/*
using MyGeneration/Template/NHibernate (c) by Sharp 1.4
based on OHM (alvy77@hotmail.com)
*/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Gean.OrientalCharacters.Data.Model
{

	/// <summary>
	/// IConfiguration interface for NHibernate mapped table 'Configuration'.
	/// </summary>
	public interface IConfiguration
	{
		#region Public Properties
		
		long Id
		{
			get ;
		}
		
		string ConfigurationKey
		{
			get ;
		}
		
		string ConfigurationValue
		{
			get ;
		}
		
		long Owner
		{
			get ;
		}
		
		DateTime ModifedDatatime
		{
			get ;
		}
		
		bool IsDeleted { get; set; }
		bool IsChanged { get; set; }
		
		#endregion 
		#region Serialize
		/// <summary>
		/// Method for entity class serialization to XML file
		/// </summary>
		/// <param name="pXMLFilePath">Path of the XML file to write to. Will be overwritten if already exists.</param>
		void SerializeToFile( string pXMLFilePath );
		
		/// <summary>
		/// Method for entity class deserialization from XML file. Does not change this object content but returns another deserialized object instance
		/// </summary>
		/// <param name="pXMLFilePath">Path of the XML file to read from.</param>
		/// <returns>Configuration object restored from XML file</returns>
		Configuration DeserializeFromFile( string pXMLFilePath );
		
		#endregion
		
		
	}

	/// <summary>
	/// Configuration object for NHibernate mapped table 'Configuration'.
	/// </summary>
	[Serializable]
	public class Configuration : ICloneable,IConfiguration
	{
		#region Member Variables

		protected long _id;
		protected string _configurationkey;
		protected string _configurationvalue;
		protected long _owner;
		protected DateTime _modifeddatatime;
		protected bool _bIsDeleted;
		protected bool _bIsChanged;
		#endregion
		
		#region Constructors
		public Configuration() {}
		
		#endregion
		
		#region Public Properties
		
		public virtual long Id
		{
			get { return _id; }
		}
		
		public virtual string ConfigurationKey
		{
			get { return _configurationkey; }
		}
		
		public virtual string ConfigurationValue
		{
			get { return _configurationvalue; }
		}
		
		public virtual long Owner
		{
			get { return _owner; }
		}
		
		public virtual DateTime ModifedDatatime
		{
			get { return _modifeddatatime; }
		}
		

		public bool IsDeleted
		{
			get
			{
				return _bIsDeleted;
			}
			set
			{
				_bIsDeleted = value;
			}
		}
		
		public bool IsChanged
		{
			get
			{
				return _bIsChanged;
			}
			set
			{
				_bIsChanged = value;
			}
		}
		
		#endregion 
		
		#region Equals And HashCode Overrides
		/// <summary>
		/// local implementation of Equals based on unique value members
		/// </summary>
		public override bool Equals( object obj )
		{
			if( this == obj ) 
				return true;
			Configuration castObj = null;
			try	
			{ 
				castObj = (Configuration)obj;
			} 
			catch(Exception) 
			{
				return false; 
			} 
			return ( castObj != null ) && ( this._id == castObj.Id );
		}
		/// <summary>
		/// local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			int hash = 57; 
			hash = 27 ^ hash ^ _id.GetHashCode();
			return hash; 
		}
		#endregion
		
		#region Serialize
		/// <summary>
		/// Method for entity class serialization to XML file
		/// </summary>
		/// <param name="pXMLFilePath">Path of the XML file to write to. Will be overwritten if already exists.</param>
		public virtual void SerializeToFile( string pXMLFilePath )
		{
			System.Xml.Serialization.XmlSerializer seriliaser = new System.Xml.Serialization.XmlSerializer( this.GetType() );
			using(System.IO.TextWriter txtWriter = new System.IO.StreamWriter( pXMLFilePath ))
			{
				seriliaser.Serialize(txtWriter, this);
				txtWriter.Close();
			}
		}
		/// <summary>
		/// Method for entity class deserialization from XML file. Does not change this object content but returns another deserialized object instance
		/// </summary>
		/// <param name="pXMLFilePath">Path of the XML file to read from.</param>
		/// <returns>Configuration object restored from XML file</returns>
		public virtual Configuration DeserializeFromFile( string pXMLFilePath )
		{
			Configuration _result = null;
			
			System.Xml.Serialization.XmlSerializer seriliaser = new System.Xml.Serialization.XmlSerializer( this.GetType() );
			using(System.IO.TextReader txtReader = new System.IO.StreamReader( pXMLFilePath ))
			{
				_result = (Configuration)seriliaser.Deserialize( txtReader );
				txtReader.Close();
			}
			return _result;
		}
		#endregion
		
		
		#region ICloneable methods
		
		public object Clone()
		{
			return this.MemberwiseClone();
		}
		
		#endregion
	}
	
	#region Custom ICollection interface for Configuration 

	
	public interface IConfigurationCollection : ICollection
	{
		Configuration this[int index]{	get; set; }
		void Add(Configuration pConfiguration);
		void Clear();
	}
	
	[Serializable]
	public class ConfigurationCollection : IConfigurationCollection
	{
		private IList<Configuration> _arrayInternal;

		public ConfigurationCollection()
		{
			_arrayInternal = new List<Configuration>();
		}
		
		public ConfigurationCollection( IList<Configuration> pSource )
		{
			_arrayInternal = pSource;
			if(_arrayInternal == null)
			{
				_arrayInternal = new List<Configuration>();
			}
		}

		public Configuration this[int index]
		{
			get
			{
				return _arrayInternal[index];
			}
			set
			{
				_arrayInternal[index] = value;
			}
		}

		public int Count { get { return _arrayInternal.Count; } }
		public bool IsSynchronized { get { return false; } }
		public object SyncRoot { get { return _arrayInternal; } }
		public void CopyTo(Array array, int index){ _arrayInternal.CopyTo((Configuration[])array, index); }
		public IEnumerator GetEnumerator() { return _arrayInternal.GetEnumerator(); }
		public void Add(Configuration pConfiguration) { _arrayInternal.Add(pConfiguration); }
		public void Clear() { _arrayInternal.Clear(); }
		public IList<Configuration> GetList() { return _arrayInternal; }
	 }
	
	#endregion
}
