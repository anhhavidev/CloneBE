using AutoMapper;
using CloneBE.Application.Interface.Serivce;
using CloneBE.Application.Mapper;
using CloneBE.Domain.InterfaceRepo;
using CloneBE.Infraction.Presistences;
using CloneBE.Infraction.Repo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CloneBEWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<Databasese>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("data"));
            });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<Databasese>()
    .AddDefaultTokenProviders();
            builder.Services.AddScoped<IUnitOfWork1,UnitOfWork>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped(typeof(IGennericRepo<>), typeof(GennerticRepo<>));
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));


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
        }
    }
}
