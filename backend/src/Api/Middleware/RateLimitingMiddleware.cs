using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Api.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDatabase _redis;

        public RateLimitingMiddleware(RequestDelegate next, IConnectionMultiplexer redis)
        {
            _next = next;
            _redis = redis.GetDatabase();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userId = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? 
                         context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            
            var key = $"ratelimit:{userId}:{context.Request.Path}";
            var current = await _redis.StringIncrementAsync(key);

            if (current == 1)
            {
                await _redis.KeyExpireAsync(key, TimeSpan.FromMinutes(1));
            }

            if (current > 60) // 1 request per second average (60 per minute)
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsJsonAsync(new { message = "Limite de requisições excedido. Tente novamente em breve." });
                return;
            }

            await _next(context);
        }
    }
}
