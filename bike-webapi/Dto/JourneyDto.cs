namespace bike_webapi.Dto
{
    public class JourneyDto
    {
        public int Id { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Return { get; set; }
        public string DepartureStationName { get; set; } = null!;
        public string ReturnStationName { get; set; } = null!;
        public int DepartureStationId { get; set; }
        public int ReturnStationId { get; set; }
        public int CoveredDistance { get; set; }
        public int Duration { get; set; }
    }
}
