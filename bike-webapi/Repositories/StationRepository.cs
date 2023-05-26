using Microsoft.EntityFrameworkCore;
using bike_webapi.Models;
using bike_webapi.Interfaces;
using bike_webapi.Data;
using bike_webapi.Dto;

namespace bike_webapi.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly AppDbContext _context;

        public StationRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedResultDto<Station> GetStations(int pageSize, int page, string? search)
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

            return new PagedResultDto<Station>() { Result = stations.ToList(), Total = total };
        }

        public Station? GetStation(int id)
        {
            var station = _context.Stations
            .Where(s => s.Id == id)
            .FirstOrDefault();

            return station;
        }

        public ICollection<CountedResultDto<Station>> GetTop5Destinations(int id, int month)
        {
            var journeys = _context.Journeys.Where(j => j.DepartureStationId == id);

            if (month > 0 && month <= 12)
            {
                journeys = journeys.Where(j => j.Departure.Month == month);
            }
            
            var top5Ids = journeys
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
            
            var topStationsWithCount = topStations.Select((s, i) => 
                new CountedResultDto<Station>()
                    {
                        Total = top5Ids[i].Count,
                        Item = s
                    }
                )
                .ToList();

            return topStationsWithCount;
        }

        public ICollection<CountedResultDto<Station>> GetTop5Origins(int id, int month)
        {
            var journeys = _context.Journeys.Where(j => j.ReturnStationId == id);

            if (month > 0 && month <= 12)
            {
                journeys = journeys.Where(j => j.Departure.Month == month);
            }

            var top5Ids = journeys
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
            
            var topStationsWithCount = topStations.Select((s, i) => 
                new CountedResultDto<Station>()
                    {
                        Total = top5Ids[i].Count,
                        Item = s
                    }
                )
                .ToList();

            return topStationsWithCount;
        }

        public StationDetailsDto GetDetails(int id, int month)
        {
            var departures = _context.Journeys.Where(j => j.DepartureStationId == id);
            var returns = _context.Journeys.Where(j => j.ReturnStationId == id);

            if (month > 0 && month <= 12)
            {
                departures = departures.Where(j => j.Departure.Month == month);
                returns = returns.Where(j => j.Departure.Month == month);
            }

            var averageDistanceOfDeparture = departures.Average(j => j.CoveredDistance);
            var averageDistanceOfReturn = returns.Average(j => j.CoveredDistance);
            var totalDepartures = departures.Count();
            var totalReturns = returns.Count();

            return new StationDetailsDto()
                {
                    AverageDistanceOfDeparture = averageDistanceOfDeparture,
                    AverageDistanceOfReturn = averageDistanceOfReturn,
                    TotalDepartures = totalDepartures,
                    TotalReturns = totalReturns
                };
        }

        public bool AddStation(Station station)
        {
            _context.Add(station);

            return _context.SaveChanges() > 0;
        }
    }
}
