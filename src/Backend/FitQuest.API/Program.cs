using FitQuest.API.Filters;
using FitQuest.API.Middleware;
using FitQuest.Application;
using FitQuest.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Adiciona o filtro de exce��o
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));


// Inje��o de Depend�ncia
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddApplication();


// Configura��es pr� build devem ir acima
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
