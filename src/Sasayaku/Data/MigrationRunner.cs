using Microsoft.EntityFrameworkCore;

namespace Sasayaku.Data
{
    public class MigrationRunner(AppDbContext context)
    {
        public async Task MigrateAsync()
        {
            await context.Database.MigrateAsync();
        }
    }
}
