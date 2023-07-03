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

    public virtual DbSet<WatchlistItem> WatchlistItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=SBConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3214EC273906D6BC");

            entity.ToTable("Person");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AspnetIdentityId)
                .HasMaxLength(64)
                .HasColumnName("ASPNetIdentityID");
        });

        modelBuilder.Entity<Watchlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Watchlis__3214EC27A416D62F");

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

        modelBuilder.Entity<WatchlistItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Watchlis__3214EC27F7A855D9");

            entity.ToTable("WatchlistItem");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ImdbId).HasMaxLength(64);
            entity.Property(e => e.Title).HasMaxLength(64);
            entity.Property(e => e.WatchlistId).HasColumnName("WatchlistID");

            entity.HasOne(d => d.Watchlist).WithMany(p => p.WatchlistItems)
                .HasForeignKey(d => d.WatchlistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_WatchlistItem_Watchlist_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
