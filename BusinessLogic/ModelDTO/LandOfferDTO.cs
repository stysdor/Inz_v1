using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ModelDTO
{
    public class LandOfferDTO
    {
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public string Electricity { get; set; }
        public string Gas { get; set; }
        public string Water { get; set; }
        public string Sewers { get; set; }
        public string Type { get; set; }
        public string Road { get; set; }
        public string Description { get; set; }
    }
}
