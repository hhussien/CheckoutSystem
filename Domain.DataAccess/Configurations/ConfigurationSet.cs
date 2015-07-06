using System;
using System.Collections;
using System.Xml;
using Domain.DataAccess.Configurations.Interfaces;

namespace Domain.DataAccess.Configurations
{
    public class ConfigurationSet : IConfigurationSet
    {
        #region Class Members
        private XmlDocument m_masterDoc;
        private Hashtable m_configurations;
        #endregion

        #region Class Constructors

        /// <summary>
        /// Public Method. Creates ConfigurationSet with XmlDoc containing configurations.
        /// </summary>
        /// <param name="filename">String for filename containing configurations.</param>
        public ConfigurationSet(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                try
                {
                    m_configurations = new Hashtable();
                    m_masterDoc = new XmlDocument();
                    m_masterDoc.Load(filename);
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Error occured in ConfigurationSet while constructing the ConfigurationSet object with the filename:{0}, {1}", filename, ex.Message));
                }
            }
            else
            {
                throw new Exception(String.Format("Error occured in ConfigurationSet while constructing the ConfigurationSet object, The filename parameter passed to the ConfigurationSet constructor is null or empty string."));
            }

            InitializeConfigurations();
        }
        #endregion

        /// <summary>
        /// Private method that parses the configuration XML and creates the ConfigCollections that reprent that
        /// configuration.  This is used to store the entire set of collections in memory once and have clones
        /// sent out whenever they are requested.
        /// </summary>
        private void InitializeConfigurations()
        {
            XmlNodeList nodeList;
            IConfigurationCollection list = null;
            IConfiguration config = null;

            m_configurations.Clear();

            const string xpathVal = "//rule";
            nodeList = m_masterDoc.SelectNodes(xpathVal);

            try
            {
                foreach (XmlNode ruleNode in nodeList)
                {
                    config = new ConfigurationData(ruleNode);
                    list = new ConfigurationCollection();
                    m_configurations.Add("ItemPrice", list);
                    list.AddConfiguration(config);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occured while initializing the configuration in ConfigurationSet.InitializeConfigurations method : {0}", ex.Message));
            }
        }

        /// <summary>
        /// Public Method.  Retrieves a <see cref="ConfigurationCollection"/> of configurations for the requested config category.
        /// </summary>
        /// <param name="configCategory">string value of requested config category.</param>
        /// <returns>IConfigurationCollection of configurations for the requested category.</returns>
        public IConfigurationCollection GetConfigurations(string configCategory)
        {
            IConfigurationCollection list;

            if (m_configurations.Count == 0)
            {
                // Return null if the configuration list is empty
                list = null;
            }
            else
            {
                list = m_configurations[configCategory] as IConfigurationCollection;
                list = list.Clone() as IConfigurationCollection;
            }

            return list;
        }

    }
}