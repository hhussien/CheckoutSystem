using System;
using System.Collections;

namespace Domain.DataAccess.Configurations.Interfaces
{
	public interface IConfigurationCollection : ICloneable
	{
        IConfiguration GetConfiguration(string key);

        void AddConfiguration(IConfiguration config);

		int Count 
		{
			get;
		}

		ICollection Keys 
		{
			get;
		}
	}

} 
