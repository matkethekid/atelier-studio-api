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
        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPost("create-review-link")]
    public async Task<IActionResult> CreateReviewLink()
    {
        var result = await reviewService.CreateReviewLink();
        if (result == null)
        {
            return BadRequest(new { message = "Link već postoji" });
        }

        return Ok(new { message = "Link je uspešno kreiran", link = result });
    }
}