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
            CreateMap<CountedResultDto<Station>, CountedResultDto<StationDto>>();
            CreateMap<PagedResultDto<Station>, PagedResultDto<StationDto>>();
            CreateMap<PagedResultDto<Journey>, PagedResultDto<JourneyDto>>();
            CreateMap<StationDto, Station>();
            CreateMap<JourneyDto, Journey>();
        }
    }
}
