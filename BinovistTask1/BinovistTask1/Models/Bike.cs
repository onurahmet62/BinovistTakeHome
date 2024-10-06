namespace BinovistTask1.Models
{
    public class Bike
    {
        public string BikeId { get; set; }
        public string StationId { get; set; }
        public string Name { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
        public int IsReserved { get; set; }
        public int IsDisabled { get; set; }
    }
}
