using atelier_studio_api.Data;
using atelier_studio_api.Entities;
using atelier_studio_api.Teachers.Dto;
using MongoDB.Driver;
using MongoDB.Bson;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using atelier_studio_api.Teachers.ResponseModels;
using StackExchange.Redis.Extensions.Core.Abstractions;
using PutObjectRequest = Amazon.S3.Model.PutObjectRequest;

namespace atelier_studio_api.Teachers;

public class TeacherService : ITeachersService
{
    private readonly IMongoCollection<Teacher> _teachers;
    private readonly IConfiguration _configuration;
    private readonly BasicAWSCredentials _credentials;
    private readonly IAmazonS3 _s3Client;
    private readonly IRedisDatabase _redis;
    private const string BucketNameConst = "atelier-studio";
    private const string DisplayUrl = "";

    public TeacherService(IMongoDatabase database, MongodbService mongodbService, IConfiguration configuration)
    {
        _teachers = database.GetCollection<Teacher>("Teachers");

        var accessKey = configuration["MinIO:AccessKey"];
        var secretKey = configuration["MinIO:SecretKey"];
        _credentials = new BasicAWSCredentials(accessKey, secretKey);
        _s3Client = new AmazonS3Client(_credentials, new AmazonS3Config
        {
            ServiceURL = "",
            ForcePathStyle = true
        });
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

    public async Task<TeacherServiceResult<string>> CreateTeacher(CreateTeacherDto dto)
    {
        var filter = Builders<Teacher>.Filter.Eq(t => t.Name, dto.Name);
        var existingTeacher = await _teachers.Find(filter).FirstOrDefaultAsync();
        if (existingTeacher != null)
        {
            return new TeacherServiceResult<string>
            {
                Success = false,
                Message = $"Predavač sa imenom {dto.Name} već postoji"
            };
        }

        using var stream = dto.ProfilePicture.OpenReadStream();
        var fileName = Guid.NewGuid() + "-" + dto.Name + Path.GetExtension(dto.ProfilePicture.FileName);
        var profilePictureRequest = new PutObjectRequest
        {
            BucketName = BucketNameConst,
            Key = fileName,
            InputStream = stream,
            ContentType = dto.ProfilePicture.ContentType
        };
        
        var awsResponse = await _s3Client.PutObjectAsync(profilePictureRequest);
        if (awsResponse.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            return new TeacherServiceResult<string>
            {
                Success = false,
                Message = "Greška prilikom uploadanja slike"
            };
        }

        var newTeacher = new Teacher
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = dto.Name,
            Location = dto.Location,
            Languages = dto.Languages,
            ProfilePictureUrl = $"{DisplayUrl}/{BucketNameConst}/{fileName}"
        };
        await _teachers.InsertOneAsync(newTeacher);
        
        return new TeacherServiceResult<string>
        {
            Success = true,
            Message = "Predavač uspešno kreiran"
        };
    }

    public async Task<TeacherServiceResult<string>> DeleteTeacher(Teacher teacher)
    {
        var filter = Builders<Teacher>.Filter.Eq(t => t.Id, teacher.Id);
        var existingTeacher = await _teachers.Find(filter).FirstOrDefaultAsync();
        if (existingTeacher == null)
        {
            return new TeacherServiceResult<string>
            {
                Success = false,
                Message = "Predavač nije pronađen"
            };
        }

        await _teachers.DeleteOneAsync(existingTeacher.Id);
        
        return new TeacherServiceResult<string>
        {
            Success = true,
            Message = "Predavač obrisan"
        };
    }

    public async Task<TeacherServiceResult<string>> UpdateTeacher(Teacher teacher)
    {
        return new TeacherServiceResult<string>
        {
            Success = true,
            Message = "Predavač izmenjen"
        };
    }
}