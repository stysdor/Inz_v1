using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ModelDTO
{
    public class FlatDTO
    {
        public int Id { get; set; }
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime OfferDateTime { get; set; }
        public LocationDTO Location { get; set; }
        public int RoomNumber { get; set; }
        public int Floor { get; set; }
        public int FloorInBuilding { get; set; }
        public int ConstructionYear { get; set; }
        public string TypeOfBuilding { get; set; }
        public bool IsBalcony { get; set; }
        public bool IsGarden { get; set; }
        public bool IsTarrace { get; set; }
        public bool IsLoggia { get; set; }
        public bool IsCellar { get; set; }
        public bool IsGarage { get; set; }
        public bool IsParkingSpace { get; set; }
        public string Kitchen { get; set; }
        public string State { get; set; }
        public string Market { get; set; }
        public bool IsLift { get; set; }
        public bool IsAccepted { get; set; }
    }
}
