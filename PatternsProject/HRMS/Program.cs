using ApplicationCore;
using ApplicationCore.Configuration.Mapping;
using ApplicationCore.Identity.JwtConfig;
using ApplicationCore.Models;
using HRMS.Extensions;
using HRMS.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(
			builder.Configuration.GetConnectionString("DatabaseSQL"),
			b => b.MigrationsAssembly("Infrastructure"))
		.EnableSensitiveDataLogging());

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey
	});

	options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.Configure<JwtTokenConfig>(
	builder.Configuration.GetSection(nameof(JwtTokenConfig)));

builder.Services.AddSingleton<IJwtTokenConfig>(x =>
	x.GetRequiredService<IOptions<JwtTokenConfig>>().Value);

// Identity
builder.Services.AddIdentity<UserModel, IdentityRole<Guid>>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

// Authentication
builder.Services.AddJwtAuthentication(
	builder.Configuration.GetValue<string>($"{nameof(JwtTokenConfig)}:JwtIssuer"),
	builder.Configuration.GetValue<string>($"{nameof(JwtTokenConfig)}:JwtAudience"),
	builder.Configuration.GetValue<string>($"{nameof(JwtTokenConfig)}:JwtKey"));

builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.RegisterRepositories();
builder.Services.RegisterServices();

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseDeveloperExceptionPage();
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

await app.ApplyMigrations();

app.UseHttpsRedirection();

app.AddSerilog();

app.UseCors(c =>
{
	c.AllowAnyOrigin()
	.AllowAnyHeader()
	.AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.AddIdentityRoles();

app.Run();
