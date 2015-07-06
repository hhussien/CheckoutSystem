
using System;
using System.Globalization;
using System.Xml;

namespace Domain.DataAccess.Utilities.XmlSerialization.Serializers
{
    /// <summary>
    /// Summary description for ObjectSerializer.
    /// </summary>
    internal class ObjectSerializer : BaseSerializer
    {

        public override object Deserialize(XmlTextReader r, Type type) 
        {
            object obj = null;

            // Read to get to the text value of the node if not an empty element
            if(!r.IsEmptyElement) 
                r.Read();

			while(r.NodeType == XmlNodeType.Comment)
				r.Read();

			if(type == typeof(String))
                obj = r.Value;
            else if(type == typeof(int)) 
                obj = int.Parse(r.Value, CultureInfo.InvariantCulture);
            else if(type == typeof(double)) 
                obj = double.Parse(r.Value, CultureInfo.InvariantCulture);
            
            else
                throw new Exception(String.Format
                    ("Error occured, cannot deserialize unknown type: {0}", type.FullName));
                
            return obj;
        }
    }
}
