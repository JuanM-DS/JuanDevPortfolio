using Core.Application;
using Infrastructure.Authentication;
using Infrastructure.BackgroundServices.Services;
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
	.AddApplicationLayer(builder.Configuration)
	.AddLogExtensions()
	.AddVersioningExtensions()
	.AddSwaggerExtensions()
	.AddApiBehaviorExtensions()
	.AddExceptionHandler<GlobalExceptionHandler>()
	.AddValidationsExtensions()
	.AddHostedService<RefreshTokenBackgroundServices>()
	.AddAuthorization();

builder.Host.UseSerilog();

var app = builder.Build();
app.UseStaticFiles();
app.UseExceptionHandler(o => { });
app.UseSwaggerExtencions();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.RegisterAuthenticationSeeds();
app.Run();
