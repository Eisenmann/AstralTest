using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HotelAdminPanel.Models
{
   
    public partial class hotel_dbContext : DbContext
    {
        public hotel_dbContext()
        {
        }

        public hotel_dbContext(DbContextOptions<hotel_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Guests> Guests { get; set; }
        public virtual DbSet<Hotels> Hotels { get; set; }
        public virtual DbSet<RoomTypes> RoomTypes { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("UserID=testuser;Password=testuser;Host=localhost;Port=5432;Database=hotel_db;Pooling=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Guests>(entity =>
            {
                entity.HasKey(e => e.GuestId)
                    .HasName("Guests_pkey");

                entity.Property(e => e.GuestId).ValueGeneratedOnAdd();

                entity.Property(e => e.DateIn).HasColumnType("date");

                entity.Property(e => e.DateOut).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Guests)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_GUEST_ROOM");
            });

            modelBuilder.Entity<Hotels>(entity =>
            {
                entity.HasKey(e => e.HotelId)
                    .HasName("Hotels_pkey");

                entity.Property(e => e.HotelId).ValueGeneratedNever();

                entity.Property(e => e.HotelName)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<RoomTypes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(11);
            });

            modelBuilder.Entity<Rooms>(entity =>
            {
                entity.HasKey(e => e.RoomId)
                    .HasName("rooms_pkey");

                entity.Property(e => e.RoomId).ValueGeneratedNever();

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOTEL_ROOM");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROOM_TYPE");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("users_pkey");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(32);
            });
        }
               
    
}
}
