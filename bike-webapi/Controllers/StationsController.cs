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
    public class StationsController : ControllerBase
    {
        private IStationRepository _stationRepository;
        private IMapper _mapper;

        public StationsController(IStationRepository stationRepository, IMapper mapper)
        {
            _stationRepository = stationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetStations([FromQuery]int pageSize = 10, [FromQuery]int page = 1, [FromQuery]string? search = "")
        {
            var stations = _stationRepository.GetStations(pageSize, page, search);

            var mappedPages = new PagedResult<StationDto>()
                {
                    Result = _mapper.Map<List<StationDto>>(stations.Result),
                    Total = stations.Total
                };

            return Ok(mappedPages);
        }

        [HttpGet("{id}")]
        public IActionResult GetStation(int id)
        {
            var station = _stationRepository.GetStation(id);

            if (station == null)
            {
                return NotFound();
            }

            var mapped = _mapper.Map<StationDetailsDto>(station);

            mapped.Top5Destinations = _mapper.Map<List<StationDto>>(_stationRepository.GetTop5Destinations(id));
            mapped.Top5Origins = _mapper.Map<List<StationDto>>(_stationRepository.GetTop5Origins(id));

            return Ok(mapped);
        }
    }
}
