using Microsoft.AspNetCore.Mvc;
using Service;
using webapi.Controllers;
using WebApi.ViewModels.Core;

namespace WebApi.Controllers {
    public class BlogPostsController : ApiController {
        private BlogPostManager _blogPostManager;

        public BlogPostsController(BlogPostManager blogPostManager) {
            _blogPostManager = blogPostManager;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetBlogPostsList(int? page, int? count) {
            try {
                return Ok(await _blogPostManager.GetBlogPostListItemsAsync(page ?? 1, count ?? 10));
            }
            catch (Exception) {
                return InternalServerError();
            }
        }

        [HttpGet("Slugs")]
        public async Task<IActionResult> GetBlogPostSlugs(int? page, int? count) {
            try {
                return Ok(await _blogPostManager.GetPostSlugsAsync(page ?? 1, count ?? 10));
            }
            catch (Exception) {
                return InternalServerError();
            }
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetPost(string slug) {
            if (string.IsNullOrEmpty(slug)) {
                return BadRequest(slug);
            }

            try {
                var post = await _blogPostManager.GetPostBySlug(slug);
                if (post == null) {
                    return NotFound();
                }

                return Ok(new BlogPostViewModel(post));
            }
            catch (Exception) {
                return InternalServerError();
            }

            
        }
    }
}
