using Microsoft.EntityFrameworkCore;
using web_api_dakota.Data;
using web_api_dakota.Repositories;
using web_api_dakota.Repositories.Interfaces;
using web_api_dakota.Services;
using web_api_dakota.Services.Interfaces;
using AutoMapper;
using web_api_dakota.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar o DbContext com a string de conexão correta
builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<WebDakotaContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database"))
    );

// Injetar o repositório genérico e o serviço genérico no contêiner de dependência
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); 
builder.Services.AddScoped(typeof(IService<,,,>), typeof(Service<,,,>));

// Registrar AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

// Middleware para CORS (se necessário)
// app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// app.UseMiddleware(typeof(GlobalHandlerException));

app.UseAuthorization();

app.MapControllers();

app.Run();