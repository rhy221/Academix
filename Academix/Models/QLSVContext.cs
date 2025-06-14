//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;

//namespace Academix.Models;

//public partial class QLSVContext : DbContext
//{
//    public QLSVContext()
//    {
//    }

//    public QLSVContext(DbContextOptions<QLSVContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Bangdiemmonhoc> Bangdiemmonhocs { get; set; }

//    public virtual DbSet<Bctongkethocky> Bctongkethockies { get; set; }

//    public virtual DbSet<Bctongketmon> Bctongketmons { get; set; }

//    public virtual DbSet<CtBangdiemmonhoc> CtBangdiemmonhocs { get; set; }

//    public virtual DbSet<CtBctongkethocky> CtBctongkethockies { get; set; }

//    public virtual DbSet<CtBctongketmon> CtBctongketmons { get; set; }

//    public virtual DbSet<CtDiemmonhoc> CtDiemmonhocs { get; set; }

//    public virtual DbSet<CtLop> CtLops { get; set; }

//    public virtual DbSet<SubjectDetail> SubjectDetails { get; set; }

//    public virtual DbSet<Hocky> Hockies { get; set; }

//    public virtual DbSet<Hocsinh> Hocsinhs { get; set; }

//    public virtual DbSet<Khoi> Khois { get; set; }

//    public virtual DbSet<ScoreType> ScoreTypes { get; set; }

//    public virtual DbSet<Lop> Lops { get; set; }

//    public virtual DbSet<Subject> Subjects { get; set; }

//    public virtual DbSet<Namhoc> Namhocs { get; set; }

//    public virtual DbSet<Parameter> Parameters { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-NQOI7D0;Database=QUANLYHOCSINH;Trusted_Connection=True;TrustServerCertificate=True");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Bangdiemmonhoc>(entity =>
//        {
//            entity.HasKey(e => e.Mabdmh).HasName("PK__BANGDIEM__F4CFCE775C39D2C2");

//            entity.ToTable("BANGDIEMMONHOC");

//            entity.Property(e => e.Mabdmh)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MABDMH");
//            entity.Property(e => e.Mahocky)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MAHOCKY");
//            entity.Property(e => e.Malop)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MALOP");
//            entity.Property(e => e.Mamh)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MAMH");

//            entity.HasOne(d => d.MahockyNavigation).WithMany(p => p.Bangdiemmonhocs)
//                .HasForeignKey(d => d.Mahocky)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__BANGDIEMM__MAHOC__571DF1D5");

//            entity.HasOne(d => d.MalopNavigation).WithMany(p => p.Bangdiemmonhocs)
//                .HasForeignKey(d => d.Malop)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__BANGDIEMM__MALOP__5535A963");

//            entity.HasOne(d => d.MamhNavigation).WithMany(p => p.Bangdiemmonhocs)
//                .HasForeignKey(d => d.Mamh)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__BANGDIEMMO__MAMH__5629CD9C");
//        });

//        modelBuilder.Entity<Bctongkethocky>(entity =>
//        {
//            entity.HasKey(e => e.Mabctkhocky).HasName("PK__BCTONGKE__DE6521E557AFFF1F");

//            entity.ToTable("BCTONGKETHOCKY");

//            entity.Property(e => e.Mabctkhocky)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MABCTKHOCKY");
//            entity.Property(e => e.Mahocky)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MAHOCKY");
//            entity.Property(e => e.Manamhoc)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MANAMHOC");

//            entity.HasOne(d => d.MahockyNavigation).WithMany(p => p.Bctongkethockies)
//                .HasForeignKey(d => d.Mahocky)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__BCTONGKET__MAHOC__70DDC3D8");

//            entity.HasOne(d => d.ManamhocNavigation).WithMany(p => p.Bctongkethockies)
//                .HasForeignKey(d => d.Manamhoc)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__BCTONGKET__MANAM__71D1E811");
//        });

//        modelBuilder.Entity<Bctongketmon>(entity =>
//        {
//            entity.HasKey(e => e.Mabctkmon).HasName("PK__BCTONGKE__DCCA9B0EEBE8666B");

//            entity.ToTable("BCTONGKETMON");

//            entity.Property(e => e.Mabctkmon)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MABCTKMON");
//            entity.Property(e => e.Mahocky)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MAHOCKY");
//            entity.Property(e => e.Mamh)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MAMH");
//            entity.Property(e => e.Manamhoc)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MANAMHOC");

//            entity.HasOne(d => d.MahockyNavigation).WithMany(p => p.Bctongketmons)
//                .HasForeignKey(d => d.Mahocky)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__BCTONGKET__MAHOC__693CA210");

//            entity.HasOne(d => d.MamhNavigation).WithMany(p => p.Bctongketmons)
//                .HasForeignKey(d => d.Mamh)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__BCTONGKETM__MAMH__68487DD7");

//            entity.HasOne(d => d.ManamhocNavigation).WithMany(p => p.Bctongketmons)
//                .HasForeignKey(d => d.Manamhoc)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__BCTONGKET__MANAM__6A30C649");
//        });

//        modelBuilder.Entity<CtBangdiemmonhoc>(entity =>
//        {
//            entity.HasKey(e => e.Mactbdmh).HasName("PK__CT_BANGD__273B105327C3A8EC");

//            entity.ToTable("CT_BANGDIEMMONHOC");

//            entity.Property(e => e.Mactbdmh)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MACTBDMH");
//            entity.Property(e => e.Dtbmon).HasColumnName("DTBMON");
//            entity.Property(e => e.Mabdmh)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MABDMH");
//            entity.Property(e => e.Mahs)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MAHS");

//            entity.HasOne(d => d.MabdmhNavigation).WithMany(p => p.CtBangdiemmonhocs)
//                .HasForeignKey(d => d.Mabdmh)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__CT_BANGDI__MABDM__5FB337D6");

//            entity.HasOne(d => d.MahsNavigation).WithMany(p => p.CtBangdiemmonhocs)
//                .HasForeignKey(d => d.Mahs)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__CT_BANGDIE__MAHS__60A75C0F");
//        });

//        modelBuilder.Entity<CtBctongkethocky>(entity =>
//        {
//            entity.HasKey(e => new { e.Mabctkhocky, e.Malop }).HasName("PK__CT_BCTON__D9C6FFC4BC08251F");

//            entity.ToTable("CT_BCTONGKETHOCKY");

//            entity.Property(e => e.Mabctkhocky)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MABCTKHOCKY");
//            entity.Property(e => e.Malop)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MALOP");
//            entity.Property(e => e.Siso).HasColumnName("SISO");
//            entity.Property(e => e.Soluongdat).HasColumnName("SOLUONGDAT");
//            entity.Property(e => e.Tiledat).HasColumnName("TILEDAT");

//            entity.HasOne(d => d.MabctkhockyNavigation).WithMany(p => p.CtBctongkethockies)
//                .HasForeignKey(d => d.Mabctkhocky)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__CT_BCTONG__MABCT__74AE54BC");

//            entity.HasOne(d => d.MalopNavigation).WithMany(p => p.CtBctongkethockies)
//                .HasForeignKey(d => d.Malop)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__CT_BCTONG__MALOP__75A278F5");
//        });

//        modelBuilder.Entity<CtBctongketmon>(entity =>
//        {
//            entity.HasKey(e => new { e.Mabctkmon, e.Malop }).HasName("PK__CT_BCTON__DB69452FE2EBC03F");

//            entity.ToTable("CT_BCTONGKETMON");

//            entity.Property(e => e.Mabctkmon)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MABCTKMON");
//            entity.Property(e => e.Malop)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MALOP");
//            entity.Property(e => e.Siso).HasColumnName("SISO");
//            entity.Property(e => e.Soluongdat).HasColumnName("SOLUONGDAT");
//            entity.Property(e => e.Tiledat).HasColumnName("TILEDAT");

//            entity.HasOne(d => d.MabctkmonNavigation).WithMany(p => p.CtBctongketmons)
//                .HasForeignKey(d => d.Mabctkmon)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__CT_BCTONG__MABCT__6D0D32F4");

//            entity.HasOne(d => d.MalopNavigation).WithMany(p => p.CtBctongketmons)
//                .HasForeignKey(d => d.Malop)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__CT_BCTONG__MALOP__6E01572D");
//        });

//        modelBuilder.Entity<CtDiemmonhoc>(entity =>
//        {
//            entity.HasKey(e => new { e.Mactbdmh, e.Maloaidiem, e.Lan }).HasName("PK__CT_DIEMM__5622FFDF95E5FD57");

//            entity.ToTable("CT_DIEMMONHOC");

//            entity.Property(e => e.Mactbdmh)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MACTBDMH");
//            entity.Property(e => e.Maloaidiem)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MALOAIDIEM");
//            entity.Property(e => e.Lan).HasColumnName("LAN");
//            entity.Property(e => e.Diem).HasColumnName("DIEM");

//            entity.HasOne(d => d.MactbdmhNavigation).WithMany(p => p.CtDiemmonhocs)
//                .HasForeignKey(d => d.Mactbdmh)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__CT_DIEMMO__MACTB__6477ECF3");

//            entity.HasOne(d => d.MaloaidiemNavigation).WithMany(p => p.CtDiemmonhocs)
//                .HasForeignKey(d => d.Maloaidiem)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__CT_DIEMMO__MALOA__656C112C");
//        });

//        modelBuilder.Entity<CtLop>(entity =>
//        {
//            entity.HasKey(e => new { e.Malop, e.Mahs }).HasName("PK__CT_LOP__BC3E101CAE7C98F2");

//            entity.ToTable("CT_LOP");

//            entity.Property(e => e.Malop)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MALOP");
//            entity.Property(e => e.Mahs)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MAHS");
//            entity.Property(e => e.Dtbhk).HasColumnName("DTBHK");
//            entity.Property(e => e.Mahocky)
//                .HasMaxLength(10)
//                .HasColumnName("MAHOCKY");

//            entity.HasOne(d => d.MahsNavigation).WithMany(p => p.CtLops)
//                .HasForeignKey(d => d.Mahs)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__CT_LOP__MAHS__4CA06362");

//            entity.HasOne(d => d.MalopNavigation).WithMany(p => p.CtLops)
//                .HasForeignKey(d => d.Malop)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__CT_LOP__MALOP__4BAC3F29");
//        });

//        modelBuilder.Entity<SubjectDetail>(entity =>
//        {
//            entity.HasKey(e => new { e.SubjectId, e.ScoreTypeId }).HasName("PK__CT_MONHO__39E0D077C31CDD9B");

//            entity.ToTable("CT_MONHOC");

//            entity.Property(e => e.SubjectId)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MAMH");
//            entity.Property(e => e.ScoreTypeId)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MALOAIDIEM");

//            entity.HasOne(d => d.ScoreTypeIdNavigation).WithMany(p => p.SubjectDetails)
//                .HasForeignKey(d => d.ScoreTypeId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__CT_MONHOC__MALOA__5CD6CB2B");

//            entity.HasOne(d => d.SubjectIdNavigation).WithMany(p => p.SubjectDetails)
//                .HasForeignKey(d => d.SubjectId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__CT_MONHOC__MAMH__5BE2A6F2");
//        });

//        modelBuilder.Entity<Hocky>(entity =>
//        {
//            entity.HasKey(e => e.Mahocky).HasName("PK__HOCKY__32C918A66EB7B9B3");

//            entity.ToTable("HOCKY");

//            entity.Property(e => e.Mahocky)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MAHOCKY");
//            entity.Property(e => e.Tenhocky)
//                .HasMaxLength(10)
//                .HasColumnName("TENHOCKY");
//        });

//        modelBuilder.Entity<Hocsinh>(entity =>
//        {
//            entity.HasKey(e => e.Mahs).HasName("PK__HOCSINH__603F20DD1D5ED92A");

//            entity.ToTable("HOCSINH");

//            entity.Property(e => e.Mahs)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MAHS");
//            entity.Property(e => e.Diachi)
//                .HasMaxLength(255)
//                .HasColumnName("DIACHI");
//            entity.Property(e => e.Email)
//                .HasMaxLength(255)
//                .HasColumnName("EMAIL");
//            entity.Property(e => e.Gioitinh)
//                .HasMaxLength(3)
//                .IsUnicode(false)
//                .HasColumnName("GIOITINH");
//            entity.Property(e => e.Hoten)
//                .HasMaxLength(50)
//                .HasColumnName("HOTEN");
//            entity.Property(e => e.Ngaysinh)
//                .HasColumnType("smalldatetime")
//                .HasColumnName("NGAYSINH");
//        });

//        modelBuilder.Entity<Khoi>(entity =>
//        {
//            entity.HasKey(e => e.Makhoi).HasName("PK__KHOI__22F41778D1CEF87B");

//            entity.ToTable("KHOI");

//            entity.Property(e => e.Makhoi)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MAKHOI");
//            entity.Property(e => e.Tenkhoi)
//                .HasMaxLength(10)
//                .HasColumnName("TENKHOI");
//        });

//        modelBuilder.Entity<ScoreType>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__LOAIDIEM__9DFB99C4CFAF6744");

//            entity.ToTable("LOAIDIEM");

//            entity.Property(e => e.Id)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MALOAIDIEM");
//            entity.Property(e => e.Multiplier).HasColumnName("HESOLD");
//            entity.Property(e => e.Name)
//                .HasMaxLength(20)
//                .HasColumnName("TENLOAIDIEM");
//        });

//        modelBuilder.Entity<Lop>(entity =>
//        {
//            entity.HasKey(e => e.Malop).HasName("PK__LOP__7A3DE211A1816766");

//            entity.ToTable("LOP");

//            entity.Property(e => e.Malop)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MALOP");
//            entity.Property(e => e.Gvcn)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("GVCN");
//            entity.Property(e => e.Makhoi)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MAKHOI");
//            entity.Property(e => e.Manamhoc)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MANAMHOC");
//            entity.Property(e => e.Siso).HasColumnName("SISO");

//            entity.HasOne(d => d.MakhoiNavigation).WithMany(p => p.Lops)
//                .HasForeignKey(d => d.Makhoi)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__LOP__MAKHOI__47DBAE45");

//            entity.HasOne(d => d.ManamhocNavigation).WithMany(p => p.Lops)
//                .HasForeignKey(d => d.Manamhoc)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__LOP__MANAMHOC__48CFD27E");
//        });

//        modelBuilder.Entity<Subject>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__MONHOC__603F69EB972B4D05");

//            entity.ToTable("MONHOC");

//            entity.Property(e => e.Id)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MAMH");
//            entity.Property(e => e.Multiplier).HasColumnName("HESO");
//            entity.Property(e => e.Name)
//                .HasMaxLength(20)
//                .HasColumnName("TENMH");
//        });

//        modelBuilder.Entity<Namhoc>(entity =>
//        {
//            entity.HasKey(e => e.Manamhoc).HasName("PK__NAMHOC__4BB084BE9CCFEF00");

//            entity.ToTable("NAMHOC");

//            entity.Property(e => e.Manamhoc)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("MANAMHOC");
//            entity.Property(e => e.Nam1).HasColumnName("NAM1");
//            entity.Property(e => e.Nam2).HasColumnName("NAM2");
//        });

//        modelBuilder.Entity<Parameter>(entity =>
//        {
//            entity.HasKey(e => e.Name).HasName("PK__THAMSO__3869154791E386F8");

//            entity.ToTable("THAMSO");

//            entity.Property(e => e.Name)
//                .HasMaxLength(255)
//                .HasColumnName("TENTHAMSO");
//            entity.Property(e => e.Value).HasColumnName("GIATRI");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
