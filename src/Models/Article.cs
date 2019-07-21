using System;
using System.ComponentModel.DataAnnotations;

namespace Librum.Models
{
    public class Article
    {
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Slug { get; set; }

        // [Required]
        public string Description { get; set; }

        [Required]
        public string Keywords { get; set; }

        [Required]
        public string Content { get; set; }
        public DateTime WritedDatetime { get; set; }
        public int ReadCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;
        public bool IsDraft { get; set; } = false;
        public bool Unlisted { get; set; } = false;
        public string AuthorUsername { get; set; }
    }
}