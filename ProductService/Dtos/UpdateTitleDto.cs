namespace ProductService.Dtos
{ 
    public record class UpdateTitleDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }


    }
}
