using udemy_dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace udemy_dotnet_rpg.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
    }
}