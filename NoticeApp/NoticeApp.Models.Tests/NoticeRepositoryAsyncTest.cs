using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeApp.Models.Tests
{
    [TestClass]
    public class NoticeRepositoryAsyncTest
    {
        [TestMethod]
        public async Task NoticeRepositoryAsyncAllMethodTest()
        {
            #region [0] DbContextOptions<T> Object Creation and ILoggerFacotry Object Creation
            // [0] DbContextOptions<T> Object Creation and ILoggerFacotry Object Creation
            var options = new DbContextOptionsBuilder<NoticeAppDbContext>()
                //.UseInMemoryDatabase(databaseName: "ArticleApp").Options;
            .UseInMemoryDatabase(databaseName: $"NoticeApp{Guid.NewGuid()}").Options;
            //.UseSqlServer("Server=127.0.0.1,1433;database=NoticeApp;uid=rladyddn258;pwd=rladyddn!!95").Options;

            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            #endregion

            #region [1] AddAsync() method Test
            //[1] AddAsync() method Test
            using (var context = new NoticeAppDbContext(options))
            {
                //[A] Arrange
                var repository = new NoticeRepositoryAsync(context, factory);
                var model = new Notice { Name = "[1] 관리자", Title ="공지사항입니다.", Content = "내용입니다."};

                //[B] Act
                await repository.AddAsync(model); // Id : 1
            }

            using (var context = new NoticeAppDbContext(options))
            {
                //[C] Assert
                Assert.AreEqual(1, await context.Notices.CountAsync());
                
                var model = await context.Notices.Where(n => n.Id == 1).SingleOrDefaultAsync();
                Assert.AreEqual("[1] 관리자", model.Name);
            }
            #endregion

            #region [2] GetAllAsync() Method Test
            // [2] GetAllAsync() Method Test
            using (var context = new NoticeAppDbContext(options))
            {
                // 트랜잭션 관련 코드는 InMemoryDatabase 공급자에서는 지원 X
                //using (var transaction = context.Database.BeginTransaction()) { }
                
                //[A] Arrange
                var repository = new NoticeRepositoryAsync(context, factory);
                var model = new Notice { Name = "[2] 홍길동", Title = "공지사항입니다.", Content = "내용입니다." };

                //[B] Act
                await repository.AddAsync(model); // Id : 2
                await repository.AddAsync(new Notice { Name = "[3] 백두산", Title = "공지사항입니다." }); // Id : 3
                
                // transaction.Commit();
            }

            using (var context = new NoticeAppDbContext(options))
            {
                //[C] Assert
                var repository = new NoticeRepositoryAsync(context, factory);

                var models = await repository.GetAllAsync();
                Assert.AreEqual(3, models.Count()); // TotalRecords : 3
            }
            #endregion


            #region [3] GetByIdAsync() Method Test
            //[3] GetByIdAsync() Method Test
            using (var context = new NoticeAppDbContext(options))
            {
                // Empty
            }

            using (var context = new NoticeAppDbContext(options))
            {
                var repository = new NoticeRepositoryAsync(context, factory);
                var model = await repository.GetByIdAsync(2);

                Assert.IsTrue(model.Name.Contains("길동"));

                Assert.AreEqual("[2] 홍길동", model.Name);
            }
            #endregion

            #region [4] EditAsync() Method Test
            // [4] EditAsync() Method Test
            using (var context = new NoticeAppDbContext(options))
            {
                // Empty
            }

            using (var context = new NoticeAppDbContext(options))
            {
                var repository = new NoticeRepositoryAsync(context, factory);
                var model = await repository.GetByIdAsync(2);

                model.Name = "[2] 임꺽정"; // 데이터 수정
                await repository.EditAsync(model);

                var updateModel = await repository.GetByIdAsync(2);

                Assert.IsTrue(updateModel.Name.Contains("꺽정"));
                Assert.AreEqual("[2] 임꺽정", updateModel.Name);

                Assert.AreEqual("[2] 임꺽정", (await context.Notices.Where(m => m.Id == 2).SingleOrDefaultAsync())?.Name);
            }
            #endregion

            #region [5] DeleteAsync() Method Test
            // [5] DeleteAsync() Method Test
            using (var context = new NoticeAppDbContext(options))
            {
                // Empty
            }

            using (var context = new NoticeAppDbContext(options))
            {
                var repository = new NoticeRepositoryAsync(context, factory);
                await repository.DeleteAsync(2);

                Assert.AreEqual(2, (await context.Notices.CountAsync()));
                Assert.IsNull(await repository.GetByIdAsync(2));
            }
            #endregion


            #region [6] GetAllAsync(PagingAsync)() Method Test
            // [6] GetAllAsync(PagingAsync)() Method Test
            using (var context = new NoticeAppDbContext(options))
            {
                // Empty
            }

            using (var context = new NoticeAppDbContext(options))
            {
                int pageIndex = 0;
                int pageSize = 1;

                var repository = new NoticeRepositoryAsync(context, factory);
                var articleSet = await repository.GetAllAsync(pageIndex, pageSize);

                var firstName = articleSet.Records.FirstOrDefault()?.Name;
                var recordCount = articleSet.TotalRecords;

                Assert.AreEqual("[3] 백두산", firstName);
                Assert.AreEqual(2, recordCount);
            }
            #endregion

            #region [7] GetStatus() Method Test
            // [7] GetStatus() Method Test
            using (var context = new NoticeAppDbContext(options))
            {
                int parentId = 1;

                //[C] Assert
                var no1 = await context.Notices.Where(m => m.Id == 1).SingleOrDefaultAsync();
                no1.ParentId = parentId;
                no1.IsPinned = true; // 공지글로 올리기

                context.Entry(no1).State = EntityState.Modified;
                context.SaveChanges();

                var repository = new NoticeRepositoryAsync(context, factory);
                var result = await repository.GetStatus(parentId);

                Assert.AreEqual(1, result.Item1); // Pinned Count == 1
            } 
            #endregion



        }
    }

}
