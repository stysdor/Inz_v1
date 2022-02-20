using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Models
{
    public class FlatProxy: Flat
    {

        public FlatProxy() {
            Area = 61.23M;
            Location = new Location() { N_latitude = 50.12345, E_longitude = 22.12345 };
            ConstructionYear = 2005;
            Description = "Description";
            Floor = 5;
            FloorInBuilding = 10;
            IsBalcony = true;
            IsCellar = false;
            IsGarage = false;
            IsLoggia = false;
            IsParkingSpace = true;
            IsTarrace = false;
            IsGarden = false;
            Market = "wtórny";
            RoomNumber = 3;
            Price = 350000;
            IsLift = true;
            State = "do zamieszkania";
            TypeOfBuilding = "blok";
            FlatLink = new FlatLink("link");
        }
    }
}
