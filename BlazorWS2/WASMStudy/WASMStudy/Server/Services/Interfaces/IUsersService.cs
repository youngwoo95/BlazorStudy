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
        ValueTask<Userinfo> AddAsync(Userinfo model);

        /// <summary>
        /// 전체조회
        /// </summary>
        /// <returns></returns>
        ValueTask<List<Userinfo>> GetAllAsync();

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueTask<bool> EditAsync(Userinfo model);

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="reqno"></param>
        /// <returns></returns>
        ValueTask<bool> DeleteAsync(int reqno);

        /// <summary>
        /// 조회 - 사용자 ID 검색
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        ValueTask<Userinfo> GetByIdAsync(string userid);

        /// <summary>
        /// 조회 - REQNO 검색
        /// </summary>
        /// <param name="reqno"></param>
        /// <returns></returns>
        ValueTask<Userinfo> GetByReqNoAsync(int reqno);
    }
}
