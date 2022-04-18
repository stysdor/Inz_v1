using AutoMapper;
using API.DTO;
using Core.Domain;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<Flat, FlatDTO>().ReverseMap();
            CreateMap<Flat, FlatToModelDTO>()
                .ForMember(d => d.E_longitude, opt => opt.MapFrom(s => s.Location.E_longitude))
                .ForMember(d => d.N_latitude, opt => opt.MapFrom(s => s.Location.N_latitude))
                .ReverseMap();
            CreateMap<Model, ModelDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            
        }
    }
}
