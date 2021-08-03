using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogLabModels.Blog
{
    public class BlogCreate
    {
        public int BlogId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [MinLength(10, ErrorMessage = "It must be at least 10-30 chars")]
        [MaxLength(50, ErrorMessage = "It must be at least 10-30 chars")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [MinLength(300, ErrorMessage = "It must be at least 300-1000 chars")]
        [MaxLength(1000, ErrorMessage = "It must be at least 300-1000 chars")]
        public string Content { get; set; }
        public int? PhotoId { get; set; }
  

    }
}
