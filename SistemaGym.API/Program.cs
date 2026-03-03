using Microsoft.EntityFrameworkCore;
using SistemaGym.API.Data;
using SistemaGym.API.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IMiembroService, MiembroService>();
// 🔹 Registrar DbContext
builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

// 🔹 Swagger clásico (más práctico para probar)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run(); 