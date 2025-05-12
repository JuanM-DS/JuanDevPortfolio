using Core.Domain.Enumerables;
using Infrastructure.Persistence;
using Infrastructure.Shared;
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

		lg.Filter.ByIncludingOnly(f=>f.Properties.ContainsKey(LoggerKeys.Repository_Logs.ToString()))
		.WriteTo.File($"Logs\\Infrastructure\\{LoggerKeys.Repository_Logs}.txt")
	)
	.CreateLogger();
builder.Host.UseSerilog();

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
