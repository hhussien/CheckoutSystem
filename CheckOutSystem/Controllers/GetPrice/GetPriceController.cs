using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CheckOutSystem.ApiUtilities;
using Domain.Services.DataContract;
using Domain.Services.GetPrice.Managers;

namespace CheckOutSystem.Controllers.GetPrice
{
    public class GetPriceController : ApiController
    {
        /// <summary>
        /// Calculate price (Post method)
        /// </summary>
        /// <param name="priceCalculationResource"></param>
        /// <returns>HttpResponseMessage with calculated price</returns>
        [ActionName("Original")]
        [HttpPost]
        public Task<HttpResponseMessage> GetPrice(PriceCalculationResource priceCalculationResource)
        {
            return this.TryWork(async () =>
            {
                try
                {
                    //call PriceCalculationManager to calculate price of the passed data contract
                    var priceCalculationManager = new PriceCalculationManager();
                    var priceCalculationStatusResource = priceCalculationManager.CalculatePrice(priceCalculationResource);

                    return Request.CreateResponse(HttpStatusCode.OK, priceCalculationStatusResource);

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex);
                }
            }
            );
        }
        /// <summary>
        /// Calculate price with offer (Post method)
        /// </summary>
        /// <param name="priceCalculationResource"></param>
        /// <returns>HttpResponseMessage with calculated price with offer</returns>
         [ActionName("Offer")]
         [HttpPost]
        public Task<HttpResponseMessage> GetPriceOffer(PriceCalculationResource priceCalculationResource)
        {
            return this.TryWork(async () =>
            {
                try
                {
                    //call PriceCalculationManager to calculate price of the passed data contract
                    var priceCalculationManager = new PriceCalculationManager();
                    var priceCalculationStatusResource = priceCalculationManager.CalculatePrice(priceCalculationResource,true);

                    return Request.CreateResponse(HttpStatusCode.OK, priceCalculationStatusResource);

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex);
                }
            }
            );
        }
    }
}
