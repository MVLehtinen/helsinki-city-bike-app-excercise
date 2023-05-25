namespace bike_webapi.Dto
{
    public class StationDetailsDto
    {
        public int Id { get; set; }
        public string Nimi { get; set; } = null!;
        public string Namn { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Osoite { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public string Kaupunki { get; set; } = null!;
        public string Stad { get; set; } = null!;
        public string? Operaattori { get; set; }
        public int Kapasiteetti { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int AverageDistanceFromStation { get; set; }
        public int AverageDistanceToStation { get; set;}
        public List<StationDto> Top5Destinations { get; set; } = null!;
        public List<StationDto> Top5Origins { get; set; } = null!;
    }
}