using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HalyomorphaHalys.DensityMap.Models;

public partial class HazelnutBugDbContext : DbContext
{
    public HazelnutBugDbContext()
    {
    }

    public HazelnutBugDbContext(DbContextOptions<HazelnutBugDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BugDensityNotification> BugDensityNotifications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BugDensityNotification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__BugDensi__20CF2E12A9F807D6");

            entity.Property(e => e.NotificationCount).HasDefaultValue(0);
            entity.Property(e => e.NotificationDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NotificationLatitude).HasColumnType("decimal(12, 10)");
            entity.Property(e => e.NotificationLongitude).HasColumnType("decimal(12, 10)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
