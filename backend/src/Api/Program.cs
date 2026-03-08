using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Data.Context;
using Data.Interfaces;
using Data;
using Repositories.Interfaces;
using Repositories;
using Services.Auth;
using Api.Middleware;
using Serilog;
using Hangfire;
using Hangfire.PostgreSql;
using Polly;
using Polly.Extensions.Http;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Resilience Policies
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

builder.Services.AddHttpClient("ExternalServices")
    .AddPolicyHandler(retryPolicy);

// OpenTelemetry
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("WebIFood.Api"))
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddConsoleExporter());

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<Services.Privacy.IPrivacyService, Services.Privacy.PrivacyService>();
builder.Services.AddScoped<Services.Audit.IAuditService, Services.Audit.AuditService>();

builder.Services.AddSingleton<StackExchange.Redis.IConnectionMultiplexer>(sp => 
    StackExchange.Redis.ConnectionMultiplexer.Connect(builder.Configuration.GetSection("Redis:Configuration").Value ?? "localhost:6379"));

builder.Services.AddScoped<Services.Cart.ICartService, Services.Cart.CartService>();
builder.Services.AddScoped<Services.Delivery.IDeliveryCalculator, Services.Delivery.DeliveryCalculator>();
builder.Services.AddScoped<Services.Payments.IPaymentProvider, Services.Payments.PixPaymentService>();
builder.Services.AddScoped<Services.Payments.IRefundService, Services.Payments.MockRefundService>();
builder.Services.AddScoped<Services.Finance.IBalanceService, Services.Finance.BalanceService>();
builder.Services.AddScoped<Services.Promotions.ICouponValidator, Services.Promotions.CouponValidator>();
builder.Services.AddScoped<Services.Marketing.IMarketingJobService, Services.Marketing.MarketingJobService>();
builder.Services.AddScoped<Services.Analytics.IAdminAnalyticsService, Services.Analytics.AdminAnalyticsService>();
builder.Services.AddScoped<Services.Analytics.IStoreAnalyticsService, Services.Analytics.StoreAnalyticsService>();
builder.Services.AddScoped<Services.Security.IFraudDetectionService, Services.Security.FraudDetectionService>();
builder.Services.AddSingleton<Services.Cache.ICacheService, Services.Cache.RedisCacheService>();

var redisConfig = builder.Configuration.GetSection("Redis:Configuration").Value ?? "localhost:6379";
builder.Services.AddSignalR().AddStackExchangeRedis(redisConfig);
builder.Services.AddScoped<Services.RealTime.INotificationService, Api.Services.SignalRNotificationService>();
builder.Services.AddScoped<Services.RealTime.ICourierGeoService, Services.RealTime.CourierGeoService>();
builder.Services.AddScoped<Services.Ordering.IOrderStateService, Services.Ordering.OrderStateService>();

var storagePath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "uploads");
builder.Services.AddScoped<Services.Storage.IFileService>(sp => 
    new Services.Storage.LocalFileService(storagePath, "/uploads"));

builder.Services.AddAutoMapper(typeof(Services.Mappings.MappingProfile).Assembly);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Services.Commands.CreateUser.CreateUserHandler).Assembly));

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"] ?? "very_secret_default_key_1234567890");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("StoreOwnerOnly", policy => policy.RequireRole("StoreOwner", "Admin"));
    options.AddPolicy("CourierOnly", policy => policy.RequireRole("Courier", "Admin"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web iFood API", Version = "v1" });
    c.EnableAnnotations();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

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

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddHealthChecks();

// Hangfire
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(options => options.UseNpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard(); // Optional: secure in prod
}
else
{
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    await next();
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<Api.Middleware.RateLimitingMiddleware>();

app.MapControllers();
app.MapHub<Api.Hubs.OrderHub>("/hubs/order");
app.MapHub<Api.Hubs.ChatHub>("/hubs/chat");

// Recurring Jobs
using (var scope = app.Services.CreateScope())
{
    var marketingService = scope.ServiceProvider.GetRequiredService<Services.Marketing.IMarketingJobService>();
    RecurringJob.AddOrUpdate("inactive-users-campaign", () => marketingService.ProcessInactiveUsersCampaignAsync(), Cron.Daily);
}

app.MapHealthChecks("/health");
app.Run();