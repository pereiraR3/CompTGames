using Microsoft.EntityFrameworkCore;
using web_api_dakota.Data;
using web_api_dakota.Repositories;
using web_api_dakota.Repositories.Interfaces;
using web_api_dakota.Services;
using web_api_dakota.Services.Interfaces;
using AutoMapper;
using web_api_dakota.Middleware;
using web_api_dakota.Utils.Scheduled;

var builder = WebApplication.CreateBuilder(args);

// Configure Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5074") // Domain allowed
                .AllowAnyHeader() // Allowed any header
                .AllowAnyMethod() // Allowed any method (GET, POST, PUT, DELETE, etc)
                .AllowCredentials(); // Allowed sending of the credentials 
        });
});

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });


// Configurar Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar o DbContext com a string de conexão correta
builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<WebDakotaContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database"))
    );

builder.Services.AddScoped<IAiModelRepository, AiModelRepository>();
builder.Services.AddScoped<IAiModelService, AiModelService>();

builder.Services.AddScoped<IUserModelRepository, UserModelRepository>();
builder.Services.AddScoped<IUserModelService, UserModelService>();

builder.Services.AddScoped<IDbService, DbService>();

// Injetar o repositório genérico e o serviço genérico no contêiner de dependência
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); 
builder.Services.AddScoped(typeof(IService<,,,>), typeof(Service<,,,>));

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register the service scheduled
builder.Services.AddHostedService<ScheduledTaskDbService>();

// Habilitando as anotações do Swagger
builder.Services.AddSwaggerGen(c =>
    c.EnableAnnotations()
);

var app = builder.Build();

// Configuração do pipeline para desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseMiddleware(typeof(GlobalHandlerException));

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();