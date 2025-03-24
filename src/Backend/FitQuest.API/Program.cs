using FitQuest.API.Filters;
using FitQuest.API.Middleware;
using FitQuest.Application;
using FitQuest.Infraestructure;
using FitQuest.Infraestructure.Extension;
using FitQuest.Infraestructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Adiciona o filtro de exceção
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));


// Injeção de Dependência
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);


// Configurações pré build devem ir acima
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

MigrateDatabase();

app.Run();

void MigrateDatabase()
{
    if (builder.Configuration.IsUnityTestEnviroment())
        return; // Se for teste de unidade, não executa o resto

    var connectionString = builder.Configuration.ConnectionString();

    // Cria o escopo para utilizarmos o serviço de injeção de depedência
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    DatabaseMigration.Migrate(connectionString, serviceScope.ServiceProvider);
}

public partial class Program()
{    
}