namespace ProductService.Dtos
{
    public  record class TitleDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
        public decimal? Price { get; set; }
        public decimal? Advance { get; set; }
        public int? Royalty { get; set; }



    }
}
