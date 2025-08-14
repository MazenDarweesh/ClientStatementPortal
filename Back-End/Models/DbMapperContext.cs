using System;
using System.Collections.Generic;
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

    public virtual DbSet<VisitorEvent> VisitorEvents { get; set; }


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
            entity.Property(e => e.ServerIp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ServerIP");
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .IsUnicode(false);
        });
        modelBuilder.Entity<VisitorEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VisitorE__3214EC078D1AAEB3");

            entity.Property(e => e.AccountName).HasMaxLength(200);
            entity.Property(e => e.AccountType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CompanyKey).HasMaxLength(100);
            entity.Property(e => e.EventDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EventType).HasMaxLength(50);
            entity.Property(e => e.IpAddress).HasMaxLength(45);
            entity.Property(e => e.UserAgent).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
