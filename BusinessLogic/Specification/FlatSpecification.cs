using BusinessLogic.BaseSpecification;
using Core.Domain;

namespace BusinessLogic.Specification
{
    public class FlatSpecification : BaseSpecification<Flat>
    {
        public FlatSpecification(SpecParams productParams)
          : base()
        {
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1),
                productParams.PageSize);


            switch (productParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                case "areaAsc":
                    AddOrderBy(p => p.Area);
                    break;
                case "areaDesc":
                    AddOrderByDescending(p => p.Area);
                    break;
                case "offerData":
                    AddOrderByDescending(p => p.OfferDateTime);
                    break;
                default:
                    AddOrderByDescending(p => p.OfferDateTime);
                    break;
            }
            

            if (productParams.IsAccepted.HasValue && productParams.IsUsedInModel.HasValue)
            {
                AddCriteria(p => p.IsUsedInModel == productParams.IsUsedInModel && p.IsAccepted == productParams.IsAccepted);
            }
            else
            {
                if (productParams.IsAccepted.HasValue)
                {
                    AddCriteria(p => p.IsAccepted == productParams.IsAccepted);
                }
                if (productParams.IsUsedInModel.HasValue)
                {
                    AddCriteria(p => p.IsUsedInModel == productParams.IsUsedInModel);
                }
            }

        }
    }
}
