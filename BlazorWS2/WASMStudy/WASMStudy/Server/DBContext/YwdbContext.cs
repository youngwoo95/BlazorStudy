using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WASMStudy.Shared;

namespace WASMStudy.Server.DBContext;

public partial class YwdbContext : DbContext
{
    public YwdbContext()
    {
    }

    public YwdbContext(DbContextOptions<YwdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Compinfo> Compinfos { get; set; }
    public virtual DbSet<Deptinfo> Deptinfos { get; set; }
    public virtual DbSet<Userinfo> Userinfos { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Data Source=123.2.156.122,1002;Database=YWDB;User Id=stec;Password=stecdev1234!;TrustServerCertificate=true;");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=127.0.0.1,1433;Database=YWDB;User Id=rladyddn258;Password=rladyddn!!95;TrustServerCertificate=true;");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Compinfo>(entity =>
        {
            entity.HasKey(e => e.Compid).HasName("PK__COMPINFO__C0EBDBE6FE4CC333");

            entity.ToTable("COMPINFO");

            entity.Property(e => e.Compid).HasColumnName("COMPID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Compname)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("COMPNAME");
            entity.Property(e => e.Createdt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATEDT");
            entity.Property(e => e.Tel)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("TEL");
        });

        modelBuilder.Entity<Deptinfo>(entity =>
        {
            entity.HasKey(e => e.Deptid).HasName("PK__DEPTINFO__E0EBD3ACAAAFD5EB");

            entity.ToTable("DEPTINFO");

            entity.Property(e => e.Deptid).HasColumnName("DEPTID");
            entity.Property(e => e.Compid).HasColumnName("COMPID");
            entity.Property(e => e.Createdt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATEDT");
            entity.Property(e => e.Deptname)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("DEPTNAME");
        });

        modelBuilder.Entity<Userinfo>(entity =>
        {
            entity.HasKey(e => e.Reqno).HasName("PK__USERINFO__421A7E296FAD52F3");

            entity.ToTable("USERINFO");

            entity.Property(e => e.Reqno).HasColumnName("REQNO");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Compid).HasColumnName("COMPID");
            entity.Property(e => e.Createdt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATEDT");
            entity.Property(e => e.Deptid).HasColumnName("DEPTID");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("PHONENUMBER");
            entity.Property(e => e.Userid)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("USERID");
            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
            entity.Property(e => e.Userpw)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("USERPW");

            entity.HasOne(d => d.Comp).WithMany(p => p.Userinfos)
                .HasForeignKey(d => d.Compid)
                .HasConstraintName("COMP_FK");

            entity.HasOne(d => d.Dept).WithMany(p => p.Userinfos)
                .HasForeignKey(d => d.Deptid)
                .HasConstraintName("DEPT_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
