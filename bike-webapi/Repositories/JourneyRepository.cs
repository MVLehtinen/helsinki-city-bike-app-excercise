using Microsoft.EntityFrameworkCore;
using bike_webapi.Models;
using bike_webapi.Interfaces;
using bike_webapi.Data;

namespace bike_webapi.Repositories
{
    public class JourneyRepository : IJourneyRepository
    {
        private readonly AppDbContext _context;

        public JourneyRepository(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<Journey> GetJourneys()
        {
            return _context.Journeys
            .Include(j => j.DepartureStation)
            .Include(j => j.ReturnStation)
            .ToList();
        }
    }
}
