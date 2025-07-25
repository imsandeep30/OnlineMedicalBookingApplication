using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using OnlineMedicineBookingApplication.API.Controllers;
using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Services;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;
using OnlineMedicineBookingApplication.Infrastructure.DBContext;
using OnlineMedicineBookingApplication.Infrastructure.Repositories;
namespace OnlineMedicineBookingApplication.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<MedicineAppContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            //Medicine ervice and Repository
            builder.Services.AddScoped<IMedicineService, MedicineService>();
            builder.Services.AddScoped<IMedicineContract, MedicineRepository>();
            //Cart Service and Repository
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<ICartContract, CartRepository>();
            //User Service and Repository
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserContract, UserRepository>();
            //Transaction Service and Repository
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<ITransactionContract, TransactionRepository>();
            //Order Service and Repository
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderContract, OrderRepository>();
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

        }
    }
}