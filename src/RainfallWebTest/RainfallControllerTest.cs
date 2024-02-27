using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Rainfall.Core.Common;
using Rainfall.Core.Dto;
using Rainfall.Core.Validations;
using Rainfall.Service;
using Rainfall.Web.Controllers;
using Rainfall.Web.Helpers;
using Rainfall.Web.Model;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace RainfallWebTest
{
    public class RainfallControllerTest
    {
        private IMapper _mapper;
        private Mock<IHttpClientFactory> _httpClientFactoryMock = new();
        private Mock<HttpMessageHandler> _handlerMock = new();
        private Mock<ILogger<RainfallController>> _rainfallControllerLogger = new();
        private Mock<ILogger<EnvironmentDataService>> _environmentDataServiceLogger = new();
        private IEnvironmentDataService? _environmentDataService;
        private IConfiguration _config;
        private IValidator<RainFallRequestDto>? _validator;

        [OneTimeSetUp]
        public void Setup()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoMapper(typeof(AutoMapperProfiles));

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();

            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();

            _config = builder.Build();


        }

        [Test]
        public async Task GetRainfallStations_return_200()
        {
            //ARRANGE
            string invalidResponsePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Assets\\stations_response.json";
            var rainfallApiJson = System.IO.File.ReadAllText(invalidResponsePath);

            var response = new HttpResponseMessage()
            {
                Content = new StringContent(rainfallApiJson),
                StatusCode = System.Net.HttpStatusCode.OK
            };

            setupHttpMock(response);

            _environmentDataService = new EnvironmentDataService(_environmentDataServiceLogger.Object, _httpClientFactoryMock.Object, _config);

            var queryParams = new RainFallRequestDto() { _limit = "2", _offset = "10" };

            var _rainfallController = new RainfallController(_rainfallControllerLogger.Object, _environmentDataService, _mapper);
            var validator = new RainFallRequestDtoValidator();

            //ACT
            var res = await _rainfallController.GetRainfallStations(queryParams, validator);

            //ASSERT
            Assert.AreEqual(true, res is OkObjectResult);

        }

        [Test]
        public async Task GetRainfallStations_return_204()
        {
            //ARRANGE
            string invalidResponsePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Assets\\stations_no_content_response.json";
            var rainfallApiJson = System.IO.File.ReadAllText(invalidResponsePath);

            var response = new HttpResponseMessage()
            {
                Content = new StringContent(rainfallApiJson),
                StatusCode = System.Net.HttpStatusCode.NoContent
            };

            setupHttpMock(response);

            _environmentDataService = new EnvironmentDataService(_environmentDataServiceLogger.Object, _httpClientFactoryMock.Object, _config);

            var queryParams = new RainFallRequestDto() { _limit = "2", _offset = "10", search = "paperT" };

            var _rainfallController = new RainfallController(_rainfallControllerLogger.Object, _environmentDataService, _mapper);
            var validator = new RainFallRequestDtoValidator();

            //ACT
            var res = await _rainfallController.GetRainfallStations(queryParams, validator);

            //ASSERT
            Assert.AreEqual(true, res is NoContentResult);

        }

        [Test]
        public async Task GetRainfallStations_return_400()
        {
            _environmentDataService = new EnvironmentDataService(_environmentDataServiceLogger.Object, _httpClientFactoryMock.Object, _config);

            var queryParams = new RainFallRequestDto() { _limit = "-2", _offset = "10" };

            var _rainfallController = new RainfallController(_rainfallControllerLogger.Object, _environmentDataService, _mapper);
            var validator = new RainFallRequestDtoValidator();

            //ACT
            var res = await _rainfallController.GetRainfallStations(queryParams, validator);
            var apireponse = res as BadRequestObjectResult;

            //ASSERT
            Assert.AreEqual(400, apireponse.StatusCode);

        }


        private void setupHttpMock(HttpResponseMessage res)
        {
            _handlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
               "SendAsync",
               ItExpr.IsAny<HttpRequestMessage>(),
               ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(res)
               .Verifiable();

            _httpClientFactoryMock.Setup(x => x.CreateClient("RainfallApi"))
                .Returns(((Func<HttpClient>)(() =>
                {
                    var httpClient = new HttpClient(_handlerMock.Object);
                    var baseUrl = "https://localhost:7259/";
                    httpClient.BaseAddress = new Uri(baseUrl);
                    httpClient.DefaultRequestHeaders.Add("Accept", @"application/json");
                    return httpClient;
                }))());
        }
    }
}