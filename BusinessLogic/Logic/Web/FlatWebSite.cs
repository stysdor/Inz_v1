using Core.Domain;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Logic.Web
{
    public abstract class FlatWebSite
    {
        public virtual string Url { get; set; }
        public virtual string OfferCount_xPath { get; set; }
        public virtual string Url_xPath { get; set; }
        public virtual string Area_xPath { get; set; }
        public virtual string Price_xPath { get; set; }
        public virtual string Location_xPath { get; set; }
        public virtual string Description_xPath { get; set; }
        public virtual string OfferDate_xPath { get; set; }

        public List<string> offersLinks;
        public List<Flat> flatOffers;

        public abstract int CountOffers();
        public abstract List<string> TakeOfferLinks();
        public abstract decimal GetAreaFromUrl(HtmlDocument doc);
        public abstract decimal GetPriceFromUrl(HtmlDocument doc);
        public abstract string GetDescriptionFromUrl(HtmlDocument doc);
        public abstract Location GetLocationFromUrl(HtmlDocument doc);
        public abstract int? GetConstuctionYear(HtmlDocument doc);
        public abstract int? GetRoomNumberFromUrl(HtmlDocument doc);
        public abstract int? GetFloor(HtmlDocument doc);
        public abstract int? GetBuildingFloor(HtmlDocument doc);
        public abstract string GetTypeOfBuilding(HtmlDocument doc);
        public abstract bool GetBalcony(HtmlDocument doc);
        public abstract bool GetLoggia(HtmlDocument doc);
        public abstract bool GetTarrace(HtmlDocument doc);
        public abstract bool GetGarden(HtmlDocument doc);
        public abstract bool GetCellar(HtmlDocument doc);
        public abstract bool GetGarrage(HtmlDocument doc);
        public abstract bool GetParkingSpace(HtmlDocument doc);
        public abstract string GetState(HtmlDocument doc);
        public abstract string GetKitchen(HtmlDocument doc);
        public abstract string GetMarket(HtmlDocument doc);
        public abstract bool GetLift(HtmlDocument doc);


        public List<Flat> GetFlats(IReadOnlyList<FlatLink> urlList)
        {
            // declaring & loading dom
           HtmlWeb web = new HtmlWeb();
           HtmlDocument doc = new HtmlDocument();

            List<Flat> flats = new List<Flat>();

            foreach (FlatLink url in urlList)
            {
                doc = web.Load(url.UrlString);
                Flat flat = new Flat
                {
                    Area = GetAreaFromUrl(doc)
                };
                if (flat.Area < 5 || flat.Area > 500) continue;
                flat.Price = GetPriceFromUrl(doc);
                if (flat.Price < 10000) continue;
                flat.Location = GetLocationFromUrl(doc);
                if (flat.Location.E_longitude > 180 || flat.Location.E_longitude < 0 || flat.Location.N_latitude > 90 || flat.Location.N_latitude < 0) continue;
                flat.Market = GetMarket(doc);
                if (string.IsNullOrEmpty(flat.Market)) continue;
                flat.Floor = GetFloor(doc);
                if (!flat.Floor.HasValue) continue;
                flat.FloorInBuilding = GetBuildingFloor(doc);
                if (!flat.FloorInBuilding.HasValue) continue;
                flat.RoomNumber = GetRoomNumberFromUrl(doc);
                if (!flat.RoomNumber.HasValue) continue;
                flat.Description = GetDescriptionFromUrl(doc);
                flat.ConstructionYear = GetConstuctionYear(doc);
                flat.State = GetState(doc);
                flat.IsBalcony = GetBalcony(doc);
                flat.IsCellar = GetCellar(doc);
                flat.IsGarage = GetGarrage(doc);
                flat.IsGarden = GetGarden(doc);
                flat.IsLift = GetLift(doc);
                flat.IsLoggia = GetLoggia(doc);
                flat.IsParkingSpace = GetParkingSpace(doc);
                flat.IsTarrace = GetTarrace(doc);
                flat.Kitchen = GetKitchen(doc);
                flat.TypeOfBuilding = GetTypeOfBuilding(doc);
                flat.FlatLink = url;
                flat.OfferDateTime = DateTime.Today;
                flat.IsAccepted = false;

                flats.Add(flat);

            }
            return flats;
        }
    }
}
