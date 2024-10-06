using Newtonsoft.Json;

namespace BinovistTask1.Models
{
    public class Station
    {
        [JsonProperty("station_id")]
        public string StationId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("region_id")]
        public string RegionId { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("rental_methods")]
        public List<string> RentalMethods { get; set; }
    }
}
