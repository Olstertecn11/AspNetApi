using LaCazuelaChapinaAPI.Services;
using LaCazuelaChapinaAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // tu frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Agregar el servicio DatabaseService
builder.Services.AddScoped<DatabaseService>();

// Configuración de DbContext con PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar controladores, swagger y documentación de la API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowReactApp");
// Habilitar Swagger y SwaggerUI en entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization(); // Configurar autorización
app.MapControllers(); // Mapear los controladores

app.Run(); // Ejecutar la aplicación
