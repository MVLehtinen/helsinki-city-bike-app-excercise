using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using bike_webapi.Models;
using bike_webapi.Interfaces;
using bike_webapi.Dto;

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
        public IActionResult GetStations()
        {
            var stations = _stationRepository.GetStations();

            return Ok(_mapper.Map<List<StationDto>>(stations));
        }
    }
}
