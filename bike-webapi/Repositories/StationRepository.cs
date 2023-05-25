using Microsoft.EntityFrameworkCore;
using bike_webapi.Models;
using bike_webapi.Interfaces;
using bike_webapi.Data;
using bike_webapi.Helpers;

namespace bike_webapi.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly AppDbContext _context;

        public StationRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedResult<Station> GetStations(int pageSize, int page)
        {
            var stations = _context.Stations
                .Include(s => s.Departures)
                .Include(s => s.Returns)
                .Skip(pageSize*(page - 1))
                .Take(pageSize)
                .ToList();
            
            var total = _context.Stations.Count();

            return new PagedResult<Station>() { Result = stations, Total = total };
        }
    }
}
