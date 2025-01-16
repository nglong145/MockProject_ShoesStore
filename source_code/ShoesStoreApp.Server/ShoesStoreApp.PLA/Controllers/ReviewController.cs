using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.ReviewService;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;
using System.Security.Claims;

namespace ShoesStoreApp.PLA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        [HttpGet("Get-Reviews/{productId}")]
        public async Task<IActionResult> GetReviewsByProductId(Guid productId)
        {
            var reviews = await _reviewService.GetReviewsByProductIdAsync(productId);

            if (reviews == null || !reviews.Any())
            {
                return NotFound(new { Message = "No reviews found for this product." });
            }

            return Ok(reviews);
        }

        [HttpGet("Get-Review-By-Product-User")]
        public async Task<IActionResult> GetReviewByProductUser(Guid productId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }
            Guid userId = Guid.Parse(userIdClaim);

            var review = await _reviewService.GetReviewByIdAsync(userId, productId);

            if (review == null)
            {
                return NotFound(new { Message = "Review not found." });
            }

            return Ok(review);
        }

        [HttpPost("Add-Review")]
        public async Task<IActionResult> AddReview([FromBody]AddReviewVm reviewVm)
        {

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var review = new Review
            {
                ProductId = reviewVm.ProductId,
                UserId = Guid.Parse(userIdClaim),
                Rating = reviewVm.Rating,
                ReviewText = reviewVm.ReviewText,
                CreatedDate = DateTime.UtcNow,
                Status = reviewVm.Status,

            };

            await _reviewService.AddAsync(review);

            //var productResponse = new ProductVm
            //{
            //    ProductId = product.ProductId,
            //    BrandId = product.BrandId,
            //    ProductName = product.ProductName,
            //    ProductImage = product.ProductImage,
            //    Price = product.Price,
            //    Description = product.Description,
            //    Status = product.Status
            //};
            return Ok(review);
        }

        [HttpPost("Update-Review")]
        public async Task<IActionResult> UpdateReview(Guid productId,[FromBody] AddReviewVm reviewVm)
        {

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid userId= Guid.Parse(userIdClaim);

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var updatedReview = await _reviewService.GetReviewByIdAsync(productId, userId);

            if (updatedReview != null)
            {
                updatedReview.Rating = reviewVm.Rating;
                updatedReview.ReviewText = reviewVm.ReviewText;
                updatedReview.Status = reviewVm.ProductImage;

                await _reviewService.UpdateAsync(updatedReview);
                return Ok(updatedReview);
            }

            return NotFound($"Review doesn't already exist");
        }

        [HttpDelete("Delete-Review")]
        public async Task<IActionResult> DeleteReview(Guid productId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid userId = Guid.Parse(userIdClaim);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }

            var review = await _reviewService.GetReviewByIdAsync(productId,userId);

            if (review != null)
            {
                await _reviewService.DeleteAsync(review);
                return Ok("Delete Succes");
            }

            return BadRequest("Delete Fail");

        }
    }
}
