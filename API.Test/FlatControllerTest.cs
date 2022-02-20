using Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace API.Test
{
    [TestClass]
    public class FlatControllerTest
    {
        [TestMethod]
        public void GetFlats_ShouldReturnFlats()
        {
            var testFlats = GetTestFlats();
        }

        private List<Flat> GetTestFlats() {
            var testFlats = new List<Flat>();
            testFlats.Add(new Flat());

            return testFlats;
        }
    }
}
