using Microsoft.EntityFrameworkCore;

using Sasayaku.Data.Types;

namespace Sasayaku.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Client> Clients { get; set; }
    }
}
