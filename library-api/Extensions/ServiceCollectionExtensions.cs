using Microsoft.OpenApi.Models;

namespace library_system.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddSwaggerWithAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(g =>
            {
                g.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));

                g.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(configuration["KeyCloak:AuthorizationUrl"]!),
                            Scopes = new Dictionary<string, string>
                            {
                                {"openid", "openid"},
                                {"profile", "profile"}
                            }
                        }
                    }
                });

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Keycloak",
                                Type = ReferenceType.SecurityScheme
                            },
                            In = ParameterLocation.Header,
                            Name = "Bearer",
                            Scheme = "Bearer"
                        },
                        []
                    }
                };
                g.AddSecurityRequirement(securityRequirement);
            });

            return services;
        }
    }
}
