using Microsoft.EntityFrameworkCore;
using bike_webapi.Models;
using bike_webapi.Interfaces;
using bike_webapi.Data;
using bike_webapi.Helpers;

namespace bike_webapi.Repositories
{
    public class JourneyRepository : IJourneyRepository
    {
        private readonly AppDbContext _context;

        public JourneyRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedResult<Journey> GetJourneys(int pageSize, int page)
        {
            var journeys =  _context.Journeys
                .Include(j => j.DepartureStation)
                .Include(j => j.ReturnStation)
                .Skip(pageSize*(page - 1))
                .Take(pageSize)
                .ToList();
            
            var total = _context.Journeys.Count();

            return new PagedResult<Journey>() { Total = total, Result = journeys };
        }
    }
}
