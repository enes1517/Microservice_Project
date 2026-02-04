namespace ProductService.Dtos
{
    public record class CreateTitleDto:TitleDto
    {
    public DateTime Pubdate { get; set; }



    }
}
