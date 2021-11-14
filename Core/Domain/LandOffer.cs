using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class LandOffer
    {
        public virtual int Id { get; set; }
        public virtual decimal Area { get; set; }
        public virtual decimal Price { get; set; }
        public virtual string Electricity { get; set; }
        public virtual string Gas { get; set; }
        public virtual string Water { get; set; }
        public virtual string Sewers { get; set; }
        public virtual string Type { get; set; }
        public virtual string Road { get; set; }
        public virtual Location Location { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime OfferDateTime { get; set; }
        public virtual bool IsElectricity { get; set; }
        public virtual bool IsGas { get; set; }
        public virtual bool IsWater { get; set; }
        public virtual bool IsSewers { get; set; }
    }
}
