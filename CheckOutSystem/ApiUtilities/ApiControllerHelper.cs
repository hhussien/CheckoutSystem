using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CheckOutSystem.ApiUtilities
{
   public static class ApiControllerHelper
    {
        
        public static async Task<HttpResponseMessage> TryWork(this ApiController controller, Func<Task<HttpResponseMessage>> function)
        {
            if (!controller.ModelState.IsValid)
            {
                var wrongness = string.Join(Environment.NewLine,
                                            controller.ModelState.Values.SelectMany(
                                                v => v.Errors.Select(e => e.Exception.ToString()))
                                                      .ToArray());
                //Parsing the exception to get the invalid values
                var invalidKeys = new StringBuilder();
                foreach (var key in controller.ModelState.Keys)
                {
                    int index = key.IndexOf('.');
                    var modifiedKey = key.Substring(index+1); //to remove word "value."
                    invalidKeys.Append(modifiedKey);

                    if (key != controller.ModelState.Keys.Last())
                        invalidKeys.Append(", ");
                }

                var ex = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(wrongness),
                    ReasonPhrase = String.Format("Unable to deserialize input value(s): {0}.", invalidKeys)
                };
               
                throw new HttpResponseException(ex);
            }
            try
            {
                
               var result = await function();
               return result;
                
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.ToString()),
                    ReasonPhrase = "An Error Occurred."
                };
                throw new HttpResponseException(resp);
            }
        }
    }
}