// Data/AppDbContext.cs
using MasrafTakipApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;                      
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MasrafTakipApi.Data
{
    // Identity ile uyumlu base sınıfı
    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<ExpenseRequest> ExpenseRequests { get; set; } = null!;
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Önce Identity tablolarını kuralım

            var seedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.UserName).IsUnique();
                entity.Property(u => u.Role).HasConversion<string>().HasMaxLength(20);
                entity.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(u => u.LastName).HasMaxLength(100).IsRequired();
            });

            // ExpenseCategory Configuration
            modelBuilder.Entity<ExpenseCategory>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // ExpenseRequest Configuration
            modelBuilder.Entity<ExpenseRequest>(entity =>
            {
                entity.Property(e => e.Status).HasConversion<string>().HasMaxLength(20);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.User)
                      .WithMany(u => u.ExpenseRequests)
                      .HasForeignKey(e => e.UserId);

                entity.HasOne(e => e.Category)
                      .WithMany()
                      .HasForeignKey(e => e.CategoryId);
            });

            // Document Configuration
            modelBuilder.Entity<Document>(entity =>
            {
                entity.Property(d => d.FileName).HasMaxLength(255);

                entity.HasOne(d => d.ExpenseRequest)
                      .WithMany(e => e.Documents)
                      .HasForeignKey(d => d.ExpenseRequestId);
            });

            // Seed Data (örnek)
//            modelBuilder.Entity<User>().HasData(/* ... */);
//            modelBuilder.Entity<ExpenseCategory>().HasData(/* ... */);
        }
    }
}
