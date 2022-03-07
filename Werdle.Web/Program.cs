using Werdle.Web.Lib;
using Werdle.Web.Lib.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddMemoryCache() // TODO: remove this if replacing with distributed cache, DB, etc.
    .AddSingleton<IWordFormatValidator, WordFormatValidator>()
    .AddTransient<ICsvWordFileReader, CsvWordFileReader>()
    .AddTransient<ICsvWordSourceFactory, CsvWordSourceFactory>()
    .AddTransient<IGuessChecker, GuessChecker>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Included here for testing.
public partial class Program
{
}
