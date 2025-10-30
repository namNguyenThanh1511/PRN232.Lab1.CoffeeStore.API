using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PRN232.Lab2.CoffeeStore.API.Middleware;
using PRN232.Lab2.CoffeeStore.Repositories.CategoryRepository;
using PRN232.Lab2.CoffeeStore.Repositories.Entities;
using PRN232.Lab2.CoffeeStore.Repositories.MenuRepository;
using PRN232.Lab2.CoffeeStore.Repositories.OrderDetailRepository;
using PRN232.Lab2.CoffeeStore.Repositories.OrderRepository;
using PRN232.Lab2.CoffeeStore.Repositories.PaymentRepository;
using PRN232.Lab2.CoffeeStore.Repositories.ProductRepository;
using PRN232.Lab2.CoffeeStore.Repositories.UnitOfWork;
using PRN232.Lab2.CoffeeStore.Repositories.UserRepository;
using PRN232.Lab2.CoffeeStore.Services.AuthService;
using PRN232.Lab2.CoffeeStore.Services.CacheService;
using PRN232.Lab2.CoffeeStore.Services.Configuration;
using PRN232.Lab2.CoffeeStore.Services.MenuServices;
using PRN232.Lab2.CoffeeStore.Services.OrderService;
using PRN232.Lab2.CoffeeStore.Services.PaymentService;
using PRN232.Lab2.CoffeeStore.Services.ProductServices;
using PRN232.Lab2.CoffeeStore.Services.UserService;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;


namespace PRN232.Lab2.CoffeeStore.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            // ✅ Register repositories first
            services.AddScoped<IRedisService, RedisService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<PayOsService>();


            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();


        }

        public static void AddCorsPolicy(this IServiceCollection services, string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: policyName,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                             .AllowAnyMethod()
                                             .AllowAnyHeader();
                                  });
            });
        }

        public static void ConfigureSwaggerForAuthentication(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                        "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                        "Example: \"Bearer [token]\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                });
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "TestSetUp", Version = "v1" });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                //**Main project's XML docs
                var apiXml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXml);
                options.IncludeXmlComments(apiXmlPath, includeControllerXmlComments: true);
            });
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"];

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }

        public static void ConfigureGlobalException(this IServiceCollection services)
        {
            services.AddProblemDetails();
            services.AddExceptionHandler<GlobalExceptionMiddleware>();
        }





    }
}
