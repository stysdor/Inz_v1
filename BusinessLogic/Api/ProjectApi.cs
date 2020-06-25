using BusinessLogic.Api.Interface;
using BusinessLogic.Communication;
using BusinessLogic.Logic.Web;
using BusinessLogic.ModelDTO;
using BusinessLogic.NHibernate;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Api
{
    public class ProjectApi: BaseApi, IProjectApi
    {
        public ListLandOfferServiceResponse GetLandOffers()
        {
            try
            {
                using (var session = NHibernateBase.Session)
                {
                    var landOffers = session.Query<LandOffer>()
                        .Select(x =>
                        new LandOfferDTO
                        {
                            Area = x.Area,
                            Price = x.Price,
                            Electricity = x.Electricity,
                            Water = x.Water,
                            Gas = x.Gas,
                            Sewers = x.Sewers,
                            Type = x.Type,
                            Description = x.Description,
                            Road = x.Road
                        }).ToList();
                    return new ListLandOfferServiceResponse()
                    {
                        Data = landOffers
                    };
                }
            }
            catch (Exception e)
            {
                return new ListLandOfferServiceResponse()
                {
                    Errors = e.StackTrace + "" + e.Message,
                    Success = false
                };
            }
        }

        public void AddLandOffers(List<LandOffer> list)
        {
            using (var session = NHibernateBase.Session)
            {
                foreach (var item in list)
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        {
                            item.OfferDateTime = DateTime.Now;
                            session.SaveOrUpdate(item);
                        }
                    transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
        public void DownloadLandOffers()
        {
            var list = new GratkaWebSite().GetLandOffersFromWebsite();
            AddLandOffers(list);
        }

    }
}
