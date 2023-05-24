using Microsoft.AspNetCore.Mvc;
using bike_webapi.Models;
using bike_webapi.Interfaces;

namespace bike_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StationsController : ControllerBase
    {
        private IStationRepository _stationRepository;

        public StationsController(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        [HttpGet]
        public IActionResult GetStations()
        {
            var stations = _stationRepository.GetStations();

            return Ok(stations);
        }
    }
}
