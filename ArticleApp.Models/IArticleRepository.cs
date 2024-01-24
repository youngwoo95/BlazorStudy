using Dul.Domain.Common;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArticleApp.Models
{
    /// <summary>
    /// Repository Interface
    /// </summary>
    public interface IArticleRepository
    {
        /// <summary>
        /// 입력
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        Task<Article> AddArticleAsync(Article model);
        
        /// <summary>
        /// 출력
        /// </summary>
        /// <returns></returns>
        Task<List<Article>> GetArticleAsync();

        /// <summary>
        /// 상세
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Article> GetArticleByIdAsync(int id);

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        Task<Article> EditArticleAsync(Article model);

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        Task DeleteArticleAsync(int id);

        /// <summary>
        /// 페이징
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagingResult<Article>> GetAllAsync(int pageIndex, int pageSize);
    }
}
