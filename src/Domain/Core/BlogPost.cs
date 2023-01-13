using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core {
    public class BlogPost : Content {
        public string BannerImageName { get; set; }
        public string HtmlContent { get; set; }

        public virtual List<Tag> Tags { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public BlogPostListItem ToListItem() {
            if (this.Status != ContentStatus.Published) {
                throw new InvalidOperationException("Only published posts can be list items");
            }

            return new BlogPostListItem() {
                Author = this.Author.FullName,
                // TODO: replace with real value after creating UserProfile table
                AuthorProfilePictureUri = "/Media/UserProfile/ReGo.png",
                BannerUri = this.BannerImageName,
                CreatedAt = this.CreatedAt,
                PublishedAt = this.publishedAt.Value,
                Title = this.Title,
                Content = this.TextContent,
                Category = this.Category.Name,
                Tags = this.Tags?.Select(t => t.Name).ToList()
            };
        }
    }
}
