using ParkingApi.Repositories;
using ParkingApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<ParkingLotRepository>();
builder.Services.AddSingleton<ParkingSpotRepository>();
builder.Services.AddScoped<ParkingLotService>();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy => policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();

app.Run();
