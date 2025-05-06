// Data/AppDbContextFactory.cs
using MasrafTakipApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MasrafTakipApi.Data
{
    // Tasarım zamanında migrations için
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // Buraya appsettings.json’daki connection string’inizi koyun:
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=paparaprojedb;User Id=postgres;Password=1234;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
