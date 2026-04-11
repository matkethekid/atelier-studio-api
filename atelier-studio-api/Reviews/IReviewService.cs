using atelier_studio_api.Entities;
using atelier_studio_api.Reviews.Dto;
using atelier_studio_api.Reviews.ResponseModels;

namespace atelier_studio_api.Reviews;

public interface IReviewService
{
    Task<List<Review>> GetAllReviews();
    Task<ServiceResult<string>> CreateReview(CreateReviewDto dto);
    Task<ServiceResult<string>> CreateReviewLink();
    Task<ServiceResult<bool>> CheckReviewLink(Guid link);
}