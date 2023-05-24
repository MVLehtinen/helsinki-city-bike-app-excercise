namespace bike_webapi.Models
{
    public class Station
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
        public ICollection<Journey> Departures { get; set; } = new List<Journey>();
        public ICollection<Journey> Returns { get; set; } = new List<Journey>();

    }
}
