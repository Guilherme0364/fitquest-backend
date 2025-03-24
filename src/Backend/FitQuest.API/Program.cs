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


// Adiciona o filtro de exce��o
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));


// Inje��o de Depend�ncia
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);


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

MigrateDatabase();

app.Run();

void MigrateDatabase()
{
    if (builder.Configuration.IsUnityTestEnviroment())
        return; // Se for teste de unidade, n�o executa o resto

    var connectionString = builder.Configuration.ConnectionString();

    // Cria o escopo para utilizarmos o servi�o de inje��o de deped�ncia
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    DatabaseMigration.Migrate(connectionString, serviceScope.ServiceProvider);
}

public partial class Program()
{    
}