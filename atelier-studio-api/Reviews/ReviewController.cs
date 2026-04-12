using atelier_studio_api.Reviews.Dto;
using Microsoft.AspNetCore.Mvc;

namespace atelier_studio_api.Reviews;

[Route("reviews")]
[ApiController]
public class ReviewController(IReviewService reviewService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetReviews()
    {
        var result = await reviewService.GetAllReviews();
        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateReview(CreateReviewDto dto)
    {
        var result = await reviewService.CreateReview(dto);
        if (!result.Success)
        {
            return BadRequest(new { message = result.Message });
        }

        return Ok(result);
    }

    [HttpPost("create-review-link")]
    public async Task<IActionResult> CreateReviewLink()
    {
        var result = await reviewService.CreateReviewLink();
        if (!result.Success)
        {
            return BadRequest(new { message = result.Message });
        }

        return Ok(new { message = result.Message, link = result.Data });
    }
}