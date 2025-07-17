using OnlineMedicineBookingApplication.API.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<OnlineMedicineBookingApplication.Application.Interfaces.IMedicineService, OnlineMedicineBookingApplication.Application.Services.MedicineService>();
builder.Services.AddScoped<OnlineMedicineBookingApplication.Infrastructure.Contracts.IMedicineContract, OnlineMedicineBookingApplication.Infrastructure.Repositories.MedicineRepository>();
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
