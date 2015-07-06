using System;
using System.Collections;
using System.IO;
using Domain.DataAccess.Configurations.Interfaces;

namespace Domain.DataAccess.Configurations
{
	public class ConfigurationFactory
	{
		/// <summary>
		/// private Singleton instance
		/// </summary>
		private static volatile ConfigurationFactory m_Instance;

		/// <summary>
		/// Sync lock to ensure only one instance ever created
		/// </summary>
		private static object m_SyncRoot = new Object();

		/// <summary>
		/// Store configuration for each system
		/// </summary>
		private Hashtable m_configCollRepository;

		/// <summary>
		/// config file extension
		/// </summary>
		private const string FILE_EXTENSION = ".config";

		private ConfigurationFactory() 
		{
			m_configCollRepository = new Hashtable();
		}

		/// <summary>
		/// This method creates the instance of the singleton. 
		/// Before calling the private constructor it acquires the lock
		/// on the lock object to prevent many instances of singleton
		/// from being created
		/// </summary>
		private static void CreateInstance()
		{
			if (null == m_Instance)
			{
				// lock the lock object to prevent many
				// instances of singleton from being created
				lock(m_SyncRoot)
				{
					// check if the instance is still null
					if (null == m_Instance)
					{
						m_Instance = new ConfigurationFactory();
					}
				}
			}
		}

        
        /// <param name="classType">The class type to be used for appropriate localized XML file retrieval</param>
        /// <returns>The <see cref="IConfigurationSet"/> that represents the configuration set defined for the given class type.</returns>
        public static IConfigurationSet GetConfigurationSet(Type classType)
        {
            string nameSpace = "";

            string baseDir = Path.GetFullPath(String.Format(@"{0}\Configuration\", AppDomain.CurrentDomain.BaseDirectory));

            nameSpace = classType.Namespace;

           
            string filename = String.Concat(baseDir, nameSpace, ".", classType.Name, FILE_EXTENSION);
           
            if (!File.Exists(filename))
            {
                throw new Exception(String.Format
                   ("Cannot find configuration \"{0}.{1}\" in directory \"{2}\"", classType.Name, FILE_EXTENSION, baseDir));
            }

            return GetConfigurationSet(filename);
        }


        private static IConfigurationSet GetConfigurationSet(string filename) 
		{
			ConfigurationSet configurationSet = null;
            Hashtable configColl = null;


		    try
		    {
		        //The ConfigurationFactory instance is a singleton, it will be instantiated at this point only if it does not already exist.
                // call to create instance of the signleton
		        CreateInstance();
		    }
		    catch (Exception ex)
		    {
		        throw new Exception(
                    String.Format("Error occured while calling to get the ConfigurationFactory instance in ConfigurationFactory.GetConfigurationSet :{0}"
		                , ex.Message));
		    }

		    try
			{
                //Creating ConfigurationSet with filename if not already created.

				configColl = m_Instance.m_configCollRepository;
				if(!configColl.Contains(filename)) 
				{
                    lock (configColl)
                    {
                        if (!configColl.Contains(filename))
                        {
                            configurationSet = new ConfigurationSet(filename);
                            configColl.Add(filename, configurationSet);
                        }
                    }
				}
				
				configurationSet = configColl[filename] as ConfigurationSet;
			}
			catch(Exception ex)
			{
                throw new Exception(
                 String.Format("Error occured initializing Creating ConfigurationSet. in ConfigurationFactory.GetConfigurationSet :{0}"
                     , ex.Message));
			}

			return configurationSet;
		}
	}
}