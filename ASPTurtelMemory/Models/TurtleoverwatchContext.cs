using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASPTurtelMemory.Models;

public partial class TurtleoverwatchContext : DbContext
{
    public TurtleoverwatchContext()
    {
    }

    public TurtleoverwatchContext(DbContextOptions<TurtleoverwatchContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Block> Blocks { get; set; }

    public virtual DbSet<Turtle> Turtles { get; set; }

    public virtual DbSet<World> Worlds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=turtle;password=turtle;database=turtleoverwatch", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.8-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_swedish_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Block>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("block")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.HasIndex(e => e.WorldId, "FK_block_to_world");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .IsFixedLength();
            entity.Property(e => e.WorldId)
                .HasColumnType("int(11)")
                .HasColumnName("World_Id");
            entity.Property(e => e.X).HasColumnType("int(11)");
            entity.Property(e => e.Y).HasColumnType("int(11)");
            entity.Property(e => e.Z).HasColumnType("int(11)");

            entity.HasOne(d => d.World).WithMany(p => p.Blocks)
                .HasForeignKey(d => d.WorldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_block_to_world");
        });

        modelBuilder.Entity<Turtle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("turtle")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.HasIndex(e => e.WorldId, "FK_turtle_to_world");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.WorldId)
                .HasColumnType("int(11)")
                .HasColumnName("World_Id");
            entity.Property(e => e.X).HasColumnType("int(11)");
            entity.Property(e => e.Y).HasColumnType("int(11)");
            entity.Property(e => e.Z).HasColumnType("int(11)");

            entity.HasOne(d => d.World).WithMany(p => p.Turtles)
                .HasForeignKey(d => d.WorldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_turtle_to_world");
        });

        modelBuilder.Entity<World>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("world")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
