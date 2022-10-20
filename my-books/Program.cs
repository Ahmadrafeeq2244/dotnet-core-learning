using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Data.Services;
using my_books.Exceptions;
using Serilog;
using Microsoft.Extensions.Logging;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConectionString")));

// Add services to the container.




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<BookService>();
builder.Services.AddTransient<AuthorService>();
builder.Services.AddTransient<PublisherService>();
builder.Services.AddTransient<LogService>();
builder.Services.AddApiVersioning(config=>
{
    config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;

    //config.ApiVersionReader = new HeaderApiVersionReader("custom-version-header
    config.ApiVersionReader = new HeaderApiVersionReader();
});

var configuration=new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    
    .Build();
var logger = new LoggerConfiguration()
.ReadFrom.Configuration(configuration)
.Enrich.FromLogContext()
.CreateLogger();

//var logger = new LoggerConfiguration()
//.WriteTo.File("Logs/log.txt",rollingInterval:RollingInterval.Day)
//.CreateLogger();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//builder.Host.UseSerilog();
//var app = builder.Build();



var app = builder.Build();
var lf = app.Services.GetRequiredService<ILoggerFactory>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Exception Handling
//using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
//    .AddConfiguration(configuration)
//    .AddApplicationInsights());



app.configureBuildInExceptionHandler(lf);
//app.ConfigureCustomeExceptionHandler();
app.MapControllers();
//AppDbInitializer.seed(app);
app.Run();

