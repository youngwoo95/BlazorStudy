using Dul.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleApp.Models
{
    /// <summary>
    /// Repository Class : ADO.NET 또는 Dapper 또는 Entity Framework Core
    /// </summary>
    public class ArticleRepository : IArticleRepository
    {
        private readonly ArticleAppDbContext _context;

        public ArticleRepository(ArticleAppDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// 입력 메서드 구현
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Article> AddArticleAsync(Article model)
        {
            _context.Articles.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        /// <summary>
        /// 출력
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Article>> GetArticleAsync()
        {
            return await _context.Articles.OrderByDescending(m => m.Id).ToListAsync();
        }

        /// <summary>
        /// 상세보기
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Article> GetArticleByIdAsync(int id)
        {
            return await _context.Articles.Where(m => m.Id == id).SingleOrDefaultAsync();
            //return await _context.Articles.FindAsync(id);
        }

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Article> EditArticleAsync(Article model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task DeleteArticleAsync(int id)
        {
            //var model = await _context.Articles.FindAsync(id);
            var model = await _context.Articles.Where(m => m.Id == id).SingleOrDefaultAsync();

            if(model != null)
            {
                _context.Articles.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 페이징
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PagingResult<Article>> GetAllAsync(int pageIndex, int pageSize)
        {
            var totalRecords = await _context.Articles.CountAsync();
            var model = await _context.Articles
                .OrderByDescending(m => m.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<Article>(model, totalRecords);
        }




    
     
    
    }
}
