using Dul.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoticeApp.Models
{
    #region DUL
    /// <summary>
    /// Paging 처리된 레코드셋과 전체 레코드 카운트
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /*
    public struct PagingResult<T>
    {
        public IEnumerable<T> Records { get; set; }
        public int TotalRecords { get; set; }
        public PagingResult(IEnumerable<T> items, int totalRecords)
        {
            Records = items;
            TotalRecords = totalRecords;
        }
    }
    */
    #endregion

    /// <summary>
    /// [3] Generic Repository Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICrudRepositoryAsync<T>
    {
        Task<T> AddAsync(T model); // 입력
        Task<List<T>> GetAllAsync(); // 출력
        Task<T> GetByIdAsync(int id); // 상세
        Task<bool> EditAsync(T model); // 수정
        Task<bool> DeleteAsync(int id); // 삭제
        Task<PagingResult<T>> GetAllAsync(int skip, int take); // 페이징
        Task<PagingResult<T>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId); // 부모
        Task<PagingResult<T>> SearchAllAsync(int skip, int take, string searchQuery); // 검색기능
    }
}
