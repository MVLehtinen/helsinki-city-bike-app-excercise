using System.Diagnostics.CodeAnalysis;
using bike_webapi.Data;
using bike_webapi.Models;
using CsvHelper;

namespace bike_webapi.Helpers
{
    public class DatabaseSeeder
    {
        private AppDbContext _context;

        public DatabaseSeeder(AppDbContext context)
        {
            _context = context;
        }

        public void AddStationsFromCSV(string filename)
        {
            using (var reader = new StreamReader(filename))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                System.Console.WriteLine("Now reading file: {0}", filename);
                List<Station> stations = new List<Station>();

                reader.ReadLine();

                while (csv.Read())
                {
                    try
                    {
                        var station = new Station()
                        {
                            Id = csv.GetField<int>(1),
                            Nimi = csv.GetField<string>(2)!,
                            Namn = csv.GetField<string>(3)!,
                            Name = csv.GetField<string>(4)!,
                            Osoite = csv.GetField<string>(5)!,
                            Adress = csv.GetField<string>(6)!,
                            Kaupunki = csv.GetField<string>(7)!,
                            Stad = csv.GetField<string>(8)!,
                            Operaattori = csv.GetField<string>(9),
                            Kapasiteetti = csv.GetField<int>(10),
                            X = csv.GetField<double>(11),
                            Y = csv.GetField<double>(12)
                        };
                        stations.Add(station);
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.Message);
                    }
                }
                _context.AddRange(stations);
                _context.SaveChanges();
            }
        }

        public void AddJourneysFromCSV(string filename)
        {
            HashSet<int> stationIds = _context.Stations.Select(s => s.Id).ToHashSet();
            using (var reader = new StreamReader(filename))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                System.Console.WriteLine("Now reading file: {0}", filename);
                List<Journey> journeys = new List<Journey>();

                reader.ReadLine();

                while (csv.Read())
                {
                    try
                    {
                        var departureId = csv.GetField<int>(2);
                        var returnId = csv.GetField<int>(4);
                        var coveredDistance = csv.GetField<int>(6);
                        var duration = csv.GetField<int>(7);

                        if (!stationIds.Contains(departureId) ||
                            !stationIds.Contains(returnId) ||
                            coveredDistance < 10 ||
                            duration < 10)
                        {
                            continue;
                        }
                        var journey = new Journey()
                        {
                            Departure = csv.GetField<DateTime>(0),
                            Return = csv.GetField<DateTime>(1),
                            DepartureStationId = departureId,
                            ReturnStationId = returnId,
                            CoveredDistance = coveredDistance,
                            Duration = duration
                        };

                        journeys.Add(journey);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                journeys = journeys.Distinct(new JourneyEqualityComparer()).ToList();
                _context.AddRange(journeys);
                _context.SaveChanges();
            }
        }
    }
    class JourneyEqualityComparer : IEqualityComparer<Journey>
    {
        public bool Equals(Journey? x, Journey? y)
        {
            if (x == null || y == null) return false;
            if (x.DepartureStationId != y.DepartureStationId) return false;
            if (x.ReturnStationId != y.ReturnStationId) return false;
            if (!x.Departure.Equals(y.Departure)) return false;
            if (!x.Return.Equals(y.Return)) return false;
            return true;
        }

        public int GetHashCode([DisallowNull] Journey obj)
        {
            return obj.Departure.GetHashCode()
                ^ obj.Return.GetHashCode()
                ^ obj.ReturnStationId.GetHashCode()
                ^ obj.DepartureStationId.GetHashCode();
        }
    }
}
