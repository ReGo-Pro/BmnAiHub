using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core {
    public class BlogPostListItem {
        public string slug { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string BannerUri { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Category { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string AuthorProfilePictureUri { get; set; }
    }
}