using atelier_studio_api.Entities;
using atelier_studio_api.Reviews.Dto;

namespace atelier_studio_api.Reviews;

public interface IReviewService
{
    Task<List<Review>> GetAllReviews();
    Task<string?> CreateReview(CreateReviewDto dto);
    Task<string?> CreateReviewLink();
}