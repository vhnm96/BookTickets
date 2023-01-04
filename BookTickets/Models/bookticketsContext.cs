using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BookTickets.Models
{
    public partial class bookticketsContext : DbContext
    {
        public bookticketsContext()
        {
        }
        public bookticketsContext(DbContextOptions<bookticketsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Khachhang> Khachhangs { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Oder> Oders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Showtime> Showtimes { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public object MaKH { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ADMIN;Database=booktickets;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory);

                entity.ToTable("CATEGORY");

                entity.Property(e => e.NameCategory)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Khachhang>(entity =>
            {
                entity.HasKey(e => e.MaKh)
                    .HasName("PK_Khachhang");

                entity.ToTable("KHACHHANG");

                entity.HasIndex(e => e.Taikhoan, "UQ__KHACHHAN__7FB3F64F31BDE71F")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__KHACHHAN__A9D10534280E1910")
                    .IsUnique();

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.DiachiKh)
                    .HasMaxLength(200)
                    .HasColumnName("DiachiKH");

                entity.Property(e => e.DienthoaiKh)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DienthoaiKH");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HoTen)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Matkhau)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngaysinh).HasColumnType("datetime");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Taikhoan)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Khachhangs)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_KHACHHANG_ROLES");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.IdMovie);

                entity.ToTable("MOVIE");

                entity.Property(e => e.Anhbia)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameMovie)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("FK_Chude");
            });

            modelBuilder.Entity<Oder>(entity =>
            {
                entity.HasKey(e => e.IdOder);

                entity.ToTable("ODER");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.Ngaydat).HasColumnType("datetime");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.Oders)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_Khachhang");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLES");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Mota).HasMaxLength(50);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.IdRoom);

                entity.ToTable("ROOM");

                entity.Property(e => e.IdRoom).HasColumnName("idRoom");

                entity.Property(e => e.Soghetoida).HasColumnName("soghetoida");

                entity.Property(e => e.TenPhong)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Showtime>(entity =>
            {
                entity.HasKey(e => e.IdShowtime);

                entity.ToTable("SHOWTIME");

                entity.Property(e => e.Thoigianchieu).HasColumnType("datetime");

                entity.HasOne(d => d.IdMovieNavigation)
                    .WithMany(p => p.Showtimes)
                    .HasForeignKey(d => d.IdMovie)
                    .HasConstraintName("FK_Movie");

                entity.HasOne(d => d.IdRoomNavigation)
                    .WithMany(p => p.Showtimes)
                    .HasForeignKey(d => d.IdRoom)
                    .HasConstraintName("FK_Room");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.IdTicket);

                entity.ToTable("TICKET");

                entity.Property(e => e.Dongia).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdOderNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdOder)
                    .HasConstraintName("FK_Oder");

                entity.HasOne(d => d.IdShowtimeNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdShowtime)
                    .HasConstraintName("FK_Showtime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
