using Newtonsoft.Json;
using System;

namespace API.DTO
{
    [JsonObject(MemberSerialization.OptOut)]
    public class FlatToModelDTO
    {
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public double N_latitude { get; set; }
        public double E_longitude { get; set; }
        public int RoomNumber { get; set; }
        public int Floor { get; set; }
        public int FloorInBuilding { get; set; }
        public int ConstructionYear { get; set; }
        public bool IsBalcony { get; set; }
        public bool IsGarden { get; set; }
        public bool IsTarrace { get; set; }
        public bool IsLoggia { get; set; }
        public bool IsCellar { get; set; }
        public bool IsGarage { get; set; }
        public bool IsParkingSpace { get; set; }
        public string Market { get; set; }
        public bool IsLift { get; set; }

    }
}
