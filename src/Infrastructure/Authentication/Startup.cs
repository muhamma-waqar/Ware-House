using Infrastructure.Authentication.Core.Services;
using Infrastructure.Authentication.External.Services;
using Infrastructure.Authentication.Settings;
using Infrastructure.Common.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    internal static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.RegisterMyOptions<AuthenticationSettings>();
            ConfigureLocalJwtAuthentication(services,configuration.GetMyOptions<AuthenticationSettings>());

            services.RegisterMyOptions<ExternalAuthenticationSettings>();
            services.AddScoped<IExternalAuthenticationVerifier, ExternalAuthenticationVerifier>();
            services.AddScoped<IExternalSignInService, ExternalSignInService>();
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }

        private static void ConfigureLocalJwtAuthentication(IServiceCollection services, AuthenticationSettings authSettings)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
#if DEBUG
                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = ctx =>
                        {
                            return Task.FromResult(true);
                        }
                    };
#endif
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authSettings.JwtIssuer,

                        ValidateAudience = true,
                        ValidAudience = authSettings.JwtAudience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(authSettings.JwtSigningKey),
                        ClockSkew = TimeSpan.FromMinutes(5),

                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                    };
                });
        }
    }
}
