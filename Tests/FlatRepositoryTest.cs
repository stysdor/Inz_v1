using BusinessLogic.Interfaces;
using BusinessLogic.Repositories;
using Core.Domain;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class FlatRepositoryTest
    {

       

        [Fact]
        public void AddFlatsTest ()
        {
            using (var sut = new SystemUnderTest())
            {
                var flat = sut.CreateFlat();
                var flats = new List<Flat> { flat };
                var repoSubstitute = Substitute.For<IFlatRepository>();
            }

        }
    }
}
