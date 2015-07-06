// NameValueCollections
// Interfaces
// TraceUtil

using System;
using System.Collections;
using Domain.DataAccess.Configurations.Interfaces;

// FrameworkException

namespace Domain.DataAccess.Configurations
{
	public class ConfigurationCollection : IConfigurationCollection
	{
		#region Class Members
		/// <summary>
		/// Hashtable which in the collection of the configurations
		/// </summary>
		private Hashtable m_Configurations;
		#endregion

		#region Class Constructors
		/// <summary>
		/// Public Constructor
		/// </summary>
		public ConfigurationCollection()
		{
			m_Configurations = new Hashtable();
		}
		#endregion
		
		#region Class Methods
        public IConfiguration GetConfiguration(string key)
		{
	        return m_Configurations[key] as IConfiguration;
		}

	    public void AddConfiguration(IConfiguration config)
		{
            m_Configurations[config.Name] = config;
		}

		/// <summary>
		/// Public Property.
		/// </summary>
		/// <returns>
		/// The collection of keys contained in this ConfigurationCollection.
		/// </returns>
		public ICollection Keys
		{
			get 
			{
				return m_Configurations.Keys;
			}
		}

		/// <summary>
		/// Public Property.
		/// </summary>
		/// <returns>
		/// Integer indicating the number of configurations in the Array of configurations.
		/// </returns>
		public int Count
		{
			get 
			{
				return m_Configurations.Count;
			}
		}

		/// <summary>
		/// Implemets the <see cref="ICloneable"/> interface.
		/// </summary>
		/// <returns>A clone of the collection with clones of the configurations as well.</returns>
		public Object Clone() 
		{
			IConfigurationCollection newCollection;

			newCollection = new ConfigurationCollection();

			foreach(string key in m_Configurations.Keys) 
			{
				IConfiguration config = m_Configurations[key] as IConfiguration;
                newCollection.AddConfiguration(config.Clone() as IConfiguration);
			}

			return newCollection;
		}

		#endregion
	}

} 
