using BusinessLogic.BaseSpecification;
using BusinessLogic.Specification;
using Xunit;

namespace Tests
{
    public class SpecParamsTest
    {
        SpecParams flatparams;

        [Fact]
        public void ApplyCorrectPaginationValuesShouldReceiveCorrectOnes()
        {
            flatparams = new SpecParams()
            {
                PageIndex = 1,
                PageSize = 20
            };

            Assert.True(flatparams.PageIndex == 1 && flatparams.PageSize == 20);

            flatparams.PageSize = 50;
            flatparams.PageIndex = 5;

            Assert.True(flatparams.PageIndex == 5 && flatparams.PageSize == 50);
        }

        [Fact]
        public void ApplyIncorrectPaginationValuesShouldReceiveCorrectOnes()
        {
            flatparams = new SpecParams()
            {
                PageIndex = -5,
                PageSize = 20
            };

            Assert.True(flatparams.PageIndex == 1 && flatparams.PageSize == 20);

            flatparams.PageIndex = 0;

            Assert.True(flatparams.PageIndex == 1 && flatparams.PageSize == 20);

            flatparams.PageSize = 1000000;

            Assert.True(flatparams.PageIndex == 1 && flatparams.PageSize == 10000);

            flatparams.PageSize = 0;

            Assert.True(flatparams.PageIndex == 1 && flatparams.PageSize == 10000);

            flatparams.PageSize = -2;

            Assert.True(flatparams.PageIndex == 1 && flatparams.PageSize == 10000);
        }
    }
}
