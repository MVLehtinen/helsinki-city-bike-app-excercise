namespace bike_webapi.Dto
{
    public class StationDetailsDto
    {
        public double AverageDistanceOfDeparture { get; set; }
        public double AverageDistanceOfReturn { get; set;}
        public int TotalDepartures { get; set; }
        public int TotalReturns { get; set; }
        public ICollection<CountedResultDto<StationDto>>? Top5Destinations { get; set; }
        public ICollection<CountedResultDto<StationDto>>? Top5Origins { get; set; }
    }
}
