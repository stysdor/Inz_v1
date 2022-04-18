using BusinessLogic.BaseSpecification;
using BusinessLogic.Specification;
using Xunit;

namespace Tests
{
    public class FlatForCountingSpecificationTest
    {
        FlatForCountingSpecification spec;
        SpecParams flatparams;

        [Fact]
        public void ApplyPaginationValuesShouldNotAddSkipAndTake()
        {
            flatparams = new SpecParams()
            {
                PageIndex = 1,
                PageSize = 20
            };

            spec = new FlatForCountingSpecification(flatparams);
            Assert.Null(spec.Skip);
            Assert.Null(spec.Take);
        }

        [Fact]
        public void NotApplyingSortingShouldNotAddAnyOrder()
        {
            flatparams = new SpecParams();

            spec = new FlatForCountingSpecification(flatparams);
            Assert.True(spec.OrderByDescending == null || spec.OrderBy == null);
        }


        [Fact]
        public void ApplyingCorrectSortingShouldNotAddOrderByOrOrderByDescending()
        {
            flatparams = new SpecParams()
            {
                Sort = "priceAsc"
            };

            spec = new FlatForCountingSpecification(flatparams);
            Assert.Null(spec.OrderBy);

            flatparams.Sort = "areaAsc";
            spec = new FlatForCountingSpecification(flatparams);
            Assert.Null(spec.OrderBy);

            spec = new FlatForCountingSpecification(flatparams);
            Assert.Null(spec.OrderByDescending);

            flatparams.Sort = "areaDesc";
            spec = new FlatForCountingSpecification(flatparams);
            Assert.Null(spec.OrderByDescending);

            flatparams.Sort = "offerData";
            spec = new FlatForCountingSpecification(flatparams);
            Assert.Null(spec.OrderByDescending);
        }


        [Fact]
        public void ApplyingIsAcceptedAttributesShouldAddCritiria()
        {
            flatparams = new SpecParams()
            {
                IsAccepted = true
            };

            spec = new FlatForCountingSpecification(flatparams);
            Assert.NotNull(spec.Criteria);

            flatparams = new SpecParams()
            {
                IsAccepted = false
            };

            spec = new FlatForCountingSpecification(flatparams);
            Assert.NotNull(spec.Criteria);
        }

        [Fact]
        public void ApplyingIsUsedInModelAttributesShouldAddCritiria()
        {
            flatparams = new SpecParams()
            {
                IsUsedInModel = true
            };

            spec = new FlatForCountingSpecification(flatparams);
            Assert.NotNull(spec.Criteria);

            flatparams = new SpecParams()
            {
                IsUsedInModel = false
            };

            spec = new FlatForCountingSpecification(flatparams);
            Assert.NotNull(spec.Criteria);
        }

        [Fact]
        public void NotApplyingAnyAttributesShouldNotAddCritiria()
        {
            flatparams = new SpecParams();

            spec = new FlatForCountingSpecification(flatparams);
            Assert.Null(spec.Criteria);
        }

        [Fact]
        public void ApplyingManyAttributesShouldAddCritiria()
        {
            flatparams = new SpecParams()
            {
                IsAccepted = true,
                IsUsedInModel = false
            };

            spec = new FlatForCountingSpecification(flatparams);
            Assert.NotNull(spec.Criteria);
        }
    }
}
