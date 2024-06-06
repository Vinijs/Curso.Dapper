using Curso.Dapper.ComEntity.Api.Application.Behaviors;
using Curso.Dapper.ComEntity.Api.Data.Database.Context;
using Curso.Dapper.ComEntity.Api.Data.Database.Interfaces;
using Curso.Dapper.ComEntity.Api.Data.Database.Repositories;
using Curso.Dapper.ComEntity.Api.Data.Database.UnitOfWork;
using Curso.Dapper.ComEntity.Api.Domain.Commands.Curso;
using Curso.Dapper.ComEntity.Api.Domain.Interfaces;
using Curso.Dapper.ComEntity.Api.Domain.Services;
using Curso.Dapper.ComEntity.Api.Domain.Services.Interfaces;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CursoDapperContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<CursoDapperContext>();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
    //options.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
    //options.AddBehavior<IPipelineBehavior<RemoverCursoCommand, Unit>, UnitOfWorkRemoverCursoBehavior>();
});

builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<ICursoDapperDomainService, CursoDapperDomainService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Caso vc não possua campos nvarchar em suas tabelas, descomente a linha abaixo
//SqlMapper.AddTypeMap(typeof(string), System.Data.DbType.AnsiString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
