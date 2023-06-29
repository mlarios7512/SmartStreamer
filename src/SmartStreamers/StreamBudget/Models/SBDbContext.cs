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

    public virtual DbSet<Watchlist> Watchlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=SBConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3214EC27EDF64DB8");

            entity.ToTable("Person");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AspnetIdentityId)
                .HasMaxLength(64)
                .HasColumnName("ASPNetIdentityID");
        });

        modelBuilder.Entity<Watchlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Watchlis__3214EC27DD035D26");

            entity.ToTable("Watchlist");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(64);
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.SelectedStreamingCost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StreamingPlatform).HasMaxLength(64);

            entity.HasOne(d => d.Owner).WithMany(p => p.Watchlists)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Watchlist_Person_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
