using System.Xml;
using Domain.DataAccess.Configurations.Interfaces;

namespace Domain.DataAccess.Configurations
{
    public class ConfigurationData : IConfiguration
	{
        #region Class Members

        private XmlDocument m_configBasis;
        private string m_name;

        #endregion

        #region Class Constructors
		/// <summary>
        /// Public constructor for this class extends the <see cref="ConfigurationData"/> class.
		/// </summary>
		/// <param name="ruleNode">source XmlDocment from which to retrieve the business rule.</param>
		public ConfigurationData(XmlNode ruleNode)
		{
            XmlNode configDataNode;

            configDataNode = ruleNode.SelectSingleNode("ruleData");
            m_name = configDataNode.Attributes["name"].Value;

            m_configBasis = new XmlDocument();
            m_configBasis.LoadXml(configDataNode.InnerXml);
		}

        #endregion Class Constructors

        public object Clone()
        {
            var newConfig = MemberwiseClone() as IConfiguration;
            return newConfig;
        }

        /// <summary>
        /// The name assigned to the config.
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        /// <summary>
        /// The config basis for the config.  This value is either an XSL transform stylesheet or flat data for lookup
        /// type rules.
        /// </summary>
        public XmlDocument ConfigurationBasis
        {
            get { return m_configBasis; }
            set { m_configBasis = value; }
        }
	}

} 
