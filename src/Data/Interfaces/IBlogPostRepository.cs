using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces {
    public interface IBlogPostRepository {
        Task<IEnumerable<BlogPostListItem>> GetPostListItemsAsync(int page, int count);
        Task<BlogPost> GetPostBySlugAsync(string slug);
        Task<IEnumerable<string>> GetPostSlugsAsync(int page, int count);
    }
}
