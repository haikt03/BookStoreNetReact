using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Application.Interfaces;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Application.Options;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;
using BookStoreNetReact.Infrastructure.Repositories;
using BookStoreNetReact.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookStoreNetReact.Api.Extensions
{
    public static class AppServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Initial services
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // DbContext
            var databaseOptions = configuration.GetSection(DatabaseOptions.SqlServerOptions).Get<DatabaseOptions>();
            if (databaseOptions == null)
                throw new NullReferenceException(nameof(databaseOptions));
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(databaseOptions.ConnectionString, opt =>
                {
                    opt.CommandTimeout(databaseOptions.CommandTimeout);
                });
            });

            // Identity
            services
                .AddIdentityCore<AppUser>(opt =>
                {
                    opt.User.RequireUniqueEmail = true;
                })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            // Authentication, authorization
            var jwtOptions = configuration.GetSection(JwtOptions.JwtBearerOptions).Get<JwtOptions>();
            if (jwtOptions == null)
                throw new NullReferenceException(nameof(jwtOptions));
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.TokenKey))
                    };
                });
            services.AddAuthentication();

            // Options
            services
                .AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.JwtBearerOptions);
            services
                .AddOptions<CloudOptions>()
                .BindConfiguration(CloudOptions.CloudinaryOptions);

            // Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddScoped<ICloudUploadService, CloudUploadService>();

            // Others
            services.AddCors();
        }
    }
}
