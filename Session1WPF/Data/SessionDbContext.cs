using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Session1WPF.Data;

public partial class SessionDbContext : DbContext
{
    public SessionDbContext()
    {
    }

    public SessionDbContext(DbContextOptions<SessionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Exhibit> Exhibits { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Space> Spaces { get; set; }

    public virtual DbSet<Studio> Studios { get; set; }

    public virtual DbSet<SvizkaSpacesAndLocation> SvizkaSpacesAndLocations { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TypeOfEvent> TypeOfEvents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\Users\\1\\Downloads\\sqlitestudio_x64-3.4.4\\SQLiteStudio\\SessionDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.DateStart).HasColumnType("TEXT (10)");
            entity.Property(e => e.TimeEnd).HasColumnType("TEXT (5)");
            entity.Property(e => e.TimeStart).HasColumnType("TEXT (5)");

            entity.HasOne(d => d.Spaces).WithMany(p => p.Events)
                .HasForeignKey(d => d.SpacesId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.TypeOfEvents).WithMany(p => p.Events)
                .HasForeignKey(d => d.TypeOfEventsId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Exhibit>(entity =>
        {
            entity.HasOne(d => d.Studios).WithMany(p => p.Exhibits)
                .HasForeignKey(d => d.StudiosId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property(e => e.MestaVridu).HasColumnName("MestaVRidu");
        });

        modelBuilder.Entity<Space>(entity =>
        {
            entity.Property(e => e.Location).HasColumnType("INTEGER (1)");
        });

        modelBuilder.Entity<SvizkaSpacesAndLocation>(entity =>
        {
            entity.HasOne(d => d.Location).WithMany(p => p.SvizkaSpacesAndLocations)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Spaces).WithMany(p => p.SvizkaSpacesAndLocations)
                .HasForeignKey(d => d.SpacesId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
