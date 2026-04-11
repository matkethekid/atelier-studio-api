using atelier_studio_api.Data;
using atelier_studio_api.Entities;
using atelier_studio_api.Reviews.Dto;
using atelier_studio_api.Reviews.ResponseModels;
using MongoDB.Bson;
using MongoDB.Driver;

namespace atelier_studio_api.Reviews;

public class ReviewService : IReviewService
{
    private readonly IMongoCollection<ReviewLink> _reviewLinks;
    private readonly IMongoCollection<Review> _reviews;
    private readonly IConfiguration _configuration;

    public ReviewService(IMongoDatabase database, MongodbService mongodbService, IConfiguration configuration)
    {
        _reviewLinks = database.GetCollection<ReviewLink>("ReviewLink");
        _reviews = database.GetCollection<Review>("Review");
        _configuration = configuration;
    }
    
    public async Task<List<Review>> GetAllReviews()
    {
        return await _reviews.Find(_ => true).ToListAsync();
    }

    public async Task<ServiceResult<string>> CreateReview(CreateReviewDto dto)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.ReviewText, dto.ReviewText);
        var existingReview = await _reviews.Find(filter).FirstOrDefaultAsync();
        if (existingReview != null)
        {
            return new ServiceResult<string>
            {
                Success = false,
                Message = "Ocena već postoji"
            };
        }
        
        var newReview = new Review
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = dto.Name,
            ReviewText = dto.ReviewText
        };
        await _reviews.InsertOneAsync(newReview);
        
        return new ServiceResult<string>
        {
            Success = true,
            Message = "Ocena je uspešno dodata",
        };
    }

    public async Task<ServiceResult<string>> CreateReviewLink()
    {
        var randomGuid = Guid.NewGuid();
        var filter = Builders<ReviewLink>.Filter.Eq(r => r.Link, randomGuid);
        var existingReviewLink = await _reviewLinks.Find(filter).FirstOrDefaultAsync();
        if (existingReviewLink != null)
        {
            return new ServiceResult<string>
            {
                Success = false,
                Message = "Link već postoji",
            };
        }

        var newReviewLink = new ReviewLink
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Link = randomGuid,
            Expired = false
        };
        await _reviewLinks.InsertOneAsync(newReviewLink);
        
        var frontendLink = _configuration["FrontendUrl"];
        var fullLink = frontendLink + "/" + randomGuid;
        
        return new ServiceResult<string>
        {
            Success = true,
            Message = "Link kreiran",
            Data = fullLink
        };
    }

    public async Task<ServiceResult<bool>> CheckReviewLink(Guid link)
    {
        var filter = Builders<ReviewLink>.Filter.Eq(r => r.Link, link);
        var existingReviewLink = await _reviewLinks.Find(filter).FirstOrDefaultAsync();
        if (existingReviewLink == null)
        {
            return new ServiceResult<bool>
            {
                Success = false,
                Message = "Link ne postoji"
            };
        }

        if (existingReviewLink.Expired == true)
        {
            return new ServiceResult<bool>
            {
                Success = false,
                Message = "Link je istekao"
            };
        }

        return new ServiceResult<bool>
        {
            Success = true,
            Message = "Link je validan",
            Data = true
        };
    }
}