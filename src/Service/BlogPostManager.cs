using Data.Interfaces;
using Domain.Core;

namespace Service;

public class BlogPostManager
{
    private IBlogPostRepository _blogPostRepositroty;

    public BlogPostManager(IBlogPostRepository blogPostRepository) {
        _blogPostRepositroty = blogPostRepository;
    }

    public async Task<IEnumerable<BlogPostListItem>> GetBlogPostListItemsAsync(int page, int count) {
        return await _blogPostRepositroty.GetPostListItemsAsync(page, count);
    }

    public async Task<BlogPost> GetPostBySlug(string slug) {
        return await _blogPostRepositroty.GetPostBySlugAsync(slug);
    }

    public async Task<IEnumerable<string>> GetPostSlugsAsync(int page, int count) {
        return await _blogPostRepositroty.GetPostSlugsAsync(page, count);
    }
}
