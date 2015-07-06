using System;
using System.Reflection;
using System.Xml;

namespace Domain.DataAccess.Utilities.XmlSerialization.Serializers
{
    /// <summary>
    /// Summary description for CollectionSerializer.
    /// </summary>
    internal class CollectionSerializer : BaseSerializer
    {
        public override object Deserialize(XmlTextReader r, Type type) 
        {
            var col = type.Assembly.CreateInstance(type.FullName);
            var instanceName = r.Name;

            if (r.IsEmptyElement) return col;
            var addToColl = type.GetMethod("Add");

            r.Read();

            // Loop until end of collection
            while (!(r.NodeType == XmlNodeType.EndElement && r.Name == instanceName))
            {
                // Skip the end element node
                if(r.NodeType == XmlNodeType.EndElement)
                    r.Read();

                // Skip over white space or comments
                while(r.NodeType == XmlNodeType.Whitespace ||
                      r.NodeType == XmlNodeType.Comment) 
                {
                    r.Read();
                }

                if (r.NodeType != XmlNodeType.Element) continue;
                var childType = addToColl.GetParameters()[0].ParameterType;

                object child = CustomSerializer.DeserializeObject(r, childType);
                object[] args = { child };
                addToColl.Invoke(col, args);

                // break the endless loop if the object is an 'empty' object.
                if (r.IsEmptyElement)
                    r.Read();

                //else if (r.NodeType != XmlNodeType.EndElement)
                //{
                //    // Only an Element or EndElement is valid so throw an exception
                //    //throw new FrameworkException(
                //        //string.Format("CollectionSerializer expects all Xml nodes but found {0} node: {1}",
                //        //r.NodeType.ToString(), r.Value));
                //}
            }

            return col;
        }
    }
}
