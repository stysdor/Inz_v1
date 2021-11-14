using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ILandOfferRepository
    {
        IReadOnlyList<LandOffer> GetLandOffers();
        bool AddLandOffers(IList<LandOffer> list);
        bool DownloadLandOffers();
        void UpdateLand(LandOffer land);
    }
}
