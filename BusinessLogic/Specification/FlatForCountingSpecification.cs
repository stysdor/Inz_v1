using BusinessLogic.BaseSpecification;
using Core.Domain;

namespace BusinessLogic.Specification
{
    public class FlatForCountingSpecification : BaseSpecification<Flat>
    {
        public FlatForCountingSpecification(SpecParams productParams)
          : base()
        {
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

