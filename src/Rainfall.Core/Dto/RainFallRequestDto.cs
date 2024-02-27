namespace Rainfall.Web.Model
{

    public class RainFallRequestDto
    {
        public string? _view { get; set; }

        public string? label { get; set; }


        public string? stationReference { get; set; }

        public string? search { get; set; }


        public string? _limit { get; set; } = "10";


        public string? _offset { get; set; } = "0";
    }
}
