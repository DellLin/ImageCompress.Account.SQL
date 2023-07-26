using System;
using System.Collections.Generic;
using ImageCompress.AccountSQL.DBModels.ImageCompress;
using Microsoft.EntityFrameworkCore;

namespace ImageCompress.AccountSQL.DBContents;

public partial class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Account { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Account_pkey");

            entity.ToTable("Account", "imgcps_secuirty");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Email).HasMaxLength(254);
            entity.Property(e => e.GoogleId)
                .HasMaxLength(254)
                .HasColumnName("GoogleID");
            entity.Property(e => e.LineId)
                .HasMaxLength(254)
                .HasColumnName("LineID");
            entity.Property(e => e.Password).HasMaxLength(254);
            entity.Property(e => e.UpdateDate).HasColumnType("timestamp without time zone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
