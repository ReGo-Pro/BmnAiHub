using Core;
using Domain.Core;

namespace WebApi.ViewModels.Core {
    public class BlogPostViewModel {
        public BlogPostViewModel(BlogPost post) {
            Title = post.Title;
            TextContent = post.TextContent;
            PublishedAt = post.publishedAt.Value.ToString("yyyy/MM/dd");
            Author = post.Author.FullName;
            BannerImageName = post.BannerImageName;
        }

        public string Title { get; set; }
        public string TextContent { get; set; }
        public string PublishedAt { get; set; }
        public string Author { get; set; }
        public string BannerImageName { get; set; }
    }
}
