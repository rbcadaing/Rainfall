using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rainfall.Web.Model
{
    /// <summary>
    /// RainfallController Controller GetRainfallStations Action Query Parameters 
    /// </summary>
    public class RainFallQuery
    {
        public string? view { get; set; }

        public string? label { get; set; }


        public string? stationReference { get; set; }

        public string? search { get; set; }

        [Required(ErrorMessage = "limit is a required field")]
        [Range(1, 100, ErrorMessage = "Accepted Values are from 1 to 100")]
        public int limit { get; set; }

        [Required(ErrorMessage = "offset is a required field")]
        public int offset { get; set; }
    }
}
