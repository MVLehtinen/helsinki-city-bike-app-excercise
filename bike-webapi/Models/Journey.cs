namespace bike_webapi.Models
{
    public class Journey
    {
        public int Id { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Return { get; set; }
        public int DepartureStationId { get; set; }
        public int ReturnStationId { get; set; }
        public int CoveredDistance { get; set; }
        public int Duration { get; set; }
        public Station DepartureStation { get; set; }
        public Station ReturnStation { get; set; }
    }
}
