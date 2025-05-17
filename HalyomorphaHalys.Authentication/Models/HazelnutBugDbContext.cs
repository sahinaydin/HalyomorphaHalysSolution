using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HalyomorphaHalys.Authentication.Models;

public partial class HazelnutBugDbContext : DbContext
{

    public HazelnutBugDbContext(DbContextOptions<HazelnutBugDbContext> options)
         : base(options)
    {

    }


    public virtual DbSet<BugDensityNotification> BugDensityNotifications { get; set; }

    public virtual DbSet<TestImage> TestImages { get; set; }

    public virtual DbSet<User> Users { get; set; }

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

            entity.HasOne(d => d.User).WithMany(p => p.BugDensityNotifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BugDensityNotifications_UserId");
        });

        modelBuilder.Entity<TestImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__TestImag__7516F70C4A954B90");

            entity.Property(e => e.ImageFile).HasColumnType("image");
            entity.Property(e => e.ImageName).HasMaxLength(100);
            entity.Property(e => e.ImageTitle).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.TestImages)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TestImages_UserId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C5C8D0A54");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
