

using System;
using System.Xml;

namespace Domain.DataAccess.Utilities.XmlSerialization.Serializers
{
    /// <summary>
    /// Summary description for BaseSerializer.
    /// </summary>
    internal abstract class BaseSerializer : ISerializer
    {
        public abstract object Deserialize(XmlTextReader r, Type type);

    }
}
