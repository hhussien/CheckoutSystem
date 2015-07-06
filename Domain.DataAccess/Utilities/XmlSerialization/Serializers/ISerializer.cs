using System;
using System.Xml;

namespace Domain.DataAccess.Utilities.XmlSerialization.Serializers
{
	/// <summary>
	/// Summary description for ISerializer.
	/// </summary>
	internal interface ISerializer
    {
            object Deserialize(XmlTextReader r, Type type);
    }
}
