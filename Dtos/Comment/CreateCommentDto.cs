using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required] [StringLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required][StringLength(500)]
        public string Content { get; set; } = string.Empty;
    }
}
