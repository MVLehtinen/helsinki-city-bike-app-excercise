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
        public IActionResult GetJourneys(
            [FromQuery]int pageSize = 10, 
            [FromQuery]int page = 1, 
            [FromQuery]string? orderBy = "",
            [FromQuery]string? search = "",
            [FromQuery]int departureStationId = 0,
            [FromQuery]int returnStationId = 0,
            [FromQuery]int month = 0
            )
        {
            var journeys = _journeyRepository.GetJourneys(pageSize, page, orderBy, search, departureStationId, returnStationId, month);

            var mappedPages = new PagedResult<JourneyDto>()
                {
                    Result = _mapper.Map<List<JourneyDto>>(journeys.Result),
                    Total = journeys.Total
                };

            return Ok(mappedPages);
        }
    }
}
