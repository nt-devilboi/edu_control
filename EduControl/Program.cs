using System.Xml;
using EduControl.Controllers.AppController;
using EduControl.Controllers.AppController.Model;
using EduControl.Controllers.Model;
using EduControl.DataBase;
using EduControl.DataBase.ModelBd;
using EduControl.MiddleWare;
using EduControl.Repositories;
using EduControl.Repositories.InMemory;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;
using Vostok.Logging.Microsoft;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var log = new ConsoleLog(new ConsoleLogSettings()
{
    ColorsEnabled = true
});
builder.Logging.ClearProviders();
builder.Logging.AddVostok(log);
builder.Services.AddSingleton<ILog>(log);
builder.Services.AddControllers();

builder.Services.AddSingleton<IRepository<Book>, BookRepository>();
builder.Services.AddSingleton<IRepository<Status>, StatusRepository>();
builder.Services.AddSingleton<IEventRepository, EventRepository>();
builder.Services.AddSingleton<ITokenRepository, TokenRepository>();
builder.Services.AddSingleton<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton<IStatisticsService, StatisticsService>();
builder.Services.AddSingleton<ControlTimeDb>();
builder.Services.AddSingleton<ManageTime>();
builder.Services.AddScoped<AccountScope>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWhen(x => x.Request.Path.StartsWithSegments("/api"), c =>
{
    c.UseMiddleware<MiddleWareAuthUser>();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();