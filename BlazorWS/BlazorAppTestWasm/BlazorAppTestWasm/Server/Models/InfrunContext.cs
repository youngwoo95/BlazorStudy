using BlazorAppTestWasm.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppTestWasm.Server.Models
{
    public partial class InfrunContext : DbContext
    {
        public InfrunContext()
        {
        }

        public InfrunContext(DbContextOptions<InfrunContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=127.0.0.1,1433;Database=Infrun;User Id=rladyddn258;Password=rladyddn!!95");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}


// 스캐폴딩
/*
    Scaffold-DbContext 'Data Source=127.0.0.1,1433;Database=Infrun;User Id=rladyddn258;Password=rladyddn!!95' Microsoft.EntityFrameworkCore.SqlServer
 */