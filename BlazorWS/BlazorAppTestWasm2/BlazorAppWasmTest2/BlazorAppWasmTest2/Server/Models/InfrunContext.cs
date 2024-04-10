using System;
using System.Collections.Generic;
using BlazorAppWasmTest2.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlazorAppWasmTest2.Server.Models
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
        public virtual DbSet<Gnpopulation> Gnpopulations { get; set; } = null!;

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

            modelBuilder.Entity<Gnpopulation>(entity =>
            {
                entity.ToTable("GNpopulation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdministrativeAgency)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Administrative_Agency");

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
