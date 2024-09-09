using System.Security.Claims;
using library_system.Context;
using library_system.DataAccess.Repositories;
using library_system.DataAccess.UnitOfWork;
using library_system.Extensions;
using library_system.Factories;
using library_system.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();



builder.Services.AddControllers().AddJsonOptions(options =>
{
	//Support enum in JSON payload so a enum string can be mapped properly.
	options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerWithAuth(builder.Configuration);

builder.Services.AddDbContext<LibraryDbContext>(options =>
	options.UseInMemoryDatabase("LibraryDB"));

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddTransient<IBookFactory, AudioBookFactory>();
builder.Services.AddTransient<IBookFactory, PaperBackBookFactory>();
builder.Services.AddTransient<BookFactoryResolver>();

builder.Services.AddTransient<IDiscountRateFactory, DiscountRateFactory>();
builder.Services.AddTransient<AudioBookDiscountStrategy>();
builder.Services.AddTransient<PaperBackDiscountStrategy>();
builder.Services.AddTransient<DefaultDiscountStrategy>();

//Add support for Json Web Token.
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.Audience = builder.Configuration["Authentication:Audience"];
        x.MetadataAddress = builder.Configuration["Authentication:MetaDataAddress"]!;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"]
        };
    });

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGet("users/me", (ClaimsPrincipal claimsPrincipal) =>
{
    return claimsPrincipal.Claims.ToDictionary(c => c.Type, c => c.Value);
}).RequireAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
