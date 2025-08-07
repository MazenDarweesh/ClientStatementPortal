using Microsoft.EntityFrameworkCore;

namespace ClientStatementPortal.Models;

public partial class DbMapperContext : DbContext
{
    public DbMapperContext()
    {
    }

    public DbMapperContext(DbContextOptions<DbMapperContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CompanyConnection> CompanyConnections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyConnection>(entity =>
        {
            entity.HasKey(e => e.CompanyKey).HasName("PK__CompanyC__5A25F59BDDABF9AF");

            entity.Property(e => e.CompanyKey)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DatabaseName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ServerIP)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ServerIP");
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UseWindowsAuth)
                .HasDefaultValueSql("((0))")
                .HasColumnName("UseWindowsAuth");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
