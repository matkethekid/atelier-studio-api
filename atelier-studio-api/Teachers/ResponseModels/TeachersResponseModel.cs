namespace atelier_studio_api.Teachers.ResponseModels;

public class TeacherServiceResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}