using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Academix.Models;
namespace Academix.DbContexts;

public partial class QuanlyhocsinhContext : DbContext
{
    public QuanlyhocsinhContext()
    {
    }

    public QuanlyhocsinhContext(DbContextOptions<QuanlyhocsinhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bangdiemmonhoc> Bangdiemmonhocs { get; set; }

    public virtual DbSet<Bctongkethocky> Bctongkethockies { get; set; }

    public virtual DbSet<Bctongketmon> Bctongketmons { get; set; }

    public virtual DbSet<CtBangdiemmonhoc> CtBangdiemmonhocs { get; set; }

    public virtual DbSet<CtBctongkethocky> CtBctongkethockies { get; set; }

    public virtual DbSet<CtBctongketmon> CtBctongketmons { get; set; }

    public virtual DbSet<CtDiemmonhoc> CtDiemmonhocs { get; set; }

    public virtual DbSet<CtLop> CtLops { get; set; }

    public virtual DbSet<Hocky> Hockies { get; set; }

    public virtual DbSet<Hocsinh> Hocsinhs { get; set; }

    public virtual DbSet<Khoi> Khois { get; set; }

    public virtual DbSet<Loaidiem> Loaidiems { get; set; }

    public virtual DbSet<Lop> Lops { get; set; }

    public virtual DbSet<Monhoc> Monhocs { get; set; }

    public virtual DbSet<Namhoc> Namhocs { get; set; }

    public virtual DbSet<Thamso> Thamsos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\QUANLYHOCSINH.mdf;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bangdiemmonhoc>(entity =>
        {
            entity.HasKey(e => e.Mabdmh).HasName("PK__BANGDIEM__F4CFCE772965FB09");

            entity.ToTable("BANGDIEMMONHOC");

            entity.Property(e => e.Mabdmh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MABDMH");
            entity.Property(e => e.Mahocky)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MAHOCKY");
            entity.Property(e => e.Malop)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MALOP");
            entity.Property(e => e.Mamh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MAMH");

            entity.HasOne(d => d.MahockyNavigation).WithMany(p => p.Bangdiemmonhocs)
                .HasForeignKey(d => d.Mahocky)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BANGDIEMM__MAHOC__5E8A0973");

            entity.HasOne(d => d.MalopNavigation).WithMany(p => p.Bangdiemmonhocs)
                .HasForeignKey(d => d.Malop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BANGDIEMM__MALOP__5CA1C101");

            entity.HasOne(d => d.MamhNavigation).WithMany(p => p.Bangdiemmonhocs)
                .HasForeignKey(d => d.Mamh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BANGDIEMMO__MAMH__5D95E53A");
        });

        modelBuilder.Entity<Bctongkethocky>(entity =>
        {
            entity.HasKey(e => e.Mabctkhocky).HasName("PK__BCTONGKE__DE6521E5DC017B54");

            entity.ToTable("BCTONGKETHOCKY");

            entity.Property(e => e.Mabctkhocky)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MABCTKHOCKY");
            entity.Property(e => e.Mahocky)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MAHOCKY");
            entity.Property(e => e.Manamhoc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MANAMHOC");

            entity.HasOne(d => d.MahockyNavigation).WithMany(p => p.Bctongkethockies)
                .HasForeignKey(d => d.Mahocky)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BCTONGKET__MAHOC__719CDDE7");

            entity.HasOne(d => d.ManamhocNavigation).WithMany(p => p.Bctongkethockies)
                .HasForeignKey(d => d.Manamhoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BCTONGKET__MANAM__72910220");
        });

        modelBuilder.Entity<Bctongketmon>(entity =>
        {
            entity.HasKey(e => e.Mabctkmon).HasName("PK__BCTONGKE__DCCA9B0EF90829D1");

            entity.ToTable("BCTONGKETMON");

            entity.Property(e => e.Mabctkmon)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MABCTKMON");
            entity.Property(e => e.Mahocky)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MAHOCKY");
            entity.Property(e => e.Mamh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MAMH");
            entity.Property(e => e.Manamhoc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MANAMHOC");

            entity.HasOne(d => d.MahockyNavigation).WithMany(p => p.Bctongketmons)
                .HasForeignKey(d => d.Mahocky)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BCTONGKET__MAHOC__69FBBC1F");

            entity.HasOne(d => d.MamhNavigation).WithMany(p => p.Bctongketmons)
                .HasForeignKey(d => d.Mamh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BCTONGKETM__MAMH__690797E6");

            entity.HasOne(d => d.ManamhocNavigation).WithMany(p => p.Bctongketmons)
                .HasForeignKey(d => d.Manamhoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BCTONGKET__MANAM__6AEFE058");
        });

        modelBuilder.Entity<CtBangdiemmonhoc>(entity =>
        {
            entity.HasKey(e => e.Mactbdmh).HasName("PK__CT_BANGD__273B10537328963A");

            entity.ToTable("CT_BANGDIEMMONHOC", tb =>
            {
                tb.HasTrigger("TRG_CAPNHAT_DIEM_TRUNG_BINH_HOC_KY");
                tb.HasTrigger("TRG_Update_CT_BCTONGKETMON");
            });

            entity.Property(e => e.Mactbdmh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MACTBDMH");
            entity.Property(e => e.Dtbmon).HasColumnName("DTBMON");
            entity.Property(e => e.Mabdmh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MABDMH");
            entity.Property(e => e.Mahs)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MAHS");

            entity.HasOne(d => d.MabdmhNavigation).WithMany(p => p.CtBangdiemmonhocs)
                .HasForeignKey(d => d.Mabdmh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CT_BANGDI__MABDM__6166761E");

            entity.HasOne(d => d.MahsNavigation).WithMany(p => p.CtBangdiemmonhocs)
                .HasForeignKey(d => d.Mahs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CT_BANGDIE__MAHS__625A9A57");
        });

        modelBuilder.Entity<CtBctongkethocky>(entity =>
        {
            entity.HasKey(e => new { e.Mabctkhocky, e.Malop }).HasName("PK__CT_BCTON__D9C6FFC49C6AAD15");

            entity.ToTable("CT_BCTONGKETHOCKY");

            entity.Property(e => e.Mabctkhocky)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MABCTKHOCKY");
            entity.Property(e => e.Malop)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MALOP");
            entity.Property(e => e.Siso).HasColumnName("SISO");
            entity.Property(e => e.Soluongdat).HasColumnName("SOLUONGDAT");
            entity.Property(e => e.Tiledat).HasColumnName("TILEDAT");

            entity.HasOne(d => d.MabctkhockyNavigation).WithMany(p => p.CtBctongkethockies)
                .HasForeignKey(d => d.Mabctkhocky)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CT_BCTONG__MABCT__756D6ECB");

            entity.HasOne(d => d.MalopNavigation).WithMany(p => p.CtBctongkethockies)
                .HasForeignKey(d => d.Malop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CT_BCTONG__MALOP__76619304");
        });

        modelBuilder.Entity<CtBctongketmon>(entity =>
        {
            entity.HasKey(e => new { e.Mabctkmon, e.Malop }).HasName("PK__CT_BCTON__DB69452F0992D520");

            entity.ToTable("CT_BCTONGKETMON");

            entity.Property(e => e.Mabctkmon)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MABCTKMON");
            entity.Property(e => e.Malop)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MALOP");
            entity.Property(e => e.Siso).HasColumnName("SISO");
            entity.Property(e => e.Soluongdat).HasColumnName("SOLUONGDAT");
            entity.Property(e => e.Tiledat).HasColumnName("TILEDAT");

            entity.HasOne(d => d.MabctkmonNavigation).WithMany(p => p.CtBctongketmons)
                .HasForeignKey(d => d.Mabctkmon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CT_BCTONG__MABCT__6DCC4D03");

            entity.HasOne(d => d.MalopNavigation).WithMany(p => p.CtBctongketmons)
                .HasForeignKey(d => d.Malop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CT_BCTONG__MALOP__6EC0713C");
        });

        modelBuilder.Entity<CtDiemmonhoc>(entity =>
        {
            entity.HasKey(e => new { e.Mactbdmh, e.Maloaidiem, e.Lan }).HasName("PK__CT_DIEMM__5622FFDFCA7D7087");

            entity.ToTable("CT_DIEMMONHOC", tb => tb.HasTrigger("TRG_CAPNHAT_DIEM_TRUNG_BINH_MON"));

            entity.Property(e => e.Mactbdmh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MACTBDMH");
            entity.Property(e => e.Maloaidiem)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MALOAIDIEM");
            entity.Property(e => e.Lan).HasColumnName("LAN");
            entity.Property(e => e.Diem).HasColumnName("DIEM");

            entity.HasOne(d => d.MactbdmhNavigation).WithMany(p => p.CtDiemmonhocs)
                .HasForeignKey(d => d.Mactbdmh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CT_DIEMMO__MACTB__65370702");

            entity.HasOne(d => d.MaloaidiemNavigation).WithMany(p => p.CtDiemmonhocs)
                .HasForeignKey(d => d.Maloaidiem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CT_DIEMMO__MALOA__662B2B3B");
        });

        modelBuilder.Entity<CtLop>(entity =>
        {
            entity.HasKey(e => new { e.Malop, e.Mahs, e.Mahocky }).HasName("PK__CT_LOP__1B0CD904BC592D63");

            entity.ToTable("CT_LOP", tb =>
            {
                tb.HasTrigger("TRG_Update_CT_BCTONGKETHOCKY");
                tb.HasTrigger("trg_UpdateSiSo_Delete");
                tb.HasTrigger("trg_UpdateSiSo_Insert");
            });

            entity.Property(e => e.Malop)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MALOP");
            entity.Property(e => e.Mahs)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MAHS");
            entity.Property(e => e.Mahocky)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MAHOCKY");
            entity.Property(e => e.Dtbhk).HasColumnName("DTBHK");

            entity.HasOne(d => d.MahockyNavigation).WithMany(p => p.CtLops)
                .HasForeignKey(d => d.Mahocky)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CT_LOP__MAHOCKY__7B264821");

            entity.HasOne(d => d.MahsNavigation).WithMany(p => p.CtLops)
                .HasForeignKey(d => d.Mahs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CT_LOP__MAHS__7A3223E8");

            entity.HasOne(d => d.MalopNavigation).WithMany(p => p.CtLops)
                .HasForeignKey(d => d.Malop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CT_LOP__MALOP__793DFFAF");
        });

        modelBuilder.Entity<Hocky>(entity =>
        {
            entity.HasKey(e => e.Mahocky).HasName("PK__HOCKY__32C918A66EB7B9B3");

            entity.ToTable("HOCKY");

            entity.Property(e => e.Mahocky)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MAHOCKY");
            entity.Property(e => e.Tenhocky)
                .HasMaxLength(20)
                .HasColumnName("TENHOCKY");
        });

        modelBuilder.Entity<Hocsinh>(entity =>
        {
            entity.HasKey(e => e.Mahs).HasName("PK__HOCSINH__603F20DDCF124170");

            entity.ToTable("HOCSINH");

            entity.Property(e => e.Mahs)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MAHS");
            entity.Property(e => e.Diachi)
                .HasMaxLength(255)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Gioitinh)
                .HasMaxLength(3)
                .HasColumnName("GIOITINH");
            entity.Property(e => e.Hoten)
                .HasMaxLength(50)
                .HasColumnName("HOTEN");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("smalldatetime")
                .HasColumnName("NGAYSINH");
        });

        modelBuilder.Entity<Khoi>(entity =>
        {
            entity.HasKey(e => e.Makhoi).HasName("PK__KHOI__22F41778D1CEF87B");

            entity.ToTable("KHOI");

            entity.Property(e => e.Makhoi)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MAKHOI");
            entity.Property(e => e.Tenkhoi)
                .HasMaxLength(10)
                .HasColumnName("TENKHOI");
        });

        modelBuilder.Entity<Loaidiem>(entity =>
        {
            entity.HasKey(e => e.Maloaidiem).HasName("PK__LOAIDIEM__9DFB99C4CFAF6744");

            entity.ToTable("LOAIDIEM");

            entity.Property(e => e.Maloaidiem)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MALOAIDIEM");
            entity.Property(e => e.Hesold).HasColumnName("HESOLD");
            entity.Property(e => e.Tenloaidiem)
                .HasMaxLength(20)
                .HasColumnName("TENLOAIDIEM");
        });

        modelBuilder.Entity<Lop>(entity =>
        {
            entity.HasKey(e => e.Malop).HasName("PK__LOP__7A3DE21188B0701B");

            entity.ToTable("LOP");

            entity.Property(e => e.Malop)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MALOP");
            entity.Property(e => e.Makhoi)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MAKHOI");
            entity.Property(e => e.Manamhoc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MANAMHOC");
            entity.Property(e => e.Siso).HasColumnName("SISO");
            entity.Property(e => e.Tenlop)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TENLOP");

            entity.HasOne(d => d.MakhoiNavigation).WithMany(p => p.Lops)
                .HasForeignKey(d => d.Makhoi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LOP__MAKHOI__540C7B00");

            entity.HasOne(d => d.ManamhocNavigation).WithMany(p => p.Lops)
                .HasForeignKey(d => d.Manamhoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LOP__MANAMHOC__55009F39");
        });

        modelBuilder.Entity<Monhoc>(entity =>
        {
            entity.HasKey(e => e.Mamh).HasName("PK__MONHOC__603F69EB972B4D05");

            entity.ToTable("MONHOC");

            entity.Property(e => e.Mamh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MAMH");
            entity.Property(e => e.Heso).HasColumnName("HESO");
            entity.Property(e => e.Tenmh)
                .HasMaxLength(20)
                .HasColumnName("TENMH");

            entity.HasMany(d => d.Maloaidiems).WithMany(p => p.Mamhs)
                .UsingEntity<Dictionary<string, object>>(
                    "CtMonhoc",
                    r => r.HasOne<Loaidiem>().WithMany()
                        .HasForeignKey("Maloaidiem")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CT_MONHOC__MALOA__5CD6CB2B"),
                    l => l.HasOne<Monhoc>().WithMany()
                        .HasForeignKey("Mamh")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CT_MONHOC__MAMH__5BE2A6F2"),
                    j =>
                    {
                        j.HasKey("Mamh", "Maloaidiem").HasName("PK__CT_MONHO__39E0D077C31CDD9B");
                        j.ToTable("CT_MONHOC");
                        j.IndexerProperty<string>("Mamh")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .HasColumnName("MAMH");
                        j.IndexerProperty<string>("Maloaidiem")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .HasColumnName("MALOAIDIEM");
                    });
        });

        modelBuilder.Entity<Namhoc>(entity =>
        {
            entity.HasKey(e => e.Manamhoc).HasName("PK__NAMHOC__4BB084BE9CCFEF00");

            entity.ToTable("NAMHOC");

            entity.Property(e => e.Manamhoc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MANAMHOC");
            entity.Property(e => e.Nam1).HasColumnName("NAM1");
            entity.Property(e => e.Nam2).HasColumnName("NAM2");
        });

        modelBuilder.Entity<Thamso>(entity =>
        {
            entity.HasKey(e => e.Tenthamso).HasName("PK__THAMSO__3869154791E386F8");

            entity.ToTable("THAMSO");

            entity.Property(e => e.Tenthamso)
                .HasMaxLength(255)
                .HasColumnName("TENTHAMSO");
            entity.Property(e => e.Giatri).HasColumnName("GIATRI");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
