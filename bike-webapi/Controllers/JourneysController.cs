using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using bike_webapi.Models;
using bike_webapi.Interfaces;
using bike_webapi.Dto;
using bike_webapi.Helpers;

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
        public IActionResult GetStations([FromQuery]int pageSize = 10, [FromQuery]int page = 1)
        {
            var journeys = _journeyRepository.GetJourneys(pageSize, page);

            var mappedPages = new PagedResult<JourneyDto>()
                {
                    Result = _mapper.Map<List<JourneyDto>>(journeys.Result),
                    Total = journeys.Total
                };

            return Ok(mappedPages);
        }
    }
}
