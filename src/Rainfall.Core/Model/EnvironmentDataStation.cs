using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Core.Model
{
    public class EnvironmentDataStation
    {
        [JsonProperty("@context")]
        public string? context { get; set; }
        public Meta? meta { get; set; }
        public List<Item>? items { get; set; }
    }

    public class Item
    {
        [JsonProperty("@id")]
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

    public class Measure
    {
        [JsonProperty("@id")]
        public string? id { get; set; }
        public string? parameter { get; set; }
        public string? parameterName { get; set; }
        public int period { get; set; }
        public string? qualifier { get; set; }
        public string? unitName { get; set; }
    }

    public class Meta
    {
        public string? publisher { get; set; }
        public string? licence { get; set; }
        public string? documentation { get; set; }
        public string? version { get; set; }
        public string? comment { get; set; }
        public List<string>? hasFormat { get; set; }
        public int limit { get; set; }
    }
}
