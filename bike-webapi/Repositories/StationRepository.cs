using bike_webapi.Models;
using bike_webapi.Interfaces;
using bike_webapi.Data;

namespace bike_webapi.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly AppDbContext _context;

        public StationRepository(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<Station> GetStations()
        {
            return _context.Stations.ToList();
        }
    }
}
