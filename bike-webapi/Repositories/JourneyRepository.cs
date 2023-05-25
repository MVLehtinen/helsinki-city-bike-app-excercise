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

        public PagedResult<Journey> GetJourneys(int pageSize, int page, string orderBy)
        {
            IQueryable<Journey> journeys =  _context.Journeys
                .Include(j => j.DepartureStation)
                .Include(j => j.ReturnStation);


            switch(orderBy)
            {
                case "departureAscending":
                    journeys = journeys.OrderBy(j => j.Departure);
                    break;

                case "departureDescending":
                    journeys = journeys.OrderByDescending(j => j.Departure);
                    break;
                
                case "returnAscending":
                    journeys = journeys.OrderBy(j => j.Return);
                    break;

                case "returnDescending":
                    journeys = journeys.OrderByDescending(j => j.Return);
                    break;
                
                case "departureStationAscending":
                    journeys = journeys.OrderBy(j => j.DepartureStation.Nimi).ThenBy(j => j.Departure);
                    break;
                
                case "departureStationDescending":
                    journeys = journeys.OrderByDescending(j => j.DepartureStation.Nimi).ThenBy(j => j.Departure);
                    break;
                
                case "returnStationAscending":
                    journeys = journeys.OrderBy(j => j.ReturnStation.Nimi).ThenBy(j => j.Return);
                    break;
                
                case "returnStationDescending":
                    journeys = journeys.OrderByDescending(j => j.ReturnStation.Nimi).ThenBy(j => j.Return);
                    break;
                
                case "distanceAscending":
                    journeys = journeys.OrderBy(j => j.CoveredDistance).ThenBy(j => j.Departure);
                    break;
                
                case "distanceDescending":
                    journeys = journeys.OrderByDescending(j => j.CoveredDistance).ThenBy(j => j.Departure);
                    break;
                
                case "durationAscending":
                    journeys = journeys.OrderBy(j => j.Duration).ThenBy(j => j.Departure);
                    break;

                case "durationDescending":
                    journeys = journeys.OrderByDescending(j => j.Duration).ThenBy(j => j.Departure);
                    break;
                
                default:
                    journeys = journeys.OrderBy(j => j.Departure);
                    break;
            }
            
            journeys = journeys
                .Skip(pageSize*(page - 1))
                .Take(pageSize);
            
            var total = _context.Journeys.Count();

            return new PagedResult<Journey>() { Total = total, Result = journeys.ToList() };
        }
    }
}
