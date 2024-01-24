using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ArticleApp.Models
{
    /// <summary>
    /// DbContext Class
    /// </summary>
    public class ArticleAppDbContext : DbContext
    {
        // Install-Package Microsoft.EntityFrameworkCore.SqlServer
        // Install-Package Microsoft.EntityFrameworkCore.InMemory
        // Install-Package System.Configuration.COnfigurationManager
        public ArticleAppDbContext()
        {
            // 기본 생성자는 Empty   
        }

        public ArticleAppDbContext(DbContextOptions<ArticleAppDbContext> options) : base(options)
        {
            // 공식과 같은 코드

        }

        /// <summary>
        /// .NET Core에서만 사용할땐 필요 없음
        /// .NET Framework에서 사용할때 필요 함.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 닷넷 프레임워크 기반에서 호출되는 코드 영역:
            // App.Config 또는 Web.Config의 연결 문자열 사용
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // [A] Articles 테이블의 Created 열은 자동으로 GetDate() 제약 조건을 부여하기
            modelBuilder.Entity<Article>().Property(m => m.Created).HasDefaultValueSql("GetDate()");
        }

        /// <summary>
        /// [!] ArticleApp 관련 모든 테이블 참조
        /// </summary>
        public DbSet<Article> Articles { get; set; }
        

    }
}
