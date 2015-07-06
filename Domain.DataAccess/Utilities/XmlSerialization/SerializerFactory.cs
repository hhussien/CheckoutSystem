using System;
using System.Collections;
using Domain.DataAccess.Utilities.XmlSerialization.Serializers;

namespace Domain.DataAccess.Utilities.XmlSerialization
{
    /// <summary>
    /// Summary description for SerializerFactory.
    /// </summary>
    internal class SerializerFactory
    {
        public static ISerializer GetSerializer(Type type)
        {
            ISerializer serializer = null;

            if ((type.GetInterfaces() as IList).Contains(typeof(IList)))
                serializer = new CollectionSerializer();
            else if (type.IsValueType || type == typeof(String))
                serializer = new ObjectSerializer();

            return serializer;
        }
    }
}
