using bike_webapi.Models;
using bike_webapi.Helpers;

namespace bike_webapi.Interfaces
{
    public interface IJourneyRepository
    {
        PagedResult<Journey> GetJourneys(int pageSize, int page, string orderBy);
    }
}
