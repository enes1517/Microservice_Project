using System.ComponentModel.DataAnnotations;

namespace Shared.TitleDtos
{ 
    public record UpdateTitleDto
    {
        public string id { get; init; } = null!;

        [Required(ErrorMessage = "Kitap başlığı (Title) boş bırakılamaz.")]
        [StringLength(80, ErrorMessage = "Başlık en fazla 80 karakter olabilir.")]
        public string title { get; init; } = null!;

        [Range(0, 10000, ErrorMessage = "Fiyat 0 ile 10.000 arasında olmalıdır.")]
        public decimal? price { get; init; }

        [Required(ErrorMessage = "Kitap türü belirtilmelidir.")]
        [StringLength(12)]
        public string type { get; init; } = null!;

        [StringLength(200, ErrorMessage = "Notlar 200 karakteri geçmemelidir.")]
        public string? notes { get; init; }

        [Range(0, 100, ErrorMessage = "Telif oranı (Royalty) 0-100 arasında bir yüzde olmalıdır.")]
        public int? royalty { get; init; }
    }
}
