using System.Collections.Generic;
using System.Runtime.Serialization;
using Domain.Services.GetPrice.Entities;

namespace Domain.Services.DataContract
{
     /// <summary>
        /// 
        /// </summary>
    [DataContract]
    public class PriceCalculationResource
    {
         /// <summary>
        /// Items
         /// </summary>
         [DataMember]
         public List<Item> Items { get; set; }

    }
}