using Microsoft.IdentityModel.Tokens;

using Sasayaku.Configuration;
using Sasayaku.Services;

namespace Sasayaku
{
    public static class ConfigureServices
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.AddSwagger();
            builder.AddJwtAuthentication();

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
        private static void AddJwtAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication().AddJwtBearer(options =>
            {
                var jwtSecretKey = builder.Configuration["Sasayaku:Authentication:JwtSecretKey"]!;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = JwtTokenService.CreateSymetricSecurityKey(jwtSecretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization();

            //builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
            //builder.Services.AddTransient<Jwt>();
        }
    }
}
