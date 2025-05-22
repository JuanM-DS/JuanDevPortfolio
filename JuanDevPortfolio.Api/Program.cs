using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Infrastructure.Shared;
using JuanDevPortfolio.Api.Extensions;
using JuanDevPortfolio.Api.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
	WebRootPath = "Images",
	Args = args,
	ApplicationName = typeof(Program).Assembly.FullName,
	ContentRootPath = Directory.GetCurrentDirectory(),
	EnvironmentName = Environments.Staging
});


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
app.UseStaticFiles(new StaticFileOptions { RequestPath = "/contentRoot" });
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
