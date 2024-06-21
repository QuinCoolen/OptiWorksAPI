using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.DataAnnotations;
using MySql.EntityFrameworkCore.Extensions;

namespace OptiWorksAPI.Models;

public class OwContext(DbContextOptions<OwContext> options) : IdentityDbContext<ApplicationUser>(options)
{
  public DbSet<Visitor> Visitors { get; set; } = null!;
  public DbSet<Attraction> Attractions { get; set; } = null!;
  public DbSet<World> Worlds { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<ApplicationUser>(entity => entity.Property(m => m.Id).HasMaxLength(80));
    modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(80));
    modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(80));
    modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(80));
    modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(80));
    modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(80));
    modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(80));
    modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(80));
    modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(80));
    modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(80));
    modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(80));
    modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(80));
    modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(80));
    modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(80));

    modelBuilder.Entity<Visitor>()
        .HasOne(v => v.AttractionInQueue)
        .WithMany(a => a.VisitorsInQueue)
        .HasForeignKey(v => v.AttractionInQueueId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Visitor>()
        .HasOne(v => v.AttractionOnRide)
        .WithMany(a => a.VisitorsOnRide)
        .HasForeignKey(v => v.AttractionOnRideId)
        .OnDelete(DeleteBehavior.Restrict);
  }
}