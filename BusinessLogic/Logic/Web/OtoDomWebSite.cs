using Core.Domain;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic.Logic.Web
{
    public class OtoDomWebSite : FlatWebSite
    {
        public override string Url { get; set; } = "https://www.otodom.pl/pl/oferty/sprzedaz/mieszkanie/rzeszow";
        public override string OfferCount_xPath { get; set; } = "//span[contains(text(), 'Ogłoszenia:')]/following-sibling::span";
        public override string Url_xPath { get; set; } = "//a[@data-cy='listing-item-link']";
        public override string Area_xPath { get; set; } = "//div[contains(text(), 'Powierzchnia')]/parent::div/following-sibling::div/div";
        public override string Price_xPath { get; set; } = "//strong[@data-cy='adPageHeaderPrice']";
        public override string Description_xPath { get; set; } = "//div[@data-cy='adPageAdDescription']";
        public override string OfferDate_xPath { get; set; }


        public override int CountOffers()
        {
            int offerCount = 0;
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            doc = web.Load(Url);

            if (doc.DocumentNode.SelectNodes(OfferCount_xPath) != null)
            {
                string content_offerCount = doc.DocumentNode.SelectSingleNode(OfferCount_xPath).WriteContentTo();
                content_offerCount = Regex.Replace(content_offerCount, @"[^\d]", "");
                offerCount = Convert.ToInt32(content_offerCount);
            }

            return offerCount;
        }

        public override decimal GetAreaFromUrl(HtmlDocument doc)
        {
            decimal area = 0;
            if (doc.DocumentNode.SelectNodes(Area_xPath) != null)
            {
                HtmlNode node = doc.DocumentNode.SelectSingleNode(Area_xPath);
                string areaString = node.WriteContentTo();
                areaString = Regex.Replace(areaString, @"[^\d,.]", "");
                areaString = areaString.Replace(".", ",");
                area = areaString.Length > 0 ? Convert.ToDecimal(areaString) : 0;
            } 
            return area;
        }

        public override int? GetRoomNumberFromUrl(HtmlDocument doc)
        {
            return GetNumber("Liczba pokoi", doc);
        }

        public override int? GetConstuctionYear(HtmlDocument doc)
        {
            return GetNumber("Rok budowy", doc);
        }

        public override int? GetFloor(HtmlDocument doc)
        {
            string floorString = GetString("Piętro", doc);
            string[] values = floorString.Split('/');
            string value = values[0];

            if (value is null || value.Length == 0)
            {
                return null;
            }
            if (value == "poddasze")
            {
                return GetBuildingFloor(doc);
            }
            if (value == "parter")
            {
                return 0;
            }
            value = Regex.Replace(value, @"[^\d]", "");
            return  Convert.ToInt32(value);         
        }

        public override int? GetBuildingFloor(HtmlDocument doc)
        {
            string floorString = GetString("Piętro", doc);
            string[] values = floorString.Split('/');
            if (values.Length < 2) return null;
            string value = values[1];
            value = Regex.Replace(value, @"[^\d]", "");
            if (value is null || value.Length == 0) return null;
            return Convert.ToInt32(value);
        }

        public override string GetTypeOfBuilding(HtmlDocument doc) 
        {
            return GetString("Rodzaj zabudowy", doc);
        }

        public override bool GetBalcony(HtmlDocument doc)
        {
            int start = doc.ParsedText.IndexOf("\"features\"");
            int end = doc.ParsedText.IndexOf("\"featuresWithoutCategory\"");
            return (start > 0 && end > 0 && end > start) && doc.ParsedText.Substring(start, end - start).Contains("balkon");
        }

        public override bool GetLoggia(HtmlDocument doc)
        {
            int start = doc.ParsedText.IndexOf("\"features\"");
            int end = doc.ParsedText.IndexOf("\"featuresWithoutCategory\"");
            return (start > 0 && end > 0 && end > start) && doc.ParsedText.Substring(start, end - start).Contains("loggia");
        }

        public override bool GetTarrace(HtmlDocument doc)
        {
            int start = doc.ParsedText.IndexOf("\"features\"");
            int end = doc.ParsedText.IndexOf("\"featuresWithoutCategory\"");
            return (start > 0 && end > 0 && end > start) && doc.ParsedText.Substring(start, end - start).Contains("taras");
        }

        public override bool GetGarden(HtmlDocument doc)
        {
            int start = doc.ParsedText.IndexOf("\"features\"");
            int end = doc.ParsedText.IndexOf("\"featuresWithoutCategory\"");
            return (start > 0 && end > 0 && end > start) && doc.ParsedText.Substring(start, end - start).Contains("ogród");
        }

        public override bool GetCellar(HtmlDocument doc)
        {
            int start = doc.ParsedText.IndexOf("\"features\"");
            int end = doc.ParsedText.IndexOf("\"featuresWithoutCategory\"");
            return (start > 0 && end > 0 && end > start) && doc.ParsedText.Substring(start, end - start).Contains("piwnica");
        }

        public override bool GetGarrage(HtmlDocument doc)
        {
            int start = doc.ParsedText.IndexOf("\"features\"");
            int end = doc.ParsedText.IndexOf("\"featuresWithoutCategory\"");
            return (start > 0 && end > 0 && end > start) && doc.ParsedText.Substring(start, end - start).Contains("garaż");
        }

        public override bool GetParkingSpace(HtmlDocument doc)
        {
            int start = doc.ParsedText.IndexOf("\"features\"");
            int end = doc.ParsedText.IndexOf("\"featuresWithoutCategory\"");
            return (start > 0 && end > 0 && end > start) && doc.ParsedText.Substring(start, end - start).Contains("miejsce parkingowe");
        }

        public override bool GetLift(HtmlDocument doc)
        {
            int start = doc.ParsedText.IndexOf("\"features\"");
            int end = doc.ParsedText.IndexOf("\"featuresWithoutCategory\"");
            return (start > 0 && end > 0 && end > start) && doc.ParsedText.Substring(start, end - start).Contains("winda");
        }

        public override string GetState(HtmlDocument doc)
        {
            return GetString("Stan wykończenia", doc);
        }

        public override string GetKitchen(HtmlDocument doc)
        {
            return GetString("Kuchnia", doc);
        }

        public override string GetMarket(HtmlDocument doc)
        {
            return GetString("Rynek", doc);
        }
       
        private int? GetNumber(string contains, HtmlDocument doc)
        {
            string xPath = "//div[contains(text(), '" + contains +"')]/parent::div/following-sibling::div/div";
            int number = 0;
            if (doc.DocumentNode.SelectNodes(xPath) != null)
            {
                HtmlNode node = doc.DocumentNode.SelectSingleNode(xPath);
                string numberString = node.WriteContentTo();
                if (numberString is null) numberString = "0";
                numberString = Regex.Replace(numberString, @"[^\d]", "");
                if (numberString.Length == 0) return null;
                number = Convert.ToInt32(numberString);
            }
            return number;
        }

        private string GetString(string contains, HtmlDocument doc)
        { 
            string xPath = "//div[contains(text(), '" + contains + "')]/parent::div/following-sibling::div/div";
            string value = "";
            if (doc.DocumentNode.SelectNodes(xPath) != null)
            {

                HtmlNode node = doc.DocumentNode.SelectSingleNode(xPath);
                value = node.WriteContentTo();
            }
            return value;
        }

        public override string GetDescriptionFromUrl(HtmlDocument doc)
        {
            string descriprion = "";
            if (doc.DocumentNode.SelectNodes(Description_xPath) != null)
            {
                descriprion = doc.DocumentNode.SelectSingleNode(Description_xPath).WriteContentTo();

            }
            return descriprion;
        }

        public override Location GetLocationFromUrl(HtmlDocument doc)
        {
            Location location = new Location();
            double x, y;
            string document = doc.ParsedText;
            int latitudeIndex = document.IndexOf("latitude");
            if (latitudeIndex >=0 )
            {
                string yString = document.Substring(latitudeIndex + "latitude :".Length, 10);
                int longitudeIndex = document.IndexOf("longitude");
                string xString = document.Substring(longitudeIndex + "longitude :".Length, 10);
                xString = Regex.Replace(xString, @"[^\d.]", "");
                yString = Regex.Replace(yString, @"[^\d.]", "");
                xString = xString.Replace(".", ",");
                yString = yString.Replace(".", ",");
                x = Convert.ToDouble(xString);
                y = Convert.ToDouble(yString);
                location.E_longitude = x;
                location.N_latitude = y;
            }
            return location;
        }

        public override decimal GetPriceFromUrl(HtmlDocument doc)
        {
            decimal price = 0;
            if (doc.DocumentNode.SelectNodes(Price_xPath) != null)
            {
                string priceString = doc.DocumentNode.SelectSingleNode(Price_xPath).WriteContentTo();
                priceString = Regex.Replace(priceString, @"[^\d.,]", "");
                priceString = priceString.Replace(".", ",");
                price = priceString.Length == 0 ? 0 : Convert.ToDecimal(priceString);
            }
            return price;
        }

        public override List<string> TakeOfferLinks()
        {
            // declaring & loading dom
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            doc = web.Load(Url);

            //create new list of url string
            offersLinks = new List<string>();

            //count number of offers
            int offerCount = this.CountOffers();

            //load and save links to offers from every page
            string pageUrl = Url;
            int page = 1;

            while (offersLinks.Count < offerCount)
            {
                doc = web.Load(pageUrl);
                if (doc.DocumentNode.SelectNodes(Url_xPath) != null)
                {
                    foreach (HtmlNode link in doc.DocumentNode.SelectNodes(Url_xPath))
                    {
                        HtmlAttribute dataUdi = link.Attributes["href"];
                        offersLinks.Add("https://otodom.pl" + dataUdi.Value);
                        // Console.WriteLine(dataUdi.Value);
                    }
                }
                else
                {
                    Console.WriteLine("No bad links found");
                    break;
                }

                if (page >= 2)
                    pageUrl = Regex.Replace(pageUrl, $"{page}", "");
                page++;
                if (page == 2)
                    pageUrl = pageUrl + "?page=" + page;
                else
                    pageUrl = pageUrl + page;
            }

            return offersLinks;
        }
    }
}
