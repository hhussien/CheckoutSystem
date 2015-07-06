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


         [TestMethod]
         public void GetPricewithOffer()
         {

             var itemApple1 = new Item { BarCodeId = 111 };//Apple barcode
             var itemApple2 = new Item { BarCodeId = 111 };//Apple barcode
             var itemApple3 = new Item { BarCodeId = 111 };//Apple barcode
             var itemApple4 = new Item { BarCodeId = 111 };//Apple barcode
             var itemApple5 = new Item { BarCodeId = 111 };//Apple barcode
             var itemOrange1 = new Item { BarCodeId = 222 };//Orange barcode
             var itemOrange2 = new Item { BarCodeId = 222 };//Orange barcode
             var itemOrange3 = new Item { BarCodeId = 222 };//Orange barcode
             var itemOrange4 = new Item { BarCodeId = 222 };//Orange barcode

             var items = new List<Item> { itemApple1,itemApple2,itemApple3,itemApple4,itemApple5,itemOrange1,itemOrange2,
                 itemOrange3, itemOrange4 };
             var priceCalculationResource = new PriceCalculationResource { Items = items };

             var manager = new PriceCalculationManager();
             var result = manager.CalculatePrice(priceCalculationResource);

             Assert.IsNotNull(result);
             Assert.IsTrue(result.TotalPrice.ToString() == "2.3");
         }

    }
}
