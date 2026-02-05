using System.ComponentModel.DataAnnotations;

namespace Shared.AuthorDtos
{
    public record class AuthorWiewDto
    {
        [Required]
        public string Id { get; init; } = null!;

        [Required]
        public string FullName { get; init; } = null!;

        [Phone] // Telefon formatı kontrolü
        public string Phone { get; init; } = null!;

        // Location isteğe bağlı (City + State birleşimi olabilir)
        public string? Location { get; init; }

        // Kitap sayısı negatif olamaz
        [Range(0, int.MaxValue)]
        public int BookCount { get; init; }
    }
}
