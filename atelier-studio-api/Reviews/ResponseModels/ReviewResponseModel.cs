namespace atelier_studio_api.Reviews.ResponseModels;

public class ServiceResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}