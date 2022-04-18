using BusinessLogic.Interfaces;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;

namespace BusinessLogic.Repositories
{
    public class ModelRepository : BaseRepository, IModelRepository
    {
        public ModelRepository(ISession session) : base(session)
        { }

        public IReadOnlyList<Model> GetModel()
        {
            try
            {
                using (session)
                {
                    var models = session.Query<Model>().ToList();
                    session.Close();
                    return models;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Model SaveModel(Model model, int newOffers)
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    model.Date = DateTime.Today;
                    int count = (int)session.CreateCriteria<Model>()
                                        .SetProjection(Projections.Max("OfferCount"))
                                        .UniqueResult();
                    model.OfferCount = count + newOffers;
                    int id = (int)session.Save(model);
                    transaction.Commit();
                    var newModel = session.Query<Model>().Where(item => item.Id == id).FirstOrDefault() as Model;
                    return newModel;
                }

                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
        }
    }
}
