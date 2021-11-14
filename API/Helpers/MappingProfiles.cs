using AutoMapper;
using API.ModelDTO;
using Core.Domain;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<LandOffer, LandOfferDTO>().ReverseMap();
            CreateMap<Land, LandDTO>().ReverseMap();
            
        }
    }
}
