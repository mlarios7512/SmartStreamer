using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StreamBudget.Models;

public partial class SBDbContext : DbContext
{
    public SBDbContext()
    {
    }

    public SBDbContext(DbContextOptions<SBDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<StreamingPlatform> StreamingPlatforms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=SBConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3214EC27E75B6676");

            entity.ToTable("Person");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AspnetIdentityId)
                .HasMaxLength(64)
                .HasColumnName("ASPNetIdentityID");
        });

        modelBuilder.Entity<StreamingPlatform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Streamin__3214EC27281012E3");

            entity.ToTable("StreamingPlatform");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(64);
            entity.Property(e => e.SelectedStreamingCost).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
