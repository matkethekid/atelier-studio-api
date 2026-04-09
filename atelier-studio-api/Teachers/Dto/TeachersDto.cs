using System.ComponentModel.DataAnnotations;

namespace atelier_studio_api.Teachers.Dto;

public class CreateTeacherDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Location { get; set; }
    
    [Required]
    public List<string> Languages { get; set; }
}