using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Flat 
    {
        public virtual int Id { get; set; }
        public virtual decimal Area { get; set; }
        public virtual decimal Price { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime OfferDateTime { get; set; }
        public virtual Location Location { get; set; }
        public virtual int? RoomNumber { get; set; }
        public virtual int? Floor { get; set; }
        public virtual int? FloorInBuilding { get; set; }
        public virtual int? ConstructionYear { get; set; }
        public virtual string TypeOfBuilding { get; set; }
        public virtual bool IsBalcony { get; set; }
        public virtual bool IsGarden { get; set; }
        public virtual bool IsTarrace { get; set; }
        public virtual bool IsLoggia { get; set; }
        public virtual bool IsCellar { get; set; }
        public virtual bool IsGarage { get; set; }
        public virtual bool IsParkingSpace { get; set; }
        public virtual string Kitchen { get; set; }
        public virtual string State { get; set; }
        public virtual string Market { get; set; }
        public virtual bool IsLift { get; set; }
        public virtual bool IsAccepted { get; set; }
        public virtual FlatLink FlatLink { get; set; }

    }
}
