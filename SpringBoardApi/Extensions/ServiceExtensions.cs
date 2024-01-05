using AspNetCoreRateLimit;
using Contracts;
using Entities.Models;
using LoggerService;
using Mailjet.Client;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Repositories;
using Repositories.Configurations;
using Services;
using Services.Contracts;
using System.Text;

namespace SpringBoardApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.WithOrigins("http://localhost:3000", "http://localhost:3001", "https://spring-board.netlify.app")
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
                
            });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureController(this IServiceCollection services) => 
            services
                .AddControllers(config => {
                    config.RespectBrowserAcceptHeader = true;
                    config.ReturnHttpNotAcceptable = true;
                    config.CacheProfiles.Add("60SecondsDuration", new CacheProfile
                    {
                        Duration = 120
                    });
                })
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(x => 
                    x.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .AddXmlDataContractSerializerFormatters()
                .AddApplicationPart(typeof(SpringBoard.Presentation.AssemblyReference).Assembly);

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Kokoro"];// Environment.GetEnvironmentVariable("SECRET");
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
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services) => 
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Spring Board Api",
                    Version = "v1",
                    Description = "SpringBoard API by Ojo Toba Rufus",
                    Contact = new OpenApiContact
                    {
                        Name = "Toba R. Ojo",
                        Email = "ojotobar@gmail.com",
                        Url = new Uri("https://ojo-toba.herokuapp.com")
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Spring Board Job Listing Api"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

        public static void ConfigureMailJet(this IServiceCollection services, IConfiguration configuration) => 
            services.AddHttpClient<IMailjetClient, MailjetClient>(client =>
            {
                var settings = configuration.GetSection("Mj");
                var apiKey = settings["Kokoro"];//Environment.GetEnvironmentVariable("MAILJETAPIKEY");
                var apiSecret = settings["Asiri"];// Environment.GetEnvironmentVariable("MAILJETSECRET");
                client.UseBasicAuthentication(apiKey, apiSecret);
            });

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureEmailService(this IServiceCollection services) =>
            services.AddScoped<IEmailService, EmailService>();

        public static void ConfigureCloudinaryService(this IServiceCollection services) =>
            services.AddScoped<ICloudinaryService, CloudinaryService>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("Default")));

        public static void ConfigureIdentity(this IServiceCollection services) =>
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = false;
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<RepositoryContext>().AddDefaultTokenProviders();

        public static void ConfigureMongoIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseName = configuration.GetValue<string>("SpringBoardMongoDb:DatabaseName");
            var connectionStr = configuration.GetValue<string>("SpringBoardMongoDb:ConnectionString");
            services.AddIdentity<AppUser, AppRole>()
                .AddMongoDbStores<AppUser, AppRole, Guid>(connectionStr, databaseName)
                .AddDefaultTokenProviders();
        }

        public static void ConfigureResponseCaching(this IServiceCollection services) =>
            services.AddResponseCaching();

        public static void ConfigureHttpCacheHeaders(this IServiceCollection services) =>
            services.AddHttpCacheHeaders((expirationOpt) =>
            {
                expirationOpt.MaxAge = 20;
                expirationOpt.CacheLocation = CacheLocation.Private;
            },(validationOpt) =>
            {
                validationOpt.MustRevalidate = true;
            });

        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 50,
                    Period = "5m"
                }
             };
             services.Configure<IpRateLimitOptions>(opt => {
                opt.GeneralRules = rateLimitRules;});
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        }
    }
}
