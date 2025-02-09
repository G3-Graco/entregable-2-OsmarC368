using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IPersonajeService), typeof(PersonajeService));
builder.Services.AddScoped(typeof(IPersonajeRepository), typeof(PersonajeRepository));
builder.Services.AddScoped(typeof(IHabilidadService), typeof(HabilidadService));
builder.Services.AddScoped(typeof(IHabilidadRepository), typeof(HabilidadRepository));
builder.Services.AddScoped(typeof(IEquipoService), typeof(EquipoService));
builder.Services.AddScoped(typeof(IEquipoRepository), typeof(EquipoRepository));
builder.Services.AddScoped(typeof(IMisionService), typeof(MisionService));
builder.Services.AddScoped(typeof(IMisionRepository), typeof(MisionRepository));
builder.Services.AddScoped(typeof(IUbicacionService), typeof(UbicacionService));
builder.Services.AddScoped(typeof(IUbicacionRepository), typeof(UbicacionRepository));
builder.Services.AddScoped(typeof(IObjetoService), typeof(ObjetoService));
builder.Services.AddScoped(typeof(IObjetoRepository), typeof(ObjetoRepository));

builder.Services.AddDbContext<AppDbContext>(patata =>
        patata.UseNpgsql("Host=dpg-cu8lfdhu0jms738cjl4g-a;Server=dpg-cu8lfdhu0jms738cjl4g-a.oregon-postgres.render.com;Port=5432;Database=netcore2025graco;Username=netcore2025graco_user;Password=poip27oYyj7iu9y7oxpkXezLliJrsyIh",
        b => b.MigrationsAssembly("Infrastructure")
        ));


//builder.Services.AddDbContext<AppDbContext>(options =>
//                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
//            );

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();

