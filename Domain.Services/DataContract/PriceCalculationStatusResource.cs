using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.DataContract
{
    public class PriceCalculationStatusResource
    {
        /// <summary>
        /// TotalPrice
         /// </summary>
         [DataMember]
         public Double TotalPrice { get; set; }
    }
}
