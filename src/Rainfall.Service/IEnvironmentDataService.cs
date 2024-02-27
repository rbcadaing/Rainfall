using Rainfall.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Service
{
    public interface IEnvironmentDataService
    {
        Task<EnvironmentDataStation> GetRainfallStations(Dictionary<string,string> QParams);
    }
}
