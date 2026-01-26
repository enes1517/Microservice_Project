namespace ProductService.Dtos
{
    public record class CreateTitleDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

    }
}
