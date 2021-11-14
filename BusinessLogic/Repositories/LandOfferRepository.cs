using BusinessLogic.Logic.Web;
using Core.Domain;
using Core.Interfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Repositories
{
    public class LandOfferRepository: BaseRepository, ILandOfferRepository
    {
        public LandOfferRepository(global::NHibernate.ISession session) : base(session)
        {
        }

        public IReadOnlyList<LandOffer> GetLandOffers()
        {
            try
            {
                using (session)
                {
                    var landOffers = session.Query<LandOffer>().ToList();
                    session.Close();
                    return landOffers;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
         
        public bool AddLandOffers(IList<LandOffer> list)
        {
            using (session)
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
                        throw new Exception(e.Message);
                    }
                }
                return true;
            }
        }
        public bool DownloadLandOffers()
        {
            var list = new GratkaWebSite().GetLandOffersFromWebsite();
            return AddLandOffers(list);
        }

        public void UpdateLand(LandOffer land)
        {
            Console.WriteLine(land.Id);
            Console.WriteLine(land.OfferDateTime);

            using (session)
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    //var id = session.Get<Land>(land.Id);
                    //    if (id == null)
                    //    {
                    session.SaveOrUpdate(land);
                    session.Flush();
                    transaction.Commit();
                    session.Close();
                    //}
                    //else {                            
                    //    session.Update(land);
                    //    transaction.Commit();
                    //}
                }

                catch (Exception e) { 
                 
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
        }

    }
}
