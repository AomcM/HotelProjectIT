using System;
using System.Collections.Generic;
using HotelIT.API.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace HotelIT.API.Data;

public partial class HotelITDbContext : DbContext
{
    public HotelITDbContext()
    {
    }

    public HotelITDbContext(DbContextOptions<HotelITDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aianalysis> Aianalysis { get; set; }

    public virtual DbSet<Assets> Assets { get; set; }

    public virtual DbSet<Departments> Departments { get; set; }

    public virtual DbSet<Notification> Notification { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Tickets> Tickets { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=hotelitservicedb;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Aianalysis>(entity =>
        {
            entity.HasKey(e => e.AnalysisId).HasName("PRIMARY");

            entity.ToTable("aianalysis");

            entity.HasIndex(e => e.TicketId, "FK_ticket_analysis");

            entity.Property(e => e.AnalysisId).HasColumnType("int(11)");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.SuggestedPriority).HasMaxLength(20);
            entity.Property(e => e.SuggestedSolution).HasColumnType("text");
            entity.Property(e => e.TicketId).HasColumnType("int(11)");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Aianalysis)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK_ticket_analysis");
        });

        modelBuilder.Entity<Assets>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PRIMARY");

            entity.ToTable("assets");

            entity.HasIndex(e => e.DepartmentId, "FK_Assets_Departments");

            entity.Property(e => e.AssetId).HasColumnType("int(11)");
            entity.Property(e => e.AssetName).HasMaxLength(100);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.DepartmentId).HasColumnType("int(11)");
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.SerialNumber).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(30);

            entity.HasOne(d => d.Department).WithMany(p => p.Assets)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Assets_Departments");
        });

        modelBuilder.Entity<Departments>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PRIMARY");

            entity.ToTable("departments");

            entity.Property(e => e.DepartmentId).HasColumnType("int(11)");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PRIMARY");

            entity.ToTable("notification");

            entity.HasIndex(e => e.UserId, "FK_Users_notification");

            entity.Property(e => e.NotificationId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Message).HasColumnType("text");
            entity.Property(e => e.Title).HasMaxLength(150);
            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.HasOne(d => d.User).WithMany(p => p.Notification)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Users_notification");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId).HasColumnType("int(11)");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Tickets>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PRIMARY");

            entity.ToTable("tickets");

            entity.HasIndex(e => e.UserId, "FK_Tchnician_ticket");

            entity.Property(e => e.TicketId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Priority).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.TechnicianId).HasColumnType("int(11)");
            entity.Property(e => e.Title).HasMaxLength(150);
            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Tchnician_ticket");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.DepartmentId, "FK_Users_Departments");

            entity.HasIndex(e => e.RoleId, "FK_Users_Roles");

            entity.Property(e => e.UserId).HasColumnType("int(11)");
            entity.Property(e => e.DepartmentId).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.RoleId).HasColumnType("int(11)");

            entity.HasOne(d => d.Department).WithMany(p => p.Users)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Users_Departments");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
