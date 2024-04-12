using WASMStudy.Shared;

namespace WASMStudy.Server.Services.Interfaces
{
    public interface IUsersService 
    {
        /// <summary>
        /// 입력
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Userinfo> AddAsync(Userinfo model);

        /// <summary>
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        Task<List<Userinfo>> GetAllAsync();

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> EditAsync(Userinfo model);

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="reqno"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int reqno);

        /// <summary>
        /// 조회 - 사용자 ID 검색
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task<Userinfo> GetByIdAsync(string userid);

    }
}
