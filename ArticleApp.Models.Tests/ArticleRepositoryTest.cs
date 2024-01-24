using Microsoft.EntityFrameworkCore;

namespace ArticleApp.Models.Tests
{
    [TestClass]
    public class ArticleRepositoryTest
    {
        [TestMethod]
        public async Task ArticleRepositoryAllMethodTest()
        {
            // DbContextOptions<T> Object 생성
            var options = new DbContextOptionsBuilder<ArticleAppDbContext>()
                //.UseInMemoryDatabase(databaseName: "ArticleApp").Options;
                //.UseSqlServer("server=(localdb)\\mssqllocaldb;database=ArticleApp;integrated security=true;").Options;
                .UseSqlServer("Server=127.0.0.1,1433;database=ArticleApp;uid=rladyddn258;pwd=rladyddn!!95").Options;

            // AddAsync() Method Test
            using (var context = new ArticleAppDbContext(options))
            {
                // Repository Object 생성
                var repository = new ArticleRepository(context);
                var model = new Article {Title = "[1] 게시판 시작", Created = DateTime.Now, CreatedBy ="KKK" };
                await repository.AddArticleAsync(model);
                await context.SaveChangesAsync();
            }
            
            using (var context = new ArticleAppDbContext(options))
            {
                Assert.AreEqual(1, await context.Articles.CountAsync());

                var model = await context.Articles.Where(m => m.Id == 1).SingleOrDefaultAsync();
                Assert.AreEqual("[1] 게시판 시작", model?.Title);
            }

            // GetAllAsync() Method Test
            using (var context = new ArticleAppDbContext(options))
            {
                // Repository Object 생성
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var repository = new ArticleRepository(context);
                        var model = new Article {Title = "[2] 게시판 가동", Created = DateTime.Now, CreatedBy = "KKK" };
                        context.Articles.Add(model);
                        await context.SaveChangesAsync(); // [1] 저장

                        context.Articles.Add(new Article {Title = "[3] 게시판 중지", Created = DateTime.Now, CreatedBy = "KKK" });
                        await context.SaveChangesAsync(); // [2] 저장

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Empty
                    }
                }
            }

            using (var context = new ArticleAppDbContext(options))
            {
                var repository = new ArticleRepository(context);
                var models = await repository.GetArticleAsync();

                Assert.AreEqual(3, models.Count);
            }

            // GetById() Method 테스트
            using (var context = new ArticleAppDbContext(options))
            {
            }

            // 확인
            using (var context = new ArticleAppDbContext(options))
            {
                var repository = new ArticleRepository(context);
                var models = await repository.GetArticleByIdAsync(2);
                Assert.IsTrue(models?.Title.Contains("가동"));
                Assert.AreEqual("[2] 게시판 가동", models?.Title);
            }

            // GetEditAsync() Method 테스트
            using (var context = new ArticleAppDbContext(options))
            {
            }

            // 확인
            using (var context = new ArticleAppDbContext(options))
            {
                var repository = new ArticleRepository(context);
                var models = await repository.GetArticleByIdAsync(2);

                models.Title = "[2] 게시판 바쁨";

                await repository.EditArticleAsync(models);
                await context.SaveChangesAsync();

                Assert.AreEqual("[2] 게시판 바쁨", (await context.Articles.Where(m => m.Id == 2).SingleOrDefaultAsync()).Title);
            }


            // GetDeleteAsync() Method 테스트
            using (var context = new ArticleAppDbContext(options))
            {
            }

            // 확인
            using (var context = new ArticleAppDbContext(options))
            {
                var repository = new ArticleRepository(context);

                await repository.DeleteArticleAsync(2);
                await context.SaveChangesAsync();

                Assert.AreEqual(2, await context.Articles.CountAsync());
                Assert.IsNull(await repository.GetArticleByIdAsync(2));
            }


            // PagingAsync() Method 테스트
            using (var context = new ArticleAppDbContext(options))
            {
            }

            // 확인
            using (var context = new ArticleAppDbContext(options))
            {
                int pageIndex = 0;
                int pageSize = 1;


                var repository = new ArticleRepository(context);
                var model = await repository.GetAllAsync(pageIndex, pageSize);

                Assert.AreEqual("[3] 게시판 중지", model.Records.FirstOrDefault()?.Title);
                Assert.AreEqual(2, model.TotalRecords);
            }

        }
    }
}
