using System;
using BusinessLogic.Logic.Web;
using Core.Domain;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class GratkaWebSiteTest
    {
        //[TestMethod]
        //public void GettingDataFromGratkaWebSiteTest()
        //{
        //    string url = "https://gratka.pl/nieruchomosci/dzialka-budowlana-rzeszow-zaleze-ul-spichlerzowa/ob/21004923";
        //    // declaring & loading dom
        //    HtmlWeb web = new HtmlWeb();
        //    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        //    doc = web.Load(url);
        //    GratkaWebSite gratka = new GratkaWebSite();
        //    string water = gratka.GetWaterFromUrl(doc);
        //    string gas = gratka.GetGasFromUrl(doc);
        //    string electricity = gratka.GetElectricityFromUrl(doc);
        //    string sewers = gratka.GetSewersFromUrl(doc);

        //    Assert.IsFalse(string.IsNullOrEmpty(water));
        //    Assert.IsFalse(string.IsNullOrEmpty(gas));
        //    Assert.IsFalse(string.IsNullOrEmpty(electricity));
        //    Assert.IsFalse(string.IsNullOrEmpty(sewers));
        //}

        [TestMethod]
        public void GettingDataFromOtoDomWebSiteTest()
        {
            string url = "https://www.otodom.pl/pl/oferta/kawalerka-w-doskonalej-lokalizacji-pod-wynajem-ID4ex1P";
            // declaring & loading dom
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            string url2 = "https://www.otodom.pl/pl/oferta/piekny-apartament-w-centrum-rzeszowa-ID4c1cx";
            doc = web.Load(url);
            HtmlAgilityPack.HtmlDocument doc2 = new HtmlAgilityPack.HtmlDocument();
            doc2 = web.Load(url2);
            OtoDomWebSite otoDom = new OtoDomWebSite();
            decimal area2 = otoDom.GetAreaFromUrl(doc2);
            Assert.IsTrue(Math.Abs(140.8M - area2) < 0.00001M);
            decimal area = otoDom.GetAreaFromUrl(doc);
            decimal price = otoDom.GetPriceFromUrl(doc);
            Location location = otoDom.GetLocationFromUrl(doc);
            int? rooms = otoDom.GetRoomNumberFromUrl(doc);
            string description = otoDom.GetDescriptionFromUrl(doc);
            Assert.IsTrue(area == 65);
            Assert.IsTrue(price == 499900);
            Assert.IsTrue(location.N_latitude == 50.0099837);
            Assert.IsTrue(location.E_longitude == 22.0096804);
            Assert.IsTrue(description != null);
            Assert.IsTrue(rooms == 3);
            Assert.IsTrue(otoDom.GetBalcony(doc));
            Assert.IsTrue(otoDom.GetFloor(doc) == 2);
            Assert.IsTrue(otoDom.GetBuildingFloor(doc) == 4);
            Assert.IsTrue(!otoDom.GetGarden(doc));
            Assert.IsTrue(!otoDom.GetTarrace(doc));
            Assert.IsTrue(!otoDom.GetLoggia(doc));
            Assert.IsTrue(!otoDom.GetCellar(doc));
            Assert.IsTrue(otoDom.GetGarrage(doc));
            Assert.IsTrue(otoDom.GetParkingSpace(doc));
            Assert.IsTrue(otoDom.GetState(doc) == "do zamieszkania");
            Assert.IsTrue(!otoDom.GetLift(doc));
        }


        //[TestMethod]
        //public void DownloadFlats()
        //{
        //    OtoDomWebSite otoDom = new OtoDomWebSite();
        //    var countOffer = otoDom.CountOffers();
        //    Assert.IsTrue(countOffer > 0);

        //    var list = otoDom.GetFlatOffersFromWebsite();
        //    Assert.IsTrue(list.Count > 0);
        //}
    }
}
