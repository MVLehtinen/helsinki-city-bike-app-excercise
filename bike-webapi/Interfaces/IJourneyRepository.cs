using bike_webapi.Models;

namespace bike_webapi.Interfaces
{
    public interface IJourneyRepository
    {
        ICollection<Journey> GetJourneys();
    }
}
