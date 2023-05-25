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
            CreateMap<Station, StationDetailsDto>()
                .ForMember(d => d.AverageDistanceFromStation, o => o.MapFrom(s => s.Departures.Average(j => j.CoveredDistance)))
                .ForMember(d => d.AverageDistanceToStation, o => o.MapFrom(s => s.Returns.Average(j => j.CoveredDistance)));
        }
    }
}
