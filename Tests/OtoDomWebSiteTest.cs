using BusinessLogic.Logic.Web;
using Core.Domain;
using HtmlAgilityPack;
using System.IO;
using System.Reflection;
using Xunit;

namespace Tests
{
    public class OtoDomWebSiteTest
    {
        OtoDomWebSite otoDom = new OtoDomWebSite();
        HtmlDocument doc;

        [Fact]
        public void GetAreaFromUrlReturnCorrectAreaValue()
        {
            doc = this.prepareWebSite();
            decimal area = otoDom.GetAreaFromUrl(doc);
            Assert.True(32.23M.Equals(area));
        }

        [Fact]
        public void GetPriceFromUrlReturnCorrectPriceValue()
        {
            doc = this.prepareWebSite();
            decimal price = otoDom.GetPriceFromUrl(doc);
            Assert.True(299000M.Equals(price));
        }

        [Fact]
        public void GetRoomNumberFormUrlReturnCorrectRoomNumberValue()
        {
            doc = this.prepareWebSite();
            int? value = otoDom.GetRoomNumberFromUrl(doc);
            Assert.Equal(1, value);
        }
        [Fact]
        public void GetFloorRturnCorrectFloorValue()
        {
            doc = this.prepareWebSite();
            int? value = otoDom.GetFloor(doc);
            Assert.Equal(1, value);
        }

        [Fact]
        public void GetBuildingFloorReturnCorrectFloorInBuildingValue()
        {
            doc = this.prepareWebSite();
            int? value = otoDom.GetBuildingFloor(doc);
            Assert.Equal(2, value);
        }

        [Fact]
        public void GetMarketReturnCorrectMarketValue()
        {
            doc = this.prepareWebSite();
            string value = otoDom.GetMarket(doc);
            Assert.Equal("wtórny", value);
        }

        [Fact]
        public void GetConstructionYearReturnCorrectConstructionYearValue()
        {
            doc = this.prepareWebSite();
            int? value = otoDom.GetConstuctionYear(doc);
            Assert.Equal(1977, value);
        }

        [Fact]
        public void GetLiftReturnCorrectIsLiftValue()
        {
            doc = this.prepareWebSite();
            bool value = otoDom.GetLift(doc);
            Assert.False(value);
        }

        [Fact]
        public void GetBalconyReturnCorrectIsBalconyValue()
        {
            doc = this.prepareWebSite();
            bool value = otoDom.GetBalcony(doc);
            Assert.False(value);
        }

        [Fact]
        public void GetGardenReturnCorrectIsGardenValue()
        {
            doc = this.prepareWebSite();
            bool value = otoDom.GetGarden(doc);
            Assert.False(value);
        }

        [Fact]
        public void GetTarraceReturnCorrectIsTarraceValue()
        {
            doc = this.prepareWebSite();
            bool value = otoDom.GetTarrace(doc);
            Assert.False(value);
        }

        [Fact]
        public void GetLoggiaReturnCorrectIsLoggiaValue()
        {
            doc = this.prepareWebSite();
            bool value = otoDom.GetLoggia(doc);
            Assert.False(value);
        }

        [Fact]
        public void GetParkingSpaceReturnCorrectIsParkingSpaceValue()
        {
            doc = this.prepareWebSite();
            bool value = otoDom.GetParkingSpace(doc);
            Assert.False(value);
        }

        [Fact]
        public void GetGarageReturnCorrectIsGarageSpaceValue()
        {
            doc = this.prepareWebSite();
            bool value = otoDom.GetGarrage(doc);
            Assert.False(value);
        }

        [Fact]
        public void GetLocationReturnCorrectLocationValue()
        {
            doc = this.prepareWebSite();
            Location value = otoDom.GetLocationFromUrl(doc);
            Assert.True(value.N_latitude.Equals(50.0237894));
            Assert.True(value.E_longitude.Equals(21.9820505));
        }


        [Fact]
        public void GetStateReturnCorrectStateValue()
        {
            doc = this.prepareWebSite();
            string value = otoDom.GetState(doc);
            Assert.Equal("do zamieszkania", value);
        }

        [Fact]
        public void GetCellarReturnCorrectIsCellarValue()
        {
            doc = this.prepareWebSite();
            bool value = otoDom.GetCellar(doc);
            Assert.True(value);
        }

        [Fact]
        public void GetAreaFromUrlReturn_0_whenNoArea()
        {
            doc = new HtmlDocument();
            decimal value = otoDom.GetAreaFromUrl(doc);
            Assert.True(value == 0);
        }

        [Fact]
        public void GetPriceFromUrlReturn_0_whenNoPrice()
        {
            doc = new HtmlDocument();
            decimal value = otoDom.GetPriceFromUrl(doc);
            Assert.True(value == 0);
        }

        [Fact]
        public void CountOffersReturnsValue()
        {
            int value = otoDom.CountOffers();
            Assert.True(value > 0);
        }

        private HtmlDocument prepareWebSite() {
            var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "website.txt");
            string mockWebsite = File.ReadAllText(filePath);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(mockWebsite);
            return doc;
        }
    }
}
