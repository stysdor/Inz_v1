using BusinessLogic.BaseSpecification;
using BusinessLogic.Interfaces;
using BusinessLogic.Logic.Web;
using BusinessLogic.Specification;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace BusinessLogic.Repositories
{
    public class FlatRepository : BaseRepository, IFlatRepository
    {
        public FlatRepository(ISession session) : base(session)
        {}

        public IReadOnlyList<Flat> GetAllFlats()
        {
            try
            {
                using (session)
                {
                    var flats = session.Query<Flat>().ToList();
                    session.Close();
                    return flats;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IReadOnlyList<Flat> GetFlats(ISpecification<Flat> spec)
        {
            try
            {
                using (session)
                {
                    var flats = ApplySpecification(spec).ToList();
                    session.Close();
                    return flats;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool AddFlats(IList<Flat> list)
        {
            foreach (var item in list)
            {
                using (var transaction = session.BeginTransaction())
                {
                    try

                    {
                        bool exist = false;
                        if (!(item.Id > 0))
                        {
                             exist = session.Query<Flat>()
                                            .Where(x =>
                                                    (x.Area.Equals(item.Area)) &&
                                                    (x.Price.Equals(item.Price)) &&
                                                    //(x.Location.E_longitude == item.Location.E_longitude) &&
                                                    //(x.Location.N_latitude == item.Location.N_latitude) &&
                                                    x.Floor == item.Floor &&
                                                    x.FloorInBuilding == item.FloorInBuilding &&
                                                    (x.RoomNumber == item.RoomNumber) &&
                                                    (x.IsBalcony == item.IsBalcony) &&
                                                    (x.IsGarden == item.IsGarden) &&
                                                    (x.IsTarrace == item.IsTarrace) &&
                                                    (x.IsLoggia == item.IsLoggia) &&
                                                    (x.IsGarage == item.IsGarage) &&
                                                    (x.IsParkingSpace == item.IsParkingSpace) &&
                                                    (x.Market.Equals(item.Market)))
                                            .ToList().Count > 0;
                        }
                        if (!exist)
                        {
                            session.SaveOrUpdate(item);
                            transaction.Commit();
                        }

                        else transaction.Rollback();
                    }

                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new Exception(e.Message);
                    }
                }
            }
            return true;
        }

        private IReadOnlyList<FlatLink> DownloadFlatLinks()
        {
            var list = new OtoDomWebSite().TakeOfferLinks();
            var links = new List<FlatLink>();
            list.ForEach(item => {
                links.Add(new FlatLink(item));
            });
            return links;
        }

        public int DownloadAndSaveFlatLinks() {
            IReadOnlyList<FlatLink> links = DownloadFlatLinks();
            using (session)
            {
                foreach (var item in links)
                {
                    var exist = session.Query<FlatLink>()
                                    .Where(x => x.UrlString == item.UrlString)
                                    .ToList()
                                    .Count > 0;
                    using (var transaction = session.BeginTransaction())
                    {
                        try
                        {
                            {
                                if (exist) transaction.Rollback();
                                else
                                {
                                    session.SaveOrUpdate(item);
                                    transaction.Commit();
                                }
                            }

                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            throw new Exception(e.Message);
                        }
                    }
                }
            }
            return links.Count;
        }
    
        public IReadOnlyList<FlatLink> GetFlatsLinksToGetFlatOffers(int count) {
            
            try
            {
                    List<int> listId = session.Query<Flat>()
                        .Where(f => f.FlatLink != null )
                         .Select(f => f.FlatLink.Id)
                        .ToList();
                    var links = session.Query<FlatLink>()
                        .Where(x => !listId.Contains(x.Id))
                        .Take(count)
                        .ToList();
                    return links;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DownloadFlatsFromLinks(IReadOnlyList<FlatLink> links)
        {
            var list = new OtoDomWebSite().GetFlats(links);
            list.ForEach(item => { item.IsAccepted = false; item.OfferDateTime = DateTime.Now;});
            return AddFlats(list);
        }

        public int Count(ISpecification<Flat> spec)
        {
           var results = ApplySpecification(spec).Count();
           return results;
        }

        private IQueryable<Flat> ApplySpecification(ISpecification<Flat> spec)
        {
            return SpecificationEvaluator<Flat>.GetQuery(session.Query<Flat>().AsQueryable(), spec); ;
        }
    }
}
