using AutoMapper;
using bike_webapi.Models;

namespace bike_webapi.Dto
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Journey, JourneyDto>()
                .ForMember(d => d.DepartureStationName, o => o.MapFrom(s => s.DepartureStation.Nimi))
                .ForMember(d => d.ReturnStationName, o => o.MapFrom(s => s.ReturnStation.Nimi));
            CreateMap<Station, StationDto>();
        }
    }
}
