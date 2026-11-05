using UserApi.Lesson08.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<UserRepository>();

// Povolí požadavky z testovací aplikace, která běží na jiné adrese.
//builder.Services.AddCors(options =>
//    options.AddDefaultPolicy(policy => policy
//        .AllowAnyOrigin()
//        .AllowAnyHeader()
//        .AllowAnyMethod()));

var app = builder.Build();

app.UseHttpsRedirection();
//app.UseCors();
app.MapControllers();

app.Run();
