using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ModelDTO
{
    public class LandOfferDTO
    {
        public int Id { get; set; }
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public string Electricity { get; set; }
        public string Gas { get; set; }
        public string Water { get; set; }
        public string Sewers { get; set; }
        public string Type { get; set; }
        public string Road { get; set; }
        public string Description { get; set; }
        public LocationDTO Location { get; set; }
        public bool IsElectricity { get; set; }
        public bool IsGas { get; set; }
        public bool IsWater { get; set; }
        public bool IsSewers { get; set; }
        public DateTime OfferDateTime { get; set; }
    }
}
