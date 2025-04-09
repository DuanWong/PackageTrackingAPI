using Microsoft.EntityFrameworkCore;
using PackageTrackingAPI.BLL;
using PackageTrackingAPI.DAL;
using PackageTrackingAPI.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Entity Framework Core
builder.Services.AddDbContext<PackageTrackingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Register Services
builder.Services.AddScoped<UserService>();

// Register Repositories
builder.Services.AddScoped<UserRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();