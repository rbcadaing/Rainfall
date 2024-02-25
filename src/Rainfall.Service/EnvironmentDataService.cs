using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rainfall.Core.Common;
using Rainfall.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Service
{
    public class EnvironmentDataService : IEnvironmentDataService
    {
        private readonly ILogger<EnvironmentDataService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        

        public EnvironmentDataService(ILogger<EnvironmentDataService> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<EnvironmentDataStation> GetRainfallStations(int limit, int offset, string view)
        {
            using (var client = _httpClientFactory.CreateClient("RainfallApi"))
            {
                var endpoint = $"{_configuration["RainFallStationsEndpoint"]}&_limit={limit.ToString()}&_offset={offset.ToString()}";

                var response = await client.GetAsync(endpoint);
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EnvironmentDataStation>(content);
            }
        }
    }
}
