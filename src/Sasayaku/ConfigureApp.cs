using Sasayaku.Data;
using Sasayaku.Endpoints;

namespace Sasayaku
{
    public static class ConfigureApp
    {
        public static async Task ConfigureAsync(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            app.MapEndpoints();

            // await EnsureDatabaseInitializedAsync();
        }

        private static async Task EnsureDatabaseInitializedAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var migrationRunner = scope.ServiceProvider.GetRequiredService<MigrationRunner>();

            await migrationRunner.MigrateAsync();
        }
    }
}
