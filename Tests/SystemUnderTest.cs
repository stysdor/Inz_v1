using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Models;

namespace Tests
{
    public class SystemUnderTest: IDisposable
    {

        public void Dispose() { }

        public Flat CreateFlat() {
            var flat = new FlatProxy();
            return flat;
        }
    }
}
