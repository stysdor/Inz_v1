using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Land
    {
        public int Id { get; set; }
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public Boolean IsElectricity { get; set; }
        public Boolean IsGas { get; set; }
        public Boolean IsWater { get; set; }
        public Boolean IsSewers { get; set; }
        public string Type { get; set; }
        public string Road { get; set; }
        public Location Location { get; set; }   
    }
}
