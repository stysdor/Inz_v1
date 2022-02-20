using BusinessLogic.BaseSpecification;
using Core.Domain;

namespace BusinessLogic.Specification
{
    public class FlatForCountingSpecification : BaseSpecification<Flat>
    {
        public FlatForCountingSpecification(SpecParams productParams)
          : base()
        {
            if (productParams.IsAccepted.HasValue)
            {
                AddCriteria(p => p.IsAccepted == productParams.IsAccepted);
            }

        }
    }
}

