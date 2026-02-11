namespace Shared.RequestParameters
{
    public class TitleRequestParameters:CammonParameters
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public decimal? Price { get; set; }
    }
}
