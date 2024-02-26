namespace Rainfall.Web.Model
{

    public class RainFallRequestDto
    {
        public string? view { get; set; }

        public string? label { get; set; }


        public string? stationReference { get; set; }

        public string? search { get; set; }


        public int limit { get; set; }


        public int offset { get; set; }
    }
}
