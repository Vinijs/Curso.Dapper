using Curso.Dapper.Api.Data.Database.TypeHandlers;
using Curso.Dapper.Api.Enums;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SqlMapper.AddTypeHandler(new EmailTypeHandler());
SqlMapper.RemoveTypeMap(typeof(TipoCurso));
SqlMapper.AddTypeHandler(new EnumTypeHandler<TipoCurso>());

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
