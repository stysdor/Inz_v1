using BusinessLogic.Logic.Interface;
using OfferService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class OfferDownloader: IOfferDownloader
    {
        public void DownloadOffers()
        {
            WebSite gratka = new GratkaWebSite();
            gratka.GetLandOffersFromWebsite();
        }
    }
}
