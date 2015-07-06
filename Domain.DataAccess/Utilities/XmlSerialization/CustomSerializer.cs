

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using Domain.DataAccess.Utilities.XmlSerialization.Serializers;

namespace Domain.DataAccess.Utilities.XmlSerialization
{
    /// <summary>
    /// Summary description for CustomXmlSerializer.
    /// </summary>
    public class CustomSerializer
    {
        
        // Attribute for class Type name
        public const string TypeAttribute = "type";

        public static object Deserialize(XmlTextReader xmlReader, Type type) 
        {
            object obj;
            xmlReader.MoveToContent();
            obj = DeserializeObject(xmlReader, type);
            return obj;
        }

        public static object DeserializeObject(XmlTextReader r, Type type) 
        {
            object o = null;
            Type valueType = null;
            ISerializer serializer = null;

            // Skip over any whitespace
            while(r.NodeType == XmlNodeType.Whitespace || r.NodeType == XmlNodeType.Comment) 
            {
                r.Read();
            }

            valueType = type;
            if(valueType.IsValueType || valueType == typeof(String)) 
            {
                serializer = SerializerFactory.GetSerializer(valueType);
                o = serializer.Deserialize(r, valueType);
            }
            else 
            {
                string instanceName = r.Name;

               
                    o = type.Assembly.CreateInstance(type.FullName);

                    if(o is IList)
                    {
                        serializer = SerializerFactory.GetSerializer(o.GetType());
                        o = serializer.Deserialize(r, o.GetType());
                    }
                    else 
                    {
                        PropertyInfo[] properties = o.GetType().GetProperties(BindingFlags.Public|BindingFlags.Instance);
			
                        // Empty element will have no properties to read
                        if(!r.IsEmptyElement) 
                        {
                            while(r.Read()) 
                            {
                                if((r.Name == instanceName) && (r.NodeType == XmlNodeType.EndElement)) 
                                {
                                    // Break out of loop when done with the instance being read
                                    break;
                                } 
                                else if (r.NodeType == XmlNodeType.EndElement || r.NodeType == XmlNodeType.Whitespace || r.NodeType == XmlNodeType.Comment) 
                                {
                                    // Continue when done reading an element within the instance or hitting Whitespace
                                    continue;
                                }
                
                                PropertyInfo elementProperty = GetProperty(r.Name, properties);

                                if (elementProperty == null)
                                    continue;

                                //Ignore properties with no set method
                                if (!elementProperty.CanWrite)
                                    continue;

                                //Ignore static properties
                                if (elementProperty.GetSetMethod().IsStatic)
                                    continue;

                              
                                object objDeserialized = null;

                                if((r.NodeType == XmlNodeType.Element)) 
                                {
                                    serializer = SerializerFactory.GetSerializer(elementProperty.PropertyType);

                                    if(serializer != null) 
                                    {
                                        objDeserialized = serializer.Deserialize(r, elementProperty.PropertyType);
                                    }
                                    
                                }

                                if ( objDeserialized != null )
                                    elementProperty.SetValue(o, objDeserialized, null);
                            }
                        }
                    }
            }
            return o;
        }

        
        private static PropertyInfo GetProperty(string name, IEnumerable<PropertyInfo> properties)
        {
            return (from property in properties let attribs = property.GetCustomAttributes(false) let attrib = GetAttribFromName(name, attribs) where attrib != null || property.Name.Equals(name) select property).FirstOrDefault();
        }


        /// <summary>
        /// Get the element name from attributes.
        /// Supports multiple XmlElementAttribute declaration, so that the
        /// following syntax is supported:
        /// <code>
        ///   [XmlElementAttribute("DropCase", typeof(DropCase))]
        ///   [XmlElementAttribute("SetCase", typeof(SetCase))]
        ///   public object Item
        ///   {
        ///       get { return _item; }
        ///       set { _item = value; }
        ///   }
        /// </code>
        /// That is, <code>Item</code> will serialize to either 
        /// <code>DropCase</code> or <code>SetCase</code> 
        /// depending on the type of the <code>Item</code>.
        /// 
        /// If no name can be found using these methods, the property
        /// info name will be used.
        /// </summary>
        /// <param name="obj">object to be serialized</param>
        /// <param name="pi">the property info</param>
        /// <returns>the element name to be used</returns>
        public static string GetElementName(object obj, PropertyInfo pi)
        {
            string name = null;
            object[] attribs = pi.GetCustomAttributes(false);

            if (obj != null)
            {
                Type t = obj.GetType();
                for (int i = 0; i < attribs.Length; i++)
                {
                    object attrib = attribs[i];
                    if(attrib.GetType() == typeof(XmlRootAttribute)) 
                    {
                        name = (attrib as XmlRootAttribute).ElementName;
                        break;
                    }
                    else if(attrib.GetType() == typeof(XmlElementAttribute)) 
                    {
                        var elAttrib = attrib as XmlElementAttribute;
                        if (elAttrib != null && (name == null || elAttrib.Type == t))
                        {
                            name = elAttrib.ElementName;
                        }
                    }
                }
            }

            return name ?? (name = pi.Name);
        }

        private static string GetAttributeName(object attrib)
        {
            string name = null;
            Type t = attrib.GetType();

            if(t == typeof(XmlRootAttribute)) 
                name = (attrib as XmlRootAttribute).ElementName;
            else if(t == typeof(XmlElementAttribute)) 
                name = (attrib as XmlElementAttribute).ElementName;
            else if(t == typeof(XmlAttributeAttribute)) 
                name = (attrib as XmlAttributeAttribute).AttributeName;

            return name;
        }

        private static object GetAttribFromName(string name, IEnumerable<object> attribs)
        {
            return (from attrib in attribs let attrName = GetAttributeName(attrib) where attrName != null && attrName.Equals(name) select attrib).FirstOrDefault();
        }
    }

}
