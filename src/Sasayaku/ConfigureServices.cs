using Sasayaku.Configuration;
using Sasayaku.Services;

namespace Sasayaku
{
    public static class ConfigureServices
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.AddSwagger();

            builder.Services
                .AddOptions<AppOptions>()
                .BindConfiguration(AppOptions.Section)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            var authenticationSection = builder.Configuration
                .GetSection($"{AppOptions.Section}:{AuthenticationOptions.Section}");

            builder.Services
                .AddOptions<AuthenticationOptions>()
                .Bind(authenticationSection)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<JwtTokenService>();
        }

        private static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.FullName?.Replace('+', '.'));
                options.InferSecuritySchemes();
            });
        }
    }
}
