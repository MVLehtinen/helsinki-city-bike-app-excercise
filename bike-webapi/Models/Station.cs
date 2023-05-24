namespace bike_webapi.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Namn { get; set; }
        public string Name { get; set; }
        public string Osoite { get; set; }
        public string Adress { get; set; }
        public string Kaupunki { get; set; }
        public string Stad { get; set; }
        public string? Operaattori { get; set; }
        public int Kapasiteetti { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}
