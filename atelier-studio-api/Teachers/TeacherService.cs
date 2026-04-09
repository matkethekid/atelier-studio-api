using atelier_studio_api.Data;
using atelier_studio_api.Entities;
using atelier_studio_api.Teachers.Dto;
using MongoDB.Driver;

namespace atelier_studio_api.Teachers;

public class TeacherService : ITeachersService
{
    private readonly IMongoCollection<Teacher> _teachers;

    public TeacherService(IMongoDatabase database, MongodbService mongodbService)
    {
        _teachers = database.GetCollection<Teacher>("Teachers");
    }

    public async Task<List<Teacher>> GetTeachers()
    {
        return await _teachers.Find(_ => true).ToListAsync();
    }

    public async Task<Teacher?> GetTeacherByName(string name)
    {
        var filter = Builders<Teacher>.Filter.Eq(t => t.Name, name);
        return await _teachers.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<string> CreateTeacher(CreateTeacherDto dto)
    {
        return "Predavač uspešno kreiran";
    }

    public async Task<string> DeleteTeacher(Teacher teacher)
    {
        return "Predavač obrisan";
    }

    public async Task<string> UpdateTeacher(Teacher teacher)
    {
        return "Predavač izmenjen";
    }
}