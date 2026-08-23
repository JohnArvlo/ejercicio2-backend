using caso2_solucion.application.Interfaces;
using caso2_solucion.infrastructure.Repositories;
using caso2_solucion.infrastructure.Persistence;
using Microsoft.EntityFrameworkCore; // ajusta esto al namespace real donde tengas tu ApplicationDbContext

using System.Text.Json.Serialization; //para los enums

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Base de datos 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository 
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ISupplierRepository).Assembly));

builder.Services.AddAutoMapper(cfg => { }, typeof(ISupplierRepository).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}


app.Run();