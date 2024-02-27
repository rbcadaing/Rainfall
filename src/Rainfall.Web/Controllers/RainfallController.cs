using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rainfall.Core.Dto;
using Rainfall.Service;
using Rainfall.Web.Model;
using System.Reflection;

namespace Rainfall.Web.Controllers
{
    [Tags("Name: Rainfall Station")]
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
        public async Task<ActionResult<List<EnvironmentDataStationResponseDto>>> GetRainfallStations([FromQuery] RainFallRequestDto rainfallQuery, [FromServices] IValidator<RainFallRequestDto> validator)
        {

            try
            {
                // Validate Model State
                ValidationResult validationResult = validator.Validate(rainfallQuery);
                if (!validationResult.IsValid)
                {
                    var modelStateDictionary = new ModelStateDictionary();
                    foreach (ValidationFailure failure in validationResult.Errors)
                    {
                        modelStateDictionary.AddModelError(
                            failure.PropertyName,
                            failure.ErrorMessage);
                    }
                    return ValidationProblem(modelStateDictionary);
                }

               
                _logger.LogInformation($"get-rainfall-stations processed a request!");

                var _params = rainfallQuery.GetType()
                             .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                  .ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(rainfallQuery, null));

                var response = await _environmentDataService.GetRainfallStations(_params);
                var stations = _mapper.Map<List<EnvironmentDataStationResponseDto>>(response.items);

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
