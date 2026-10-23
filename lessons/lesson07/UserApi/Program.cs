using UserApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Jedna instance pro celou aplikaci, aby uložení uživatelé přežili mezi požadavky.
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<ProductRepository>();

// Povolí požadavky z testovací aplikace, která běží na jiné adrese.
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
