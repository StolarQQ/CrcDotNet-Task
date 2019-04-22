using System;
using System.ComponentModel.DataAnnotations;

namespace CrcAspNetCore.Api.Models
{
    public class CreationBookDto
    {
        [Required]
        [MaxLength(150, ErrorMessage = "Title cannot contain more than 150 characters.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(450, ErrorMessage = "Description cannot contain more than 450 characters.")]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "Isbn have contained 13 digits."), MinLength(13)]
        public string Isbn { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PublishedAt { get; set; }
    }
}
