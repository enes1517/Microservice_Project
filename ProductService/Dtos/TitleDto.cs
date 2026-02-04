using System.ComponentModel.DataAnnotations;

namespace ProductService.Dtos
{
    public  record class TitleDto
    {
        [Required(ErrorMessage = "Kitap ID zorunludur.")]
        [StringLength(6, ErrorMessage = "ID en fazla 6 karakter olabilir.")]
        public string Id { get; init; } = null!;

        [Required(ErrorMessage = "Kitap başlığı (Title) boş bırakılamaz.")]
        [StringLength(80, ErrorMessage = "Başlık en fazla 80 karakter olabilir.")]
        public string Title { get; init; } = null!;

        [Range(0, 10000, ErrorMessage = "Fiyat 0 ile 10.000 arasında olmalıdır.")]
        public decimal? Price { get; init; }

        [Required(ErrorMessage = "Kitap türü belirtilmelidir.")]
        [StringLength(12)]
        public string Type { get; init; } = null!;

        [StringLength(200, ErrorMessage = "Notlar 200 karakteri geçmemelidir.")]
        public string? Notes { get; init; }

        [Range(0, 100, ErrorMessage = "Telif oranı (Royalty) 0-100 arasında bir yüzde olmalıdır.")]
        public int? Royalty { get; init; }



    }
}
