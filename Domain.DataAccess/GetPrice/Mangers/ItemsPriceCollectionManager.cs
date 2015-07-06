using System;
using Domain.DataAccess.Configurations;
using Domain.DataAccess.Configurations.Interfaces;
using Domain.DataAccess.GetPrice.Entities;
using Domain.DataAccess.Utilities.XmlSerialization;

namespace Domain.DataAccess.GetPrice.Mangers
{
    public static class ItemsPriceCollectionManager
    {
        #region Private Members

        /// <summary>
        /// Collection of item price objects
        /// </summary>
        private static ItemPriceCollection _itemsPriceCollection;

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Constructor that loads the collection
        /// </summary>
        static ItemsPriceCollectionManager()
        {
            //load configurations form xml file
            _itemsPriceCollection = GetItemsPriceCollectionFromConfig();
        }

        #endregion Constructor


        #region Private Methods

        /// <summary>
        /// It reads an xml config file and deserializes into collection of ItemPriceCollection
        /// </summary>
        /// <returns>Collection of item price</returns>
        private static ItemPriceCollection GetItemsPriceCollectionFromConfig()
        {
            const string configCategory = "ItemPrice";
            const string configRuleData = "ItemPriceData";
            ItemPriceCollection itemsPriceColl = null;

            try
            {

                IConfigurationSet configurationSet = ConfigurationFactory.GetConfigurationSet(typeof(ItemsPriceCollectionManager));
                IConfigurationCollection configurations = configurationSet.GetConfigurations(configCategory);

                IConfiguration itemConfig = configurations.GetConfiguration(configRuleData);
                if (itemConfig != null)
                {
                    itemsPriceColl = SerializerHelper.Deserialize(
                        itemConfig.ConfigurationBasis.InnerXml,
                        typeof(ItemPriceCollection)
                        ) as ItemPriceCollection;
                }
                
            }
            catch(Exception ex)
            {
                throw new Exception(String.Format
                    ("Error occured while retrieving ItemPrice configuration and ItemPricecollection in ItemsPriceCollectionManager.GetItemsPriceCollectionFromConfig method : {0}",
                    ex.Message));
            
            }
            return itemsPriceColl;
        }

        #endregion Private Methods



    #region public 
        /// <summary>
        /// GetItemPriceCollection will return ItemPriceCollection that loads only one in the constructor as it's static.
        /// </summary>
        /// <returns>ItemPriceCollection</returns>
        public static ItemPriceCollection GetItemPriceCollection()
        {
            return _itemsPriceCollection;
        }

    #endregion public
    }
}

