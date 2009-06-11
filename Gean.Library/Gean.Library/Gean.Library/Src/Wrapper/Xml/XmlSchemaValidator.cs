using System;
using System.Xml;
using System.Xml.Schema;
using System.IO;


namespace Gean.Xml
{
    /*
    /// <summary>
    /// This class validates an xml string or xml document against an xml schema.
    /// It has public methods that return a boolean value depending on the validation
    /// of the xml.
    /// </summary>
    public class XmlSchemaValidator
    {
        private bool isValidXml = true;
        private string validationError = "";

        /// <summary>
        /// Empty Constructor.
        /// </summary>
        public XmlSchemaValidator()
        {

        }

        /// <summary>
        /// Public get/set access to the validation error.
        /// </summary>
        public String ValidationError
        {
            get
            {
                return "<ValidationError>" + this.validationError + "</ValidationError>";
            }
            set
            {
                this.validationError = value;
            }
        }

        /// <summary>
        /// Public get access to the isValidXml attribute.
        /// </summary>
        public bool IsValidXml
        {
            get
            {
                return this.isValidXml;
            }
        }

        /// <summary>
        /// This method is invoked when the XML does not match
        /// the XML Schema.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            // The xml does not match the schema.
            isValidXml = false;
            this.ValidationError = args.Message;
        }

        /// <summary>
        /// This method validates an xml string against an xml schema.
        /// </summary>
        /// <param name="xml">XML string</param>
        /// <param name="schemaNamespace">XML Schema Namespace</param>
        /// <param name="schemaUri">XML Schema Uri</param>
        /// <returns>bool</returns>
        public bool ValidXmlDoc(string xml, string schemaNamespace, string schemaUri)
        {
            try
            {
                // Is the xml string valid?
                if (xml == null || xml.Length < 1)
                {
                    return false;
                }

                StringReader srXml = new StringReader(xml);
                return ValidXmlDoc(srXml, schemaNamespace, schemaUri);
            }
            catch (Exception ex)
            {
                this.ValidationError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// This method validates an xml document against an xml schema.
        /// </summary>
        /// <param name="xml">XmlDocument</param>
        /// <param name="schemaNamespace">XML Schema Namespace</param>
        /// <param name="schemaUri">XML Schema Uri</param>
        /// <returns>bool</returns>
        public bool ValidXmlDoc(XmlDocument xml, string schemaNamespace, string schemaUri)
        {
            try
            {
                // Is the xml object valid?
                if (xml == null)
                {
                    return false;
                }

                // Create a new string writer.
                StringWriter sw = new StringWriter();
                // Set the string writer as the text writer to write to.
                XmlTextWriter xw = new XmlTextWriter(sw);
                // Write to the text writer.
                xml.WriteTo(xw);
                // Get 
                string strXml = sw.ToString();

                StringReader srXml = new StringReader(strXml);

                return ValidXmlDoc(srXml, schemaNamespace, schemaUri);
            }
            catch (Exception ex)
            {
                this.ValidationError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// This method validates an xml string against an xml schema.
        /// </summary>
        /// <param name="xml">StringReader containing xml</param>
        /// <param name="schemaNamespace">XML Schema Namespace</param>
        /// <param name="schemaUri">XML Schema Uri</param>
        /// <returns>bool</returns>
        public bool ValidXmlDoc(StringReader xml, string schemaNamespace, string schemaUri)
        {
            // Continue?
            if (xml == null || schemaNamespace == null || schemaUri == null)
            {
                return false;
            }

            isValidXml = true;
            XmlValidatingReader vr;
            XmlTextReader tr;
            XmlSchemaCollection schemaCol = new XmlSchemaCollection();
            schemaCol.Add(schemaNamespace, schemaUri);

            try
            {
                // Read the xml.
                tr = new XmlTextReader(xml);
                // Create the validator.
                vr = new XmlValidatingReader(tr);
                // Set the validation tyep.
                vr.ValidationType = ValidationType.Auto;
                // Add the schema.
                if (schemaCol != null)
                {
                    vr.Schemas.Add(schemaCol);
                }
                // Set the validation event handler.
                vr.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
                // Read the xml schema.
                while (vr.Read())
                {
                }

                vr.Close();

                return isValidXml;
            }
            catch (Exception ex)
            {
                this.ValidationError = ex.Message;
                return false;
            }
            finally
            {
                // Clean up
                vr = null;
                tr = null;
            }
        }

        */
}