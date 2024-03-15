using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
    public class UpdateCommentDto
    {
        [Required]
        [MaxLength(255)]
        [MinLength(5)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(255)]
        [MinLength(5)]
        public string Content { get; set; } = string.Empty;
    }
}
