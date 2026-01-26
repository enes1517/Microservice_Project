namespace ProductService.Dtos
{
    public  record class TitleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal? Price { get; set; }


     
    }
}
