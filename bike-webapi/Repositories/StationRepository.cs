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

        public PagedResult<Station> GetStations(int pageSize, int page, string? search)
        {
            IQueryable<Station> stations = _context.Stations;

            if (!String.IsNullOrWhiteSpace(search))
            {
                stations = stations.Where(s => 
                    s.Name.Contains(search) ||
                    s.Namn.Contains(search) ||
                    s.Nimi.Contains(search) ||
                    s.Osoite.Contains(search) ||
                    s.Adress.Contains(search) ||
                    s.Stad.Contains(search) ||
                    s.Kaupunki.Contains(search)
                );
            }

            stations = stations
                .Skip(pageSize*(page - 1))
                .Take(pageSize);
            
            var total = _context.Stations.Count();

            return new PagedResult<Station>() { Result = stations.ToList(), Total = total };
        }

        public Station GetStation(int id)
        {
            var station = _context.Stations
            .Include(s => s.Departures)
            .Include(s => s.Returns)
            .Where(s => s.Id == id)
            .FirstOrDefault();

            return station;
        }

        public ICollection<Station> GetTop5Destinations(int id)
        {
            var top5Ids = _context.Journeys
                .Where(j => j.DepartureStationId == id)
                .GroupBy(j => j.ReturnStationId)
                .Select(g => new { Id = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(5)
                .ToList();
            
            var topStations = _context.Stations
                .Where(s => top5Ids.Select(t => t.Id).Contains(s.Id))
                .ToList()
                .OrderBy(s => top5Ids.FindIndex(t => t.Id == s.Id))
                .ToList();
            
            return topStations;
        }

        public ICollection<Station> GetTop5Origins(int id)
        {
            var top5Ids = _context.Journeys
                .Where(j => j.ReturnStationId == id)
                .GroupBy(j => j.DepartureStationId)
                .Select(g => new { Id = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(5)
                .ToList();
            
            var topStations = _context.Stations
                .Where(s => top5Ids.Select(t => t.Id).Contains(s.Id))
                .ToList()
                .OrderBy(s => top5Ids.FindIndex(t => t.Id == s.Id))
                .ToList();
            
            return topStations;
        }
    }
}
