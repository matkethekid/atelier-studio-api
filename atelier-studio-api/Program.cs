using atelier_studio_api.Data;
using atelier_studio_api.Reviews;
using atelier_studio_api.Teachers;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var mongoService = scope.ServiceProvider.GetRequiredService<MongodbService>();
    await mongoService.CreateIndexes();
}
builder.Services.AddScoped<ITeachersService, TeacherService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

app.MapGet("/", () => "Hello World!");

app.Run();
