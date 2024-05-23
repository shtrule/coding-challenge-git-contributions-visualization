using LibGit2Sharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/contributions", (string path, int numberOfDays) =>
{
    if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
    {
        return "Invalid path.";
    }
    if (!Repository.IsValid(path))
    {
        return "Invalid repository.";
    }
    return path + " " + numberOfDays;  
})
.WithName("GetContributions")
.WithOpenApi();

app.Run();
