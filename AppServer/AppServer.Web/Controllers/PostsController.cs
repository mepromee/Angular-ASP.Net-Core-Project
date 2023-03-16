using AppServer.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppServer.Web.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] List<string> tags, [FromQuery] string? sortBy = "id", [FromQuery] string? direction = "asc")
        {
            var posts = await _postService.GetPostsAsync(tags, sortBy, direction);
            return Ok(posts);
        }
    }
}
