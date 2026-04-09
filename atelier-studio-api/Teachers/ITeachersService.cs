using atelier_studio_api.Entities;
using atelier_studio_api.Teachers.Dto;

namespace atelier_studio_api.Teachers;

public interface ITeachersService
{
    Task<List<Teacher>> GetTeachers();
    Task<Teacher?> GetTeacherByName(string name);
    Task<string> CreateTeacher(CreateTeacherDto dto);
    Task<string> DeleteTeacher(Teacher teacher);
    Task<string> UpdateTeacher(Teacher teacher);
}