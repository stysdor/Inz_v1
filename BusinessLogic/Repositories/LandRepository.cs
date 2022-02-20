using BusinessLogic.NHibernate;
using Core.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Repositories
{
    public class LandRepository : BaseRepository
    {
        public LandRepository(global::NHibernate.ISession session) : base(session)
        {
        }
        public void UpdateLand(LandOffer land)
        {
            Console.WriteLine(land.Id);

            using (session)
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    //var id = session.Get<Land>(land.Id);
                    //    if (id == null)
                    //    {
                    session.Update(land);
                    transaction.Commit();
                    //}
                    //else {                            
                    //    session.Update(land);
                    //    transaction.Commit();
                    //}
                }

                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
        }

        public IReadOnlyList<LandOffer> GetLands()
        {
            try
            {
                using (session)
                {
                    var lands = session.Query<LandOffer>().ToList();
                    return lands;
                }
            }
            catch 
            {
                throw new Exception("You can not get lands right now.");
            }
        }
    }
}
