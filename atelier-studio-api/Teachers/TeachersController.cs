using Microsoft.AspNetCore.Mvc;

namespace atelier_studio_api.Teachers;

[Route("teachers")]
[ApiController]
public class TeachersController(ITeachersService teachersService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetTeachers()
    {
        var response = await teachersService.GetTeachers();
        return Ok(response);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetTeacherByName(string name)
    {
        var response = await teachersService.GetTeacherByName(name);
        return Ok(response);
    }
}