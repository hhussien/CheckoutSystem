using System;
using Domain.Services.DataContract;

namespace Domain.Services.GetPrice.Managers
{
    /// <summary>
    /// PriceCalculationManager
    /// </summary>
    public class PriceCalculationManager
    {
        /// <summary>
        /// CalculatePrice method will call the mappers to get the total price includes in the status data contract
        /// </summary>
        /// <param name="priceCalculationResource"> contains the list of items</param>
        /// <example>[ 111, 111, 222, 111 ] </example> 111 represnt apple barcode, 222 represent orange barcode for example
        /// <returns>PriceCalculationStatusResource</returns> 
        public PriceCalculationStatusResource CalculatePrice(PriceCalculationResource priceCalculationResource)
        {
            var priceCalculationStatusResource = new PriceCalculationStatusResource();

            //Validate resource
            if (!ValidateResource(priceCalculationResource)) return priceCalculationStatusResource;
            try
            {
                //Call mapper to get total price
                var priceCalculationMapper = new PriceCalculationMapper();
                priceCalculationStatusResource = priceCalculationMapper.GetPrice(priceCalculationResource.Items);
                
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error in calculating price in PriceCalculationManager.CalculatePrice method : {0}",ex.Message));
            }
            
            return priceCalculationStatusResource;
        }


        /// <summary>
        /// ValidateResource to validate the request body object
        /// </summary>
        /// <param name="priceCalculationResource"></param>
        /// <returns>bool</returns>
        private static bool ValidateResource(PriceCalculationResource priceCalculationResource)
        {
            var valid = priceCalculationResource != null && priceCalculationResource.Items != null;

            return valid;
        }
    }
}
