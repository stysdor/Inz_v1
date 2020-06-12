using BusinessLogic.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Api.Interface
{
    public interface IProjectApi
    {
        void TestNHibernate();
        ListLandOfferServiceResponse GetLandOffers();
    }
}
