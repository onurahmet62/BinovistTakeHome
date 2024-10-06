using BinovistTask1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BinovistTask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly string _stationDataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "station.json");

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Station>>> GetStations()
        {
            try
            {
              
                var json = await System.IO.File.ReadAllTextAsync(_stationDataFilePath);

                
                var stationData = JsonConvert.DeserializeObject<StationData>(json);

               
                return Ok(stationData.Data.Stations);
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
    }
   
    public class StationData
    {
        public int LastUpdated { get; set; }
        public int Ttl { get; set; }
        public StationDataDetail Data { get; set; }
    }

    public class StationDataDetail
    {
        public List<Station> Stations { get; set; }
    }


}
