using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain.DataAccess.GetPrice.Entities;
using Domain.DataAccess.GetPrice.Mangers;
using Domain.Services.DataContract;
using Domain.Services.GetPrice.Entities;

namespace Domain.Services.GetPrice
{
    /// <summary>
    /// PriceCalculationMapper
    /// </summary>
    public class PriceCalculationMapper
    {
        /// <summary>
        /// GetPrice method will get the configured values for all items in the system and start 
        /// calculating the price depending on the matching barcodes between the request barcodes 
        /// and the one exist in the returned collections.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public PriceCalculationStatusResource GetPrice(List<Item> items)
        {
            var priceCalculationStatusResource = new PriceCalculationStatusResource();


            double total = 0;
            var totalStr = "0";

            //Apples cost 60p and oranges cost 25p.
            //111 apple barcode
            //222 orange barcode
            try
            {
                //This Collection can be stored in database and returned values from it.
                //Or save this static values in configuration files and returned these values from it.
                //ItemsPriceCollection is static and will be loads once in the static constructor.
                ItemPriceCollection itemsPriceColl = ItemsPriceCollectionManager.GetItemPriceCollection();

                foreach (Item item in items)
                {
                    foreach (ItemPrice itemPrice in itemsPriceColl)
                    {
                        if (item.BarCodeId == itemPrice.BarCodeId)
                        {
                            double price = itemPrice.Price;
                            total += price;
                            break;
                        }

                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception(String.Format("Error in calculating price in PriceCalculationMapper.CalcualtePrice method : {0}", ex.Message));
            }

            
            //set total price to the data contrat that will be returen in the response body
            priceCalculationStatusResource.TotalPrice = total;

            return priceCalculationStatusResource;
        }
 		
    }
}
