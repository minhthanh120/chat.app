using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace Notification.Extentions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    string secret = configuration.GetValue<string>("Secret");
                    string issuer = configuration.GetValue<string>("Issuer");
                    string audience = configuration.GetValue<string>("Audience");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                    };
                });
            var redisConnectionString = configuration.GetValue<string>("<your_Redis_connection_string>");
            services.AddSignalR()
              .AddStackExchangeRedis(redisConnectionString, options => {
                  options.Configuration.ChannelPrefix = "chat.app";
              });
            services.AddHostedService<Worker>();
            return services;
        }

    }
}
