using Microsoft.AspNetCore.Mvc;
using bike_webapi.Models;
using bike_webapi.Interfaces;

namespace bike_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JourneysController : ControllerBase
    {
        private IJourneyRepository _journeyRepository;

        public JourneysController(IJourneyRepository journeyRepository)
        {
            _journeyRepository = journeyRepository;
        }

        [HttpGet]
        public IActionResult GetStations()
        {
            var journeys = _journeyRepository.GetJourneys();

            return Ok(journeys);
        }
    }
}
