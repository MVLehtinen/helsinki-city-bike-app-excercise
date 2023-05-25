using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using bike_webapi.Models;
using bike_webapi.Interfaces;
using bike_webapi.Dto;

namespace bike_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JourneysController : ControllerBase
    {
        private IJourneyRepository _journeyRepository;
        private IMapper _mapper;

        public JourneysController(IJourneyRepository journeyRepository, IMapper mapper)
        {
            _journeyRepository = journeyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetStations()
        {
            var journeys = _journeyRepository.GetJourneys();

            return Ok(_mapper.Map<List<JourneyDto>>(journeys));
        }
    }
}
