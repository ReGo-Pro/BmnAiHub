using Core;
using data;
using data.Repositories;
using Data.Interfaces;
using Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories {
    public class BlogPostRepository : Repository<BlogPost, int>, IBlogPostRepository {
        public BlogPostRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<BlogPostListItem>> GetPostListItemsAsync(int page, int count) {
            // TODO: check query with profiler and optimize
            return await Context.BlogPosts
                .Where(p => p.Status == ContentStatus.Published)
                .OrderByDescending(p => p.publishedAt)
                .Skip((page - 1) * count)
                .Take(count)
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Select(p => new BlogPostListItem() {
                    slug = p.Slug,
                    Author = p.Author.FullName,
                    // TODO: replace with real value after creating UserProfile table
                    AuthorProfilePictureUri = "rego.png",
                    BannerUri = p.BannerImageName,
                    CreatedAt = p.CreatedAt,
                    PublishedAt = p.publishedAt.Value,
                    Title = p.Title,
                    Content = p.TextContent,
                    Category = p.Category.Name,
                    Tags = p.Tags.Select(t => t.Name).ToList()
                })
                .ToListAsync();
        }

        public async Task<BlogPost> GetPostBySlugAsync(string slug) {
            return await Context.BlogPosts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .SingleOrDefaultAsync(p => p.Slug == slug);
        }

        public async Task<IEnumerable<string>> GetPostSlugsAsync(int page, int count) {
            return await Context.BlogPosts
                .Where(p => p.Status == ContentStatus.Published)
                .OrderByDescending(p => p.publishedAt)
                .Skip((page - 1) * count)
                .Take(count)
                .Select(p => p.Slug)
                .ToListAsync();
        }

        protected override AppDbContext Context => base.Context as AppDbContext;
    }
}