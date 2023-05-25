using bike_webapi.Models;
using bike_webapi.Helpers;

namespace bike_webapi.Interfaces
{
    public interface IStationRepository
    {
        PagedResult<Station> GetStations(int pageSize, int page, string? search);
        Station GetStation(int id);
        ICollection<Station> GetTop5Destinations(int id);
        ICollection<Station> GetTop5Origins(int id);
    }
}
