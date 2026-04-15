using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace atelier_studio_api.Teachers.Dto;

public class CreateTeacherDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Location { get; set; }
    
    [Required]
    public List<string> Languages { get; set; }
    
    [Required]
    public IFormFile ProfilePicture { get; set; }
}

public class UpdateTeacherDto
{
    public string? Name { get; set; }
    
    public string? Location { get; set; }
    
    public List<string>? Languages { get; set; }
    
    public IFormFile? ProfilePicture { get; set; }
}