using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIModule.AIModel
{
    public class Flat
    {
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public double N_latitude { get; set; }
        public double E_longitude { get; set; }
        public  int? Floor { get; set; }
        public int? FloorInBuilding { get; set; }
        public int? ConstructionYear { get; set; }
        public string TypeOfBuilding { get; set; }
        public bool IsBalcony { get; set; }
        public bool IsGarden { get; set; }
        public bool IsTarrace { get; set; }
        public bool IsLoggia { get; set; }
        public bool IsCellar { get; set; }
        public bool IsGarage { get; set; }
        public bool IsParkingSpace { get; set; }
        public  string State { get; set; }
        public string Market { get; set; }
        public bool IsLift { get; set; }
    }

    public class NormalizedFlat
    {
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public double N_latitude { get; set; }
        public double E_longitude { get; set; }
        public int? Floor { get; set; }
        public int? FloorInBuilding { get; set; }
        public int? ConstructionYear { get; set; }
        public int TypeOfBuilding { get; set; }
        public int IsBalcony { get; set; }
        public int IsGarden { get; set; }
        public int IsTarrace { get; set; }
        public int IsLoggia { get; set; }
        public int IsCellar { get; set; }
        public int IsGarage { get; set; }
        public int IsParkingSpace { get; set; }
        public int State { get; set; }
        public int Market { get; set; }
        public int IsLift { get; set; }
    }
}

