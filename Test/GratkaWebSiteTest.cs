using System;
using BusinessLogic.Logic.Web;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class GratkaWebSiteTest
    {
        [TestMethod]
        public void GettingDataFromGratkaWebSiteTest()
        {
            string url = "https://gratka.pl/nieruchomosci/dzialka-budowlana-rzeszow-zaleze-ul-spichlerzowa/ob/21004923";
            // declaring & loading dom
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = web.Load(url);
            GratkaWebSite gratka = new GratkaWebSite();
            string water = gratka.GetWaterFromUrl(doc);
            string gas = gratka.GetGasFromUrl(doc);
            string electricity = gratka.GetElectricityFromUrl(doc);
            string sewers = gratka.GetSewersFromUrl(doc);

            Assert.IsFalse(string.IsNullOrEmpty(water));
            Assert.IsFalse(string.IsNullOrEmpty(gas));
            Assert.IsFalse(string.IsNullOrEmpty(electricity));
            Assert.IsFalse(string.IsNullOrEmpty(sewers));
        }
    }
}
