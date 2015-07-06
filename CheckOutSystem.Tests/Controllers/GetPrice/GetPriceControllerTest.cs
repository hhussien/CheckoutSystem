using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CheckOutSystem.Controllers;
using Domain.Services.DataContract;
using Domain.Services.GetPrice.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CheckOutSystem.Tests.Controllers.GetPrice
{
    [TestClass]
    public class GetPriceControllerTest
    {
        [TestMethod]
        public void GetPrice()
        {
            var itemOne = new Item { BarCodeId = 111 };//Apple barcode
            var itemTwo = new Item { BarCodeId = 222 };//Orange barcode
            var items = new List<Item> {itemOne, itemTwo};
            var priceCalculationResource = new PriceCalculationResource {Items = items};
            var content = JsonConvert.SerializeObject(priceCalculationResource);
            byte[] byteArray = Encoding.UTF8.GetBytes(content);

            HttpWebResponse response = null;

            var theRequest = (HttpWebRequest)WebRequest.Create("http://localhost:27387/api/getprice/Original");
            theRequest.ContentType = "application/Json";
            theRequest.Method = WebRequestMethods.Http.Post;
            theRequest.ContentLength = content.Length;

            var requestStream = theRequest.GetRequestStream();
            requestStream.Write(byteArray, 0, byteArray.Length);
            requestStream.Close();

            response = (HttpWebResponse)theRequest.GetResponse();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }


        [TestMethod]
        public void GetPriceWithOffer()
        {
            var itemOne = new Item { BarCodeId = 111 };//Apple barcode
            var itemTwo = new Item { BarCodeId = 222 };//Orange barcode
            var items = new List<Item> { itemOne, itemTwo };
            var priceCalculationResource = new PriceCalculationResource { Items = items };
            var content = JsonConvert.SerializeObject(priceCalculationResource);
            byte[] byteArray = Encoding.UTF8.GetBytes(content);

            HttpWebResponse response = null;

            var theRequest = (HttpWebRequest)WebRequest.Create("http://localhost:27387/api/getprice/Offer");
            theRequest.ContentType = "application/Json";
            theRequest.Method = WebRequestMethods.Http.Post;
            theRequest.ContentLength = content.Length;

            var requestStream = theRequest.GetRequestStream();
            requestStream.Write(byteArray, 0, byteArray.Length);
            requestStream.Close();

            response = (HttpWebResponse)theRequest.GetResponse();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }


    }
}
