using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OfferService.Model
{
    public class GratkaWebSite: WebSite
    {
        //Url of the website
        public override string Url => "https://gratka.pl/nieruchomosci/dzialki-grunty/rzeszow/sprzedaz";
        //xPaths to get informations from the website
        public override string OfferCount_xPath => "//span[@class='content__offerCount']";
        public override string Url_xPath => "//article[@class='teaserEstate ']";
        public override string Area_xPath => "//li[span[contains(text(),'Powierzchnia działki w m2')]]/b";
        public override string Price_xPath => "//span[@class='priceInfo__value']";
        public override string Location_xPath => "//div[@id='item-map']/following-sibling::script[1]";
        public override string Electricity_xPath => "//li[span[contains(text(),'Prąd')]]/b";
        public override string Sewers_xPath => "//li[span[contains(text(),'Kanalizacja')]]/b";
        public override string Water_xPath => "//li[span[contains(text(),'Woda')]]/b";
        public override string Gas_xPath => "//li[span[contains(text(),'Gaz')]]/b";
        public override string Road_xPath => "//li[span[contains(text(),'Droga dojazdowa')]]/b";
        public override string Description_xPath => "//div[@class='description__rolled ql-container']";

        public override int CountOffers()
        {
            int offerCount = 0;
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = web.Load(Url);

            if (doc.DocumentNode.SelectNodes(OfferCount_xPath) != null)
            {
                string content_offerCount = doc.DocumentNode.SelectSingleNode(OfferCount_xPath).WriteContentTo();
                content_offerCount = Regex.Replace(content_offerCount, @"[^\d]", "");
                offerCount = Convert.ToInt32(content_offerCount);
                // Console.WriteLine("Liczba ofert to: {0}", offerCount);
            }

            return offerCount;
        }

        public override List<string> TakeOfferLinks()
        {
            // declaring & loading dom
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
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
                        HtmlAttribute dataUdi = link.Attributes["data-href"];
                        offersLinks.Add(dataUdi.Value);
                        // Console.WriteLine(dataUdi.Value);
                    }
                }
                else
                {
                    Console.WriteLine("No bad links found");
                    break;
                }

                //change urlPage for the next page 
                if (page >= 2)
                    pageUrl = Regex.Replace(pageUrl, $"{page}", "");
                page++;
                if (page == 2)
                    pageUrl = pageUrl + "?page=" + page;
                else
                    pageUrl = pageUrl + page;
                //Console.WriteLine(pageUrl);
                // Console.WriteLine("Liczba linkow to : {0}", offersLinks.Count);
            }

            return offersLinks;
        }

        public override decimal GetAreaFromUrl(HtmlDocument doc)
        {
            decimal area = 0;
            if (doc.DocumentNode.SelectNodes(Area_xPath) != null)
            {
                HtmlNode node = doc.DocumentNode.SelectSingleNode(Area_xPath);
                string areaString = node.WriteContentTo();
                areaString = Regex.Replace(areaString, @"[^\d]", "");
                areaString = areaString.Remove(areaString.Length - 1, 1);
                area = Convert.ToDecimal(areaString);
            }
            else
            {
                Console.WriteLine("No nodes found");
            }
            return area;
        }

        public override decimal GetPriceFromUrl(HtmlDocument doc)
        {
            decimal price = 0;
            if (doc.DocumentNode.SelectNodes(Price_xPath) != null)
            {
                string priceString = doc.DocumentNode.SelectSingleNode(Price_xPath).WriteContentTo();
                priceString = Regex.Replace(priceString, @"[^\d]", "");
                price = Convert.ToDecimal(priceString);
            }
            else
            {
                Console.WriteLine("No nodes found");
            }
            return price;
        }

        public override Location GetLocationFromUrl(HtmlDocument doc)
        {
            Location location = new Location();
            double x, y;
            if (doc.DocumentNode.SelectNodes(Location_xPath) != null)
            {
                string scriptContent = doc.DocumentNode.SelectSingleNode(Location_xPath).WriteContentTo();
                int indexY = scriptContent.IndexOf("geograficzna-y");
                string yString = scriptContent.Substring(indexY + 16, 7);
                int indexX = scriptContent.IndexOf("geograficzna-x");
                string xString = scriptContent.Substring(indexX + 16, 7);
                xString = Regex.Replace(xString, @"[^\d.]", "");
                yString = Regex.Replace(yString, @"[^\d.]", "");
                x = Convert.ToDouble(xString);
                y = Convert.ToDouble(yString);
                location.E_longitude = x;
                location.N_latitude = y;
            }
            else
            {
                Console.WriteLine("No nodes found");
            }
            return location;
        }

        public override string GetElectricityFromUrl(HtmlDocument doc)
        {
            string electricityString = "";
            if (doc.DocumentNode.SelectNodes(Electricity_xPath) != null)
            {
                electricityString = doc.DocumentNode.SelectSingleNode(Electricity_xPath).WriteContentTo();

            }
            else
            {
                Console.WriteLine("No nodes found");
            }
            return electricityString;
        }

        public override string GetGasFromUrl(HtmlDocument doc)
        {
            string gasString = "";
            if (doc.DocumentNode.SelectNodes(Gas_xPath) != null)
            {
                gasString = doc.DocumentNode.SelectSingleNode(Gas_xPath).WriteContentTo();

            }
            else
            {
                Console.WriteLine("No nodes found");
            }
            return gasString;
        }

        public override string GetWaterFromUrl(HtmlDocument doc)
        {
            string waterString = "";
            if (doc.DocumentNode.SelectNodes(Water_xPath) != null)
            {
                waterString = doc.DocumentNode.SelectSingleNode(Water_xPath).WriteContentTo();

            }
            else
            {
                Console.WriteLine("No nodes found");
            }
            return waterString;
        }

        public override string GetSewersFromUrl(HtmlDocument doc)
        {
            string sewersString = "";
            if (doc.DocumentNode.SelectNodes(Sewers_xPath) != null)
            {
                sewersString = doc.DocumentNode.SelectSingleNode(Sewers_xPath).WriteContentTo();

            }
            else
            {
                Console.WriteLine("No nodes found");
            }
            return sewersString;
        }

        public override string GetRoadFromUrl(HtmlDocument doc)
        {
            string roadString = "";
            if (doc.DocumentNode.SelectNodes(Road_xPath) != null)
            {
                roadString = doc.DocumentNode.SelectSingleNode(Sewers_xPath).WriteContentTo();

            }
            else
            {
                Console.WriteLine("No nodes found");
            }
            return roadString;
        }

        public override string GetDescriptionFromUrl(HtmlDocument doc)
        {
            string descriprion = "";
            if (doc.DocumentNode.SelectNodes(Description_xPath) != null)
            {
                descriprion = doc.DocumentNode.SelectSingleNode(Description_xPath).WriteContentTo();

            }
            else
            {
                Console.WriteLine("No nodes found");
            }
            return descriprion;
        }

        #region not implemented

        public override string GetTypeFromUrl(HtmlDocument doc)
        {
            //throw new NotImplementedException();
            return null;
        }

        #endregion
    }
}

