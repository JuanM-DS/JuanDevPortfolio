using Core.Domain.Enumerables;
using Infrastructure.Persistence;
using Infrastructure.Shared;
using JuanDevPortfolio.Api.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSharedLayer()
	.AddPersistenceLayer(builder.Configuration);

builder.Services.AddLogging();
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Information()
	.WriteTo.File("Logs\\General_log.txt")
	.WriteTo.Logger(lg =>

		lg.Filter.ByIncludingOnly(f=>f.Properties.ContainsKey(LoggerKeys.RepositoryLogs.ToString()))
		.WriteTo.File($"Logs\\Infrastructure\\{LoggerKeys.RepositoryLogs}.txt")
	)
	.WriteTo.Logger(lg => 
		lg.Filter.ByIncludingOnly(p=>p.Properties.ContainsKey(LoggerKeys.AuthenticationLogs.ToString()))
		.WriteTo.File($"Logs\\Infrastructure\\{LoggerKeys.AuthenticationLogs}.txt")
	)
	.CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();
app.UseExceptionHandler();
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
