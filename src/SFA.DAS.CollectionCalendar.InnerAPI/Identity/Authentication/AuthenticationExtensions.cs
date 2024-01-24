using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SFA.DAS.CollectionCalendar.Infrastructure.Configuration;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace SFA.DAS.CollectionCalendar.InnerAPI.Identity.Authentication;

[ExcludeFromCodeCoverage]
public static class AuthenticationExtensions
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services, ApplicationSettings applicationSettings, bool isDevelopment = false)
    {
        if (isDevelopment)
        {
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
        }
        else
        {
            var azureActiveDirectoryConfiguration = applicationSettings.AzureActiveDirectoryConfiguration;
            services.AddAuthentication(auth =>
            {
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(auth =>
            {
                auth.Authority = $"https://login.microsoftonline.com/{azureActiveDirectoryConfiguration.Tenant}";
                auth.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudiences = azureActiveDirectoryConfiguration.Identifier.Split(",")
                };
            });

            services.AddSingleton<IClaimsTransformation, AzureAdScopeClaimTransformation>();// May not need this
        }
        return services;
    }
}