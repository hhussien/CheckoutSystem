using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace Domain.DataAccess.Utilities.XmlSerialization
{
	/// <summary>
	/// Summary description for WWXmlSerializer.
	/// </summary>
    public class SerializerHelper
    {

        // Attribute for class Type name
        public const string TYPE_ATTRIBUTE = "type";
     
        /// <summary>
        /// Deserializes an XML string into an object.
        /// </summary>
        /// <param name="xml">The XML string representing the object.</param>
        /// <param name="type">The type of object to deserialize the XML into.</param>
        /// <returns>An instance of the object represented by the XML string.</returns>
        public static object Deserialize(string xml, Type type) 
        {
            StringReader reader = null;
            object output = null;

            using (reader = new StringReader(xml)) 
            {
              output = DeserializeObject(new XmlTextReader(reader), type);
            }

            return output;
        }


	    public static object DeserializeObject(XmlTextReader r, Type type)
	    {
	        object o = null;

            r.MoveToContent();

	        o = type.Assembly.CreateInstance(type.FullName);

	        if (o is IList)
	        {
	            var serializer = SerializerFactory.GetSerializer(o.GetType());
	            o = serializer.Deserialize(r, o.GetType());
	        }

	        return o;
	    }

    }
}
