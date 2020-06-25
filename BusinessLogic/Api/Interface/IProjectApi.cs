using BusinessLogic.Communication;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Api.Interface
{
    public interface IProjectApi
    {
         ListLandOfferServiceResponse GetLandOffers();

        void AddLandOffers(List<LandOffer> list);
        void DownloadLandOffers();
    }
}
