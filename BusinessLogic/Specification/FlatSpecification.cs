﻿using BusinessLogic.BaseSpecification;
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

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
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
                        AddOrderBy(p => p.Price);
                        break;
                }
            }

            if (productParams.IsAccepted.HasValue) {
                AddCriteria(p => p.IsAccepted == productParams.IsAccepted);
            }

        }
    }
}
