using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Infrastructure.Shared;
using JuanDevPortfolio.Api.Extensions;
using JuanDevPortfolio.Api.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions {WebRootPath = "Media"});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
	.AddSharedLayer(builder.Configuration)
	.AddPersistenceLayer(builder.Configuration)
	.AddAuthenticationLayer(builder.Configuration)
	.AddLogExtensions()
	.AddVersioningExtensions()
	.AddExceptionHandler<GlobalExceptionHandler>();

builder.Host.UseSerilog();

var app = builder.Build();
app.UseExceptionHandler(o => { });
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
