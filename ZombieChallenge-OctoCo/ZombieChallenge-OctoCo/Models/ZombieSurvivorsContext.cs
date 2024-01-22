using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ZombieChallenge_OctoCo.Models.Base;

namespace ZombieChallenge_OctoCo.Models;

public partial class ZombieSurvivorsContext : DbContext
{

    /*
     EXPLINATIONS:
    -This is the context class, it is the class that will be used to interact with the database.
    -It maps the classes to the database tables.
    -It uses the Entity Framework Core to do this.
    -The models are located in the Models/Base folder. They are partial classes, so they can be extended in the Models folder.

    THOUGHTS:
    -The connection string is located in the appsettings.json file. But if this where a real project, I would use the user secrets to store the connection string.
    - In hindsight this project would have been complete quicker if I had used MongoDB instead of MySQL. This is becase of how the data is structured. Obviously nothing wrong with 
      the MySQL implimentation. But for prototyping purposes, it would have been quicker
     */

    public ZombieSurvivorsContext()
    {
    }

    public ZombieSurvivorsContext(DbContextOptions<ZombieSurvivorsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InventoryItem> InventoryItems { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Survivor> Survivors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<InventoryItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inventory_items");

            entity.HasIndex(e => e.SurvivorsId, "fk_inventory_items_survivors_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Item)
                .HasMaxLength(255)
                .HasColumnName("item");
            entity.Property(e => e.SurvivorsId).HasColumnName("survivors_id");

            entity.HasOne(d => d.Survivors).WithMany(p => p.InventoryItems)
                .HasForeignKey(d => d.SurvivorsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_inventory_items_survivors");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("locations");

            entity.HasIndex(e => e.SurvivorsId, "fk_locations_survivors1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Latitude)
                .HasPrecision(10, 8)
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasPrecision(11, 8)
                .HasColumnName("longitude");
            entity.Property(e => e.SurvivorsId).HasColumnName("survivors_id");

            entity.HasOne(d => d.Survivors).WithMany(p => p.Locations)
                .HasForeignKey(d => d.SurvivorsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_locations_survivors1");
        });

        modelBuilder.Entity<Survivor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("survivors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Infected).HasColumnName("infected");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
