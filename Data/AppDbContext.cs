using EcoProfit.Models;
using Microsoft.EntityFrameworkCore;
namespace EcoProfit.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
 public DbSet<AppUser> Users => Set<AppUser>();
 public DbSet<WasteEntry> WasteEntries => Set<WasteEntry>();
 protected override void OnModelCreating(ModelBuilder b)
 {
  b.Entity<AppUser>().HasIndex(x=>x.Email).IsUnique();
  b.Entity<WasteEntry>().HasOne(x=>x.User).WithMany(x=>x.WasteEntries).HasForeignKey(x=>x.UserId).OnDelete(DeleteBehavior.Cascade);
  b.Entity<WasteEntry>().Property(x=>x.QuantityKg).HasPrecision(12,2);
  b.Entity<WasteEntry>().Property(x=>x.RatePerKg).HasPrecision(12,2);
 }
}