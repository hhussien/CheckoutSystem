
using System.Runtime.Serialization;

namespace Domain.Services.GetPrice.Entities
{
    [DataContract]
    public class Item
    {
        /// <summary>
        /// BarCodeId represent item barode
        /// </summary>
        [DataMember]
        public int BarCodeId { get; set; }

    }
}
