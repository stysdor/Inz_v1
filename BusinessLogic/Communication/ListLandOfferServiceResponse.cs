using BusinessLogic.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Communication
{
    public class ListLandOfferServiceResponse : ServiceResponse
    {
        public List<LandOfferDTO> Data { get; set; }
    }
}
