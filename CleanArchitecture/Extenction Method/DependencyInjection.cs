using BusinessLayer.RepositortImplementation;
using BusinessLayer.Repository;
using CleanArchitecture.Contract;
using DataAcessLayer.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Reflection;
using System.Text;

namespace CleanArchitecture.Extenction_Method
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepo, StudentImple>();
            services.AddTransient<ITeacherRepo, TeacherImple>();

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CleanDbContext>(options => options
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection")
            , dbOpt => dbOpt.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));

            return services;
        }

        //?-----------------------------stripe payment ------------------------------------------------
        public static IServiceCollection AddStripeInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration.GetValue<string>("StripeSettings:SecretKey");

            return services
                .AddScoped<CustomerService>()
                .AddScoped<ChargeService>()
                .AddScoped<TokenService>()
                .AddScoped<IStripeAppService, StripeAppService>();
        }

        //?--------------------------JWT authentication ---------------------------------------------

        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
           .GetBytes(configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
            return services;
        }
    }
}