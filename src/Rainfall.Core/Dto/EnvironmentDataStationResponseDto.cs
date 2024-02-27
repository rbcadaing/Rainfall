using Rainfall.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Core.Dto
{
    public class EnvironmentDataStationResponseDto
    {
        public string? id { get; set; }
        public string? RLOIid { get; set; }
        public string? catchmentName { get; set; }
        public string? dateOpened { get; set; }
        public int easting { get; set; }
        public string? label { get; set; }
        public double lat { get; set; }
        public double @long { get; set; }
        public List<Measure>? measures { get; set; }
        public int northing { get; set; }

        public string? notation { get; set; }
        public string? riverName { get; set; }
        public string? stageScale { get; set; }
        public string? stationReference { get; set; }
        public string? status { get; set; }
        public string? town { get; set; }
        public string? wiskiID { get; set; }
    }
}
