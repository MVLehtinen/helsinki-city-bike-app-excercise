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
        public IActionResult GetStations([FromQuery]int pageSize = 10, [FromQuery]int page = 1)
        {
            var stations = _stationRepository.GetStations(pageSize, page);

            var mappedPages = new PagedResult<StationDto>()
                {
                    Result = _mapper.Map<List<StationDto>>(stations.Result),
                    Total = stations.Total
                };

            return Ok(mappedPages);
        }
    }
}
