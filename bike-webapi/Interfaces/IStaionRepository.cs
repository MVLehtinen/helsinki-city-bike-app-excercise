using bike_webapi.Models;
using bike_webapi.Dto;

namespace bike_webapi.Interfaces
{
    public interface IStationRepository
    {
        PagedResultDto<Station> GetStations(int pageSize, int page, string? search);
        Station? GetStation(int id);
        ICollection<CountedResultDto<Station>> GetTop5Destinations(int id, int month);
        ICollection<CountedResultDto<Station>> GetTop5Origins(int id, int month);
        StationDetailsDto GetDetails(int id, int month);
        bool AddStation(Station station);
    }
}
