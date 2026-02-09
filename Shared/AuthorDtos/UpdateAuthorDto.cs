using System.ComponentModel.DataAnnotations;

namespace Shared.AuthorDtos
{
    public record class UpdateAuthorDto
    {
        public string Id { get; init; } = null!;

        [Required(ErrorMessage = "Soyadı boş bırakılamaz.")]
        [StringLength(40)]
        public string AuLName { get; init; } = null!;

        [Required(ErrorMessage = "Ad boş bırakılamaz.")]
        [StringLength(20)]
        public string AuFname { get; init; } = null!;

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string Phone { get; init; } = null!;

        public string? Address { get; init; }

        public string? City { get; init; }

        [Required]
        public bool Contract { get; init; }
    }
}
