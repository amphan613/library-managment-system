using library_system.Context;
using library_system.DataAccess.Repositories;
using library_system.DataAccess.UnitOfWork;
using library_system.Factories;
using library_system.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers().AddJsonOptions(options =>
{
	//Support enum in JSON payload so a enum string can be mapped properly.
	options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();

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
