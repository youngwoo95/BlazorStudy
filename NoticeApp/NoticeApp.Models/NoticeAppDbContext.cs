﻿using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace NoticeApp.Models
{
    /// <summary>
    /// [5] DbContext Class
    /// </summary>
    public class NoticeAppDbContext : DbContext
    {
        public NoticeAppDbContext()
        {
            // Empty
        }

        public NoticeAppDbContext(DbContextOptions<NoticeAppDbContext> options) : base(options)
        {
            // 공식과 같은 코드
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 닷넷 프레임워크 기반에서 호출되는 코드 영역:
            // App.config 또는 Web.config의 연결 문자열 사용
            /*
            if(!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
            */
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notice>().Property(m => m.Created).HasDefaultValueSql("GetDate()");
        }

        public DbSet<Notice> Notices { get; set; }
    }
}
