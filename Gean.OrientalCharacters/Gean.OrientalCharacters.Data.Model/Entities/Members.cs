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
	/// IMembers interface for NHibernate mapped table 'Members'.
	/// </summary>
	public interface IMembers
	{
		#region Public Properties
		
		long Id
		{
			get ;
		}
		
		string Guid
		{
			get ;
		}
		
		string MemberName
		{
			get ;
		}
		
		string Password
		{
			get ;
		}
		
		DateTime AppendDatetime
		{
			get ;
		}
		
		DateTime LoginDatetime
		{
			get ;
		}
		
		long LoginCount
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
		/// <returns>Members object restored from XML file</returns>
		Members DeserializeFromFile( string pXMLFilePath );
		
		#endregion
		
		
	}

	/// <summary>
	/// Members object for NHibernate mapped table 'Members'.
	/// </summary>
	[Serializable]
	public class Members : ICloneable,IMembers
	{
		#region Member Variables

		protected long _id;
		protected string _guid;
		protected string _membername;
		protected string _password;
		protected DateTime _appenddatetime;
		protected DateTime _logindatetime;
		protected long _logincount;
		protected bool _bIsDeleted;
		protected bool _bIsChanged;
		#endregion
		
		#region Constructors
		public Members() {}
		
		#endregion
		
		#region Public Properties
		
		public virtual long Id
		{
			get { return _id; }
		}
		
		public virtual string Guid
		{
			get { return _guid; }
		}
		
		public virtual string MemberName
		{
			get { return _membername; }
		}
		
		public virtual string Password
		{
			get { return _password; }
		}
		
		public virtual DateTime AppendDatetime
		{
			get { return _appenddatetime; }
		}
		
		public virtual DateTime LoginDatetime
		{
			get { return _logindatetime; }
		}
		
		public virtual long LoginCount
		{
			get { return _logincount; }
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
			Members castObj = null;
			try	
			{ 
				castObj = (Members)obj;
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
		/// <returns>Members object restored from XML file</returns>
		public virtual Members DeserializeFromFile( string pXMLFilePath )
		{
			Members _result = null;
			
			System.Xml.Serialization.XmlSerializer seriliaser = new System.Xml.Serialization.XmlSerializer( this.GetType() );
			using(System.IO.TextReader txtReader = new System.IO.StreamReader( pXMLFilePath ))
			{
				_result = (Members)seriliaser.Deserialize( txtReader );
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
	
	#region Custom ICollection interface for Members 

	
	public interface IMembersCollection : ICollection
	{
		Members this[int index]{	get; set; }
		void Add(Members pMembers);
		void Clear();
	}
	
	[Serializable]
	public class MembersCollection : IMembersCollection
	{
		private IList<Members> _arrayInternal;

		public MembersCollection()
		{
			_arrayInternal = new List<Members>();
		}
		
		public MembersCollection( IList<Members> pSource )
		{
			_arrayInternal = pSource;
			if(_arrayInternal == null)
			{
				_arrayInternal = new List<Members>();
			}
		}

		public Members this[int index]
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
		public void CopyTo(Array array, int index){ _arrayInternal.CopyTo((Members[])array, index); }
		public IEnumerator GetEnumerator() { return _arrayInternal.GetEnumerator(); }
		public void Add(Members pMembers) { _arrayInternal.Add(pMembers); }
		public void Clear() { _arrayInternal.Clear(); }
		public IList<Members> GetList() { return _arrayInternal; }
	 }
	
	#endregion
}
