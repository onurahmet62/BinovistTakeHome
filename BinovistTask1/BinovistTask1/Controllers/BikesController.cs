using BinovistTask1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace BinovistTask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikesController : ControllerBase
    {
        private readonly string _dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "bike.json");

        // GET api/Bikes?searchTerm=term
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bike>>> GetBikes(string searchTerm = null)
        {
            try
            {
              
                var json = await System.IO.File.ReadAllTextAsync(_dataFilePath);

               
                var bikeData = JsonConvert.DeserializeObject<BikeData>(json);

                
                var filteredBikes = string.IsNullOrEmpty(searchTerm)
                    ? bikeData.Data.Bikes
                    : bikeData.Data.Bikes.Where(b => b.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

               
                return Ok(filteredBikes);
            }
            catch (IOException ex)
            {
                return StatusCode(500, $"Dosya okuma hatası: {ex.Message}");
            }
            catch (JsonException ex)
            {
                return BadRequest($"JSON işleme hatası: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Bike>> PostBike([FromBody] Bike newBike)
        {
            try
            {
                // Önce Station'ın geçerli olup olmadığını kontrol et
                var stationJson = await System.IO.File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "data", "station.json"));
                var stationData = JsonConvert.DeserializeObject<StationData>(stationJson);

                var matchingStation = stationData.Data.Stations.FirstOrDefault(s => s.StationId == newBike.StationId);
                if (matchingStation == null)
                {
                    return BadRequest("Geçersiz stationId.");
                }

                
                var json = await System.IO.File.ReadAllTextAsync(_dataFilePath);
                var bikeData = JsonConvert.DeserializeObject<BikeData>(json);

               
                bikeData.Data.Bikes.Add(newBike);

               
                var updatedJson = JsonConvert.SerializeObject(bikeData, Formatting.Indented);
                await System.IO.File.WriteAllTextAsync(_dataFilePath, updatedJson);

               
                return CreatedAtAction(nameof(GetBikes), new { id = newBike.BikeId }, newBike);
            }
            catch (IOException ex)
            {
                return StatusCode(500, $"Dosya okuma/yazma hatası: {ex.Message}");
            }
            catch (JsonException ex)
            {
                return BadRequest($"JSON işleme hatası: {ex.Message}");
            }
        }
    }

  
    public class BikeData
    {
        public int LastUpdated { get; set; }
        public int Ttl { get; set; }
        public BikeDataDetail Data { get; set; }
    }

    public class BikeDataDetail
    {
        public List<Bike> Bikes { get; set; }
    }
}
