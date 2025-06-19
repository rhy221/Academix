using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Academix.Models
{
    public class PhanQuyenNguoiDungContext : DbContext
    {
        public DbSet<NguoiDung> NguoiDung { get; set; }
        public DbSet<NhomNguoiDung> NhomNguoiDung { get; set; }
        public DbSet<ChucNang> ChucNang { get; set; }
        public DbSet<PhanQuyen> PhanQuyen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=DESKTOP-E9KKDG7;Initial Catalog=PHANQUYENNGUOIDUNG;Integrated Security=True;TrustServerCertificate=True");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhanQuyen>()
                .HasKey(pq => new { pq.MaNhom, pq.MaChucNang });

            modelBuilder.Entity<PhanQuyen>()
                .HasOne(pq => pq.NhomNguoiDung)
                .WithMany(n => n.PhanQuyens)
                .HasForeignKey(pq => pq.MaNhom);

            modelBuilder.Entity<PhanQuyen>()
                .HasOne(pq => pq.ChucNang)
                .WithMany(c => c.PhanQuyens)
                .HasForeignKey(pq => pq.MaChucNang);

            modelBuilder.Entity<NguoiDung>()
                .HasOne(n => n.NhomNguoiDung)
                .WithMany(g => g.NguoiDungs)
                .HasForeignKey(n => n.MaNhom);

            modelBuilder.Entity<ChucNang>()
                .HasKey(c => c.MaCN);

            modelBuilder.Entity<NguoiDung>()
                .HasKey(nd => nd.TenDangNhap);

            modelBuilder.Entity<NhomNguoiDung>()
                .HasKey(nd => nd.MaNhom);

            modelBuilder.Entity<PhanQuyen>()
                .HasOne(pq => pq.ChucNang)
                .WithMany(c => c.PhanQuyens)
                .HasForeignKey(pq => pq.MaChucNang);

            modelBuilder.Entity<PhanQuyen>()
                .HasOne(pq => pq.NhomNguoiDung)
                .WithMany(n => n.PhanQuyens)
                .HasForeignKey(pq => pq.MaNhom);



        }
    }
}

