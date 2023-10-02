using Microsoft.EntityFrameworkCore;
namespace startup.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext>options):base(options)
        { }
        public DbSet<Menu> Menu { get; set; }
    }
}
