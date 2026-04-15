using atelier_studio_api.Entities;
using atelier_studio_api.Teachers.Dto;
using atelier_studio_api.Teachers.ResponseModels;

namespace atelier_studio_api.Teachers;

public interface ITeachersService
{
    Task<List<Teacher>> GetTeachers();
    Task<Teacher?> GetTeacherByName(string name);
    Task<TeacherServiceResult<string>> CreateTeacher(CreateTeacherDto dto);
    Task<TeacherServiceResult<string>> DeleteTeacher(Teacher teacher);
    Task<TeacherServiceResult<string>> UpdateTeacher(Teacher teacher);
}