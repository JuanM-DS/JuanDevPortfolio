using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Infrastructure.Shared;
using JuanDevPortfolio.Api.Extensions;
using JuanDevPortfolio.Api.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions {WebRootPath = "Media"});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
	.AddSharedLayer(builder.Configuration)
	.AddPersistenceLayer(builder.Configuration)
	.AddAuthenticationLayer(builder.Configuration)
	.AddLogExtensions()
	.AddVersioningExtensions()
	.AddSwaggerExtensions()
	.AddExceptionHandler<GlobalExceptionHandler>();

builder.Host.UseSerilog();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler(o => { });
app.UseSwaggerExtencions();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
