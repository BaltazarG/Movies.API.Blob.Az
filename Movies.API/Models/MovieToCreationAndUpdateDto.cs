using Movies.API.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Movies.API.Models
{
    public class MovieToCreationAndUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int Valoration { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile Image { get; set; }

        [AllowedExtensions(new string[] { ".xls", ".xlt", ".xml", ".xlam", ".xlw", ".xlr", ".xlsx" })]
        public IFormFile Excel { get; set; }


        [AllowedExtensions(new string[] { ".pdf" })]
        public IFormFile Pdf { get; set; }
    }
}
