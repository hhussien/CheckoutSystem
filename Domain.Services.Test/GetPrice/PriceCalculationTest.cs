using System.Collections.Generic;
using Domain.Services.DataContract;
using Domain.Services.GetPrice.Entities;
using Domain.Services.GetPrice.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Services.Test.GetPrice
{
     [TestClass]
    public class PriceCalculationTest
    {
         [TestMethod]
         public void GetPrice()
         {

             var itemOne = new Item { BarCodeId = 111 };//Apple barcode
             var itemTwo = new Item { BarCodeId = 222 };//Orange barcode
             var items = new List<Item> { itemOne, itemTwo };
             var priceCalculationResource = new PriceCalculationResource { Items = items };

             var manager = new PriceCalculationManager();
             var result = manager.CalculatePrice(priceCalculationResource);

             Assert.IsNotNull(result);
             Assert.IsTrue(result.TotalPrice.ToString() == "0.85");
         }


    }
}
