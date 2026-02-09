using System.ComponentModel.DataAnnotations;

namespace Shared.AuthorDtos
{
    public class AuthorCreateDto
    {

        [Required, StringLength(40)]
        public string auLname { get; set; } = null!;

        [Required, StringLength(20)]
        public string auFname { get; set; } = null!;

        [Phone]
        public string phone { get; set; } = null!;

        public string? address { get; set; }
        public string? city { get; set; }
        public bool contract { get; set; }
    }
}
