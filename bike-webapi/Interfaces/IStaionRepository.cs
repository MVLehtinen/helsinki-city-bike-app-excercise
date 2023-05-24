using bike_webapi.Models;

namespace bike_webapi.Interfaces
{
    public interface IStationRepository
    {
        ICollection<Station> GetStations();
    }
}
