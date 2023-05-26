using bike_webapi.Models;
using bike_webapi.Dto;

namespace bike_webapi.Interfaces
{
    public interface IJourneyRepository
    {
        PagedResultDto<Journey> GetJourneys(
            int pageSize, 
            int page, 
            string? orderBy, 
            string? search, 
            int departureStationId,
            int returnStationId,
            int month);
    }
}
