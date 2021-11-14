using Core.Domain;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic.Web
{
    public abstract class WebSite
    {
        private string url;
        private string offerCount_xPath;
        private string url_xPath;
        private string area_xPath;
        private string price_xPath;
        private string electricity_xPath;
        private string gas_xPath;
        private string water_xPath;
        private string sewers_xPath;
        private string type_xPath;
        private string road_xPath;
        private string location_xPath;
        private string description_xPath;
        private string offerDate_xPath;

        public virtual string Url
        {
            get { return url; }
        }
        public virtual string OfferCount_xPath
        {
            get { return offerCount_xPath; }
        }
        public virtual string Url_xPath
        {
            get { return url_xPath; }
        }
        public virtual string Area_xPath
        {
            get { return area_xPath; }
        }
        public virtual string Price_xPath
        {
            get { return price_xPath; }
        }
        public virtual string Electricity_xPath
        {
            get { return electricity_xPath; }
        }
        public virtual string Gas_xPath
        {
            get { return gas_xPath; }
        }
        public virtual string Water_xPath
        {
            get { return water_xPath; }
        }
        public virtual string Sewers_xPath
        {
            get { return sewers_xPath; }
        }
        public virtual string Type_xPath
        {
            get { return type_xPath; }
        }
        public virtual string Road_xPath
        {
            get { return road_xPath; }
        }
        public virtual string Location_xPath
        {
            get { return location_xPath; }
        }
        public virtual string Description_xPath
        {
            get { return description_xPath; }
        }
        public virtual string OfferDate_xPath
        {
            get { return offerDate_xPath; }
        }

        public List<string> offersLinks;
        public List<LandOffer> lands;

        public abstract int CountOffers();

        public abstract List<string> TakeOfferLinks();

        public abstract decimal GetAreaFromUrl(HtmlAgilityPack.HtmlDocument doc);
        public abstract decimal GetPriceFromUrl(HtmlAgilityPack.HtmlDocument doc);
        public abstract string GetElectricityFromUrl(HtmlAgilityPack.HtmlDocument doc);
        public abstract string GetGasFromUrl(HtmlAgilityPack.HtmlDocument doc);
        public abstract string GetWaterFromUrl(HtmlAgilityPack.HtmlDocument doc);
        public abstract string GetSewersFromUrl(HtmlAgilityPack.HtmlDocument doc);
        public abstract string GetTypeFromUrl(HtmlAgilityPack.HtmlDocument doc);
        public abstract string GetRoadFromUrl(HtmlAgilityPack.HtmlDocument doc);
        public abstract string GetDescriptionFromUrl(HtmlAgilityPack.HtmlDocument doc);
        public abstract Location GetLocationFromUrl(HtmlAgilityPack.HtmlDocument doc);

        public List<LandOffer> GetLandOffersFromWebsite()
        {
             return this.GetLands(this.TakeOfferLinks());
        }

        public List<LandOffer> GetLands(List<string> urlList)
        {
            // declaring & loading dom
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

            List<LandOffer> lands = new List<LandOffer>();

            foreach (string url in urlList)
            {
                doc = web.Load(url);
                LandOffer land = new LandOffer
                {
                    Area = GetAreaFromUrl(doc),
                    Price = GetPriceFromUrl(doc),
                    Electricity = GetElectricityFromUrl(doc),
                    Gas = GetGasFromUrl(doc),
                    Water = GetWaterFromUrl(doc),
                    Sewers = GetSewersFromUrl(doc),
                    Type = GetTypeFromUrl(doc),
                    Location = GetLocationFromUrl(doc),
                    Road = GetRoadFromUrl(doc),
                    Description = GetDescriptionFromUrl(doc)
                };
                lands.Add(land);

            }
            return lands;
        }
    }
}

