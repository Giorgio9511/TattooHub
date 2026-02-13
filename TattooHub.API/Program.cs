using TattooHub.Application.Services;
using FluentValidation;
using TattooHub.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Infrastructure (DB, Repositories)
builder.Services.AddInfrastructure(builder.Configuration);

//ApplicationServices
builder.Services.AddScoped<ArtistService>();

//FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<TattooHub.Application.AssemblyReference>();

//CORS para desarrollo
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
         .AllowAnyHeader()
         .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
