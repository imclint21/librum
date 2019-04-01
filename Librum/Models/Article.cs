using System;

namespace Librum.Models
{
    public class Article
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Content { get; set; }
        public DateTime WritedDatetime { get; set; }
        public bool IsDraft { get; set; } = false;
        public bool Unlisted { get; set; } = false;
        public string AuthorUsername { get; set; }
    }
}