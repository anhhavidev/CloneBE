using AutoMapper;
using CloneBE.Application.Interface.Serivce;
using CloneBE.Application.Mapper;
using CloneBE.Domain.InterfaceRepo;
using CloneBE.Infraction.Presistences;
using CloneBE.Infraction.Repo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Google;
using CloneBE.Domain.EF;
using CloneBEWebAPI.Middleware;
using CloneBE.Application.Helper;
using CloneBE.Application.Interface;
using FluentValidation.AspNetCore;


namespace CloneBEWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // 🔥 Thêm SecurityDefinition cho JWT Bearer
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Nhập token theo format: Bearer {your_token}"
                });

                // 🔥 Thêm SecurityRequirement để yêu cầu xác thực
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<Databasese>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("data"));
            });

            builder.Services.AddIdentity<AppUser, AppIdentityRole>()
    .AddEntityFrameworkStores<Databasese>()
    .AddDefaultTokenProviders();
            builder.Services.AddScoped<IAcountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IUnitOfWork1, UnitOfWork>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICartRepo, CartRePo>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped(typeof(IGennericRepo<>), typeof(GennerticRepo<>));
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOTPService, OTPService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IIUserService, UserService>();
            builder.Services.AddTransient<CloneBE.Application.Helper.ISendMailService, CloneBE.Application.Helper.SendMailService>();
            builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

            // Đọc cấu hình từ appsettings.json
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            })
            .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                options.CallbackPath = "/signin-google";
            });
            //;
            builder.Services.AddAuthorization();
            builder.Services.AddMemoryCache();


            // Thêm Logging vào Web API
            //builder.Logging.ClearProviders(); // Xóa cấu hình log mặc định
            // builder.Logging.AddConsole(); // Log ra console
            //builder.Logging.AddDebug(); // Log ra Debug Output (Visual Studio)
            //builder.Logging.SetMinimumLevel(LogLevel.Information); // Mức log tối thiểu
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionHandlingMiddleware>(); //su dung userouting thi đi cùng endpoint
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles(); // Để truy cập ảnh qua đường dẫn /images/

            // 3️⃣ Xử lý CORS (cho phép frontend gọi API)
            app.UseCors(policy =>
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
            app.MapControllers();

            app.Run(); // termidenal middleware 
        }
    }
}
