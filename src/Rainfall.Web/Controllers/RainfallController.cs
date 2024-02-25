using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rainfall.Core.Dto;
using Rainfall.Service;
using Rainfall.Web.Model;

namespace Rainfall.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RainfallController : ControllerBase
    {
        private readonly ILogger<RainfallController> _logger;
        private readonly IEnvironmentDataService _environmentDataService;
        private readonly IMapper _mapper;

        public RainfallController(ILogger<RainfallController> logger, IEnvironmentDataService environmentDataService, IMapper mapper)
        {
            _logger = logger;
            _environmentDataService = environmentDataService;
            _mapper = mapper;
        }

        [HttpGet("get-rainfall-stations")]
        public async Task<ActionResult<List<EnvironmentDataStationDto>>> GetRainfallStations([FromQuery] RainFallQuery rainfallQuery)
        {
            _logger.LogInformation($"get-rainfall-stations processed a request!");
            try
            {
                var response = await _environmentDataService.GetRainfallStations(rainfallQuery.limit, rainfallQuery.offset, rainfallQuery.view);
                var stations = _mapper.Map<List<EnvironmentDataStationDto>>(response.items);

                if (stations == null) return StatusCode(204);

                return stations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }
    }
}
