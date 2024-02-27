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

        public async Task<EnvironmentDataStation> GetRainfallStations(Dictionary<string, string> QParams)
        {
            using (var client = _httpClientFactory.CreateClient("RainfallApi"))
            {
                var endpoint = $"{_configuration["RainFallStationsEndpoint"]}?parameter=rainfall&{QueryStringBuilder(QParams)}";

                var response = await client.GetAsync(endpoint);
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EnvironmentDataStation>(content);
            }
        }

        public string QueryStringBuilder(Dictionary<string, string> QParams)
        {
            StringBuilder _qParams = new StringBuilder("");
            foreach (var kvp in QParams)
            {
                if (kvp.Value != null)
                {
                    _qParams.Append($"{kvp.Key}={kvp.Value}&");
                }
            }
            return _qParams.ToString().Substring(0, _qParams.Length - 1);
        }
    }
}
