using System;
using System.Xml;

namespace Domain.DataAccess.Configurations.Interfaces
{
	public interface IConfiguration : ICloneable
	{

		string Name 
		{
			get;
			set;
		}

		XmlDocument ConfigurationBasis 
		{
			get;
			set;
		}

		

	}

} 
