using BusinessLogic.BaseSpecification;
using Core.Domain;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IFlatRepository
    {
        IReadOnlyList<Flat> GetAllFlats();
        IReadOnlyList<Flat> GetFlats(ISpecification<Flat> spec);
        int Count(ISpecification<Flat> spec);
        bool AddFlats(IList<Flat> list);
        int DownloadAndSaveFlatLinks();
        IReadOnlyList<FlatLink> GetFlatsLinksToGetFlatOffers(int count);
        bool DownloadFlatsFromLinks(IReadOnlyList<FlatLink> links);

    }
}
