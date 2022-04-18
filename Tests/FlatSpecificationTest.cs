using BusinessLogic.BaseSpecification;
using BusinessLogic.Specification;
using Xunit;

namespace Tests
{
    public class FlatSpecificationTest
    {
        FlatSpecification spec;
        SpecParams flatparams;

        [Fact]
        public void ApplyPaginationValuesReturnCorrectSkipAndTake()
        {
            flatparams = new SpecParams()
            {
                PageIndex = 1,
                PageSize = 20
            };

            spec = new FlatSpecification(flatparams);
            Assert.True(spec.Skip == 0 && spec.Take == 20);

            flatparams.PageIndex = 2;
            spec = new FlatSpecification(flatparams);
            Assert.True(spec.Skip == 20 && spec.Take == 20);

            flatparams.PageSize = 4;
            spec = new FlatSpecification(flatparams);
            Assert.True(spec.Skip == 4 && spec.Take == 4);


        }

        [Fact]
        public void ApplyIncorectPaginationValuesShouldAddCorrectSkipAndTake()
        {
            flatparams = new SpecParams()
            {
                PageIndex = 1,
                PageSize = 20
            };

            flatparams.PageSize = 0;
            spec = new FlatSpecification(flatparams);
            Assert.True(spec.Skip == 0 && spec.Take == 20);

            flatparams.PageIndex = -1;
            spec = new FlatSpecification(flatparams);
            Assert.True(spec.Skip == 0 && spec.Take == 20);
        }

        [Fact]
        public void NotApplyingSortingShouldAddSomeOrder()
        {
            flatparams = new SpecParams();

            spec = new FlatSpecification(flatparams);
            Assert.True(spec.OrderByDescending != null || spec.OrderBy != null);
        }


        [Fact]
        public void ApplyingCorrectSortingShouldAddOrderBy()
        {
            flatparams = new SpecParams()
            {
                Sort = "priceAsc"
            };

            spec = new FlatSpecification(flatparams);
            Assert.NotNull(spec.OrderBy);

            flatparams.Sort = "areaAsc";
            spec = new FlatSpecification(flatparams);
            Assert.NotNull(spec.OrderBy);
        }

        [Fact]
        public void ApplyingCorrectSortingShouldAddOrderByDescending()
        {
            flatparams = new SpecParams()
            {
                Sort = "priceDesc"
            };

            spec = new FlatSpecification(flatparams);
            Assert.NotNull(spec.OrderByDescending);

            flatparams.Sort = "areaDesc";
            spec = new FlatSpecification(flatparams);
            Assert.NotNull(spec.OrderByDescending);

            flatparams.Sort = "offerData";
            spec = new FlatSpecification(flatparams);
            Assert.NotNull(spec.OrderByDescending);
        }


        [Fact]
        public void ApplyingIsAcceptedAttributesShouldAddCritiria()
        {
            flatparams = new SpecParams()
            {
                IsAccepted = true
            };

            spec = new FlatSpecification(flatparams);
            Assert.NotNull(spec.Criteria);

            flatparams = new SpecParams()
            {
                IsAccepted = false
            };

            spec = new FlatSpecification(flatparams);
            Assert.NotNull(spec.Criteria);
        }

        [Fact]
        public void ApplyingIsUsedInModelAttributesShouldAddCritiria()
        {
            flatparams = new SpecParams()
            {
                IsUsedInModel = true
            };

            spec = new FlatSpecification(flatparams);
            Assert.NotNull(spec.Criteria);

            flatparams = new SpecParams()
            {
                IsUsedInModel = false
            };

            spec = new FlatSpecification(flatparams);
            Assert.NotNull(spec.Criteria);
        }

        [Fact]
        public void NotApplyingAnyAttributesShouldNotAddCritiria()
        {
            flatparams = new SpecParams();

            spec = new FlatSpecification(flatparams);
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

            spec = new FlatSpecification(flatparams);
            Assert.NotNull(spec.Criteria);
        }
    }
}
