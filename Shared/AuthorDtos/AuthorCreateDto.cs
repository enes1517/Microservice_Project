using System.ComponentModel.DataAnnotations;

namespace Shared.AuthorDtos
{
    public class AuthorCreateDto
    {
        [Required(ErrorMessage = "Yazar ID zorunludur.")]
        public string AuId { get; set; } = null!;

        [Required, StringLength(40)]
        public string AuLname { get; set; } = null!;

        [Required, StringLength(20)]
        public string AuFname { get; set; } = null!;

        [Phone]
        public string Phone { get; set; } = null!;

        public string? Address { get; set; }
        public string? City { get; set; }
        public bool Contract { get; set; }
    }
}
