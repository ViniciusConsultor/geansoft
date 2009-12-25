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
    /// IImageRecords interface for NHibernate mapped table 'ImageRecords'.
    /// </summary>
    public interface IImageRecords
    {
        #region Public Properties

        long Id
        {
            get;
        }

        string Guid
        {
            get;
        }

        string ImagePath
        {
            get;
        }

        string Author
        {
            get;
        }

        string ProductionTime
        {
            get;
        }

        long? Dynasty
        {
            get;
        }

        string ProductionName
        {
            get;
        }

        string Summary
        {
            get;
        }

        DateTime AppendedDatetime
        {
            get;
        }

        DateTime ModifedDatetime
        {
            get;
        }

        bool IsDeleted { get; set; }
        bool IsChanged { get; set; }

        #endregion

        #region Serialize
        /// <summary>
        /// Method for entity class serialization to XML file
        /// </summary>
        /// <param name="pXMLFilePath">Path of the XML file to write to. Will be overwritten if already exists.</param>
        void SerializeToFile(string pXMLFilePath);

        /// <summary>
        /// Method for entity class deserialization from XML file. Does not change this object content but returns another deserialized object instance
        /// </summary>
        /// <param name="pXMLFilePath">Path of the XML file to read from.</param>
        /// <returns>ImageRecords object restored from XML file</returns>
        ImageRecords DeserializeFromFile(string pXMLFilePath);

        #endregion
    }

    /// <summary>
    /// ImageRecords object for NHibernate mapped table 'ImageRecords'.
    /// </summary>
    [Serializable]
    public class ImageRecords : ICloneable, IImageRecords
    {
        #region Member Variables

        protected long _id;
        protected string _guid;
        protected string _imagepath;
        protected string _author;
        protected string _productiontime;
        protected long? _dynasty;
        protected string _productionname;
        protected string _summary;
        protected DateTime _appendeddatetime;
        protected DateTime _modifeddatetime;
        protected bool _bIsDeleted;
        protected bool _bIsChanged;
        #endregion

        #region Constructors
        public ImageRecords() { }

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

        public virtual string ImagePath
        {
            get { return _imagepath; }
        }

        public virtual string Author
        {
            get { return _author; }
        }

        public virtual string ProductionTime
        {
            get { return _productiontime; }
        }

        public virtual long? Dynasty
        {
            get { return _dynasty; }
        }

        public virtual string ProductionName
        {
            get { return _productionname; }
        }

        public virtual string Summary
        {
            get { return _summary; }
        }

        public virtual DateTime AppendedDatetime
        {
            get { return _appendeddatetime; }
        }

        public virtual DateTime ModifedDatetime
        {
            get { return _modifeddatetime; }
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
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            ImageRecords castObj = null;
            try
            {
                castObj = (ImageRecords)obj;
            }
            catch (Exception)
            {
                return false;
            }
            return (castObj != null) && (this._id == castObj.Id);
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
        public virtual void SerializeToFile(string pXMLFilePath)
        {
            System.Xml.Serialization.XmlSerializer seriliaser = new System.Xml.Serialization.XmlSerializer(this.GetType());
            using (System.IO.TextWriter txtWriter = new System.IO.StreamWriter(pXMLFilePath))
            {
                seriliaser.Serialize(txtWriter, this);
                txtWriter.Close();
            }
        }
        /// <summary>
        /// Method for entity class deserialization from XML file. Does not change this object content but returns another deserialized object instance
        /// </summary>
        /// <param name="pXMLFilePath">Path of the XML file to read from.</param>
        /// <returns>ImageRecords object restored from XML file</returns>
        public virtual ImageRecords DeserializeFromFile(string pXMLFilePath)
        {
            ImageRecords _result = null;

            System.Xml.Serialization.XmlSerializer seriliaser = new System.Xml.Serialization.XmlSerializer(this.GetType());
            using (System.IO.TextReader txtReader = new System.IO.StreamReader(pXMLFilePath))
            {
                _result = (ImageRecords)seriliaser.Deserialize(txtReader);
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

    #region Custom ICollection interface for ImageRecords


    public interface IImageRecordsCollection : ICollection
    {
        ImageRecords this[int index] { get; set; }
        void Add(ImageRecords pImageRecords);
        void Clear();
    }

    [Serializable]
    public class ImageRecordsCollection : IImageRecordsCollection
    {
        private IList<ImageRecords> _arrayInternal;

        public ImageRecordsCollection()
        {
            _arrayInternal = new List<ImageRecords>();
        }

        public ImageRecordsCollection(IList<ImageRecords> pSource)
        {
            _arrayInternal = pSource;
            if (_arrayInternal == null)
            {
                _arrayInternal = new List<ImageRecords>();
            }
        }

        public ImageRecords this[int index]
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
        public void CopyTo(Array array, int index) { _arrayInternal.CopyTo((ImageRecords[])array, index); }
        public IEnumerator GetEnumerator() { return _arrayInternal.GetEnumerator(); }
        public void Add(ImageRecords pImageRecords) { _arrayInternal.Add(pImageRecords); }
        public void Clear() { _arrayInternal.Clear(); }
        public IList<ImageRecords> GetList() { return _arrayInternal; }
    }

    #endregion
}
