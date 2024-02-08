﻿using Microsoft.EntityFrameworkCore;
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
                var model = new Notice { Name = "관리자", Title ="공지사항입니다.", Content = "내용입니다."};

                //[B] Act
                await repository.AddAsync(model); // Id : 1
            }

            using (var context = new NoticeAppDbContext(options))
            {
                //[C] Assert
                Assert.AreEqual(1, await context.Notices.CountAsync());
                
                var model = await context.Notices.Where(n => n.Id == 1).SingleOrDefaultAsync();
                Assert.AreEqual("관리자", model.Name);
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
                var model = new Notice { Name = "홍길동", Title = "공지사항입니다.", Content = "내용입니다." };

                //[B] Act
                await repository.AddAsync(model); // Id : 2
                await repository.AddAsync(new Notice { Name = "백두산", Title = "공지사항입니다." }); // Id : 3
                
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


        }
    }

}
