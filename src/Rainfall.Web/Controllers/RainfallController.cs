using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rainfall.Core.Dto;
using Rainfall.Service;
using Rainfall.Web.Model;
using System.Net;
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
        [ProducesResponseType(typeof(IEnumerable<ValidationFailure>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRainfallStations([FromQuery] RainFallRequestDto rainfallQuery, [FromServices] IValidator<RainFallRequestDto> validator)
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
                    return ValidationProblem(null, null, 400, null, null, modelStateDictionary);
                }


                _logger.LogInformation($"get-rainfall-stations processed a request!");

                var _params = rainfallQuery.GetType()
                             .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                  .ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(rainfallQuery, null));

                var response = await _environmentDataService.GetRainfallStations(_params);
                var stations = _mapper.Map<List<EnvironmentDataStationResponseDto>>(response.items);

                if (stations == null || stations.Count == 0) return new NoContentResult();

                return new OkObjectResult(stations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }
    }
}
