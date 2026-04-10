using System.ComponentModel.DataAnnotations;

namespace atelier_studio_api.Reviews.Dto;

public class CreateReviewDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string ReviewText { get; set; }
}