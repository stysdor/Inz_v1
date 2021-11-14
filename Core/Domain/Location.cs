using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Location
    {
        public virtual int Id { get; set; }
        public virtual double N_latitude { get; set; }
        public virtual double E_longitude { get; set; }
    }
}
