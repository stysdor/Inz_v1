using BusinessLogic.BaseSpecification;
using Core.Domain;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IModelRepository
    {
        IReadOnlyList<Model> GetModel();

        Model SaveModel(Model model, int newOffers);

    }
}
