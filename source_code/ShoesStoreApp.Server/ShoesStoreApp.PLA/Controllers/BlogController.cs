using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.BlogService;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.PLA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("get-all-blog")]
        public async Task<IActionResult> GetAllBlog()
        {
            var blogs = await _blogService.GetAllAsync();
            var blogVm = new List<BLogVm>();
            foreach (var blog in blogs)
            {
                blogVm.Add(new BLogVm
                {
                    BlogId = blog.BlogId,
                    Title = blog.Title,
                    BlogImage = blog.BlogImage,
                    Description = blog.Description,
                    Detail = blog.Detail,
                    CreatedDate = blog.CreatedDate
                });
            }
            return Ok(blogVm);
        }

        [HttpGet("get-blog-by-id/{id}")]
        public async Task<IActionResult> GetBlogById(Guid id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if(blog != null)
            {
                var blogVm = new BLogVm()
                {
                    BlogId = blog.BlogId,
                    Title = blog.Title,
                    BlogImage = blog.BlogImage,
                    Description = blog.Description,
                    Detail = blog.Detail,
                    CreatedDate = blog.CreatedDate
                };
                return Ok(blogVm);
            }
            return BadRequest("The blog does not exist!");
        }

        [HttpPost("add-new-blog")]
        public async Task<IActionResult> AddNewBlog([FromBody] AddBlogVm addBlogVm)
        {
            var blog = new Blog()
            {
                BlogId = Guid.NewGuid(),
                Title = addBlogVm.Title,
                BlogImage = addBlogVm.BlogImage,
                Description = addBlogVm.Description,
                Detail = addBlogVm.Detail,
                CreatedDate = addBlogVm.CreatedDate,
            };
            await _blogService.AddAsync(blog);
            return Ok(blog);
        }

        [HttpPut("update-blog/{id}")]
        public async Task<IActionResult> UpdateBlog(Guid id, [FromBody] AddBlogVm addBlogVm)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if(blog != null)
            {
                blog.Title = addBlogVm.Title;
                blog.BlogImage = addBlogVm.BlogImage;
                blog.Description = addBlogVm.Description;
                blog.Detail = addBlogVm.Detail;
                blog.CreatedDate = addBlogVm.CreatedDate;

                await _blogService.UpdateAsync(blog);
                return Ok(blog);
            }
            return BadRequest("The blog does not exist!");
        }

        [HttpDelete("delete-blog/{id}")]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if(blog != null)
            {
                await _blogService.DeleteAsync(blog);
                return Ok(blog);
            }
            return BadRequest("Delete Faild!");
        }

    }
}
