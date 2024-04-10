using System;
using System.Collections.Generic;
using BlazorAppTestWasm.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<GanamguPopulation> GanamguPopulations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
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

            modelBuilder.Entity<GanamguPopulation>(entity =>
            {
                entity.ToTable("ganamgu_population");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdministrativeAgenct)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Administrative_Agenct");

                entity.Property(e => e.FemalePopulation).HasColumnName("Female_Population");

                entity.Property(e => e.MalePopulation).HasColumnName("Male_Population");

                entity.Property(e => e.NumberOfHouseholdes).HasColumnName("Number_of_householdes");

                entity.Property(e => e.NumberOfPeoplePerHousehold).HasColumnName("number_of_people_per_household");

                entity.Property(e => e.SexRatio).HasColumnName("Sex_ratio");

                entity.Property(e => e.TotalPopulation).HasColumnName("Total_Population");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
