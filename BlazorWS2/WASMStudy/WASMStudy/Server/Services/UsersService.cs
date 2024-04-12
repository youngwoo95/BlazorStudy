using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WASMStudy.Server.DBContext;
using WASMStudy.Server.Services.Interfaces;
using WASMStudy.Shared;

namespace WASMStudy.Server.Services
{
    public class UsersService : IUsersService
    {
        private readonly YwdbContext context;

        public UsersService(YwdbContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// 입력
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Userinfo> AddAsync(Userinfo model)
        {
            try
            {
                context.Userinfos.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        
        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="reqno"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> DeleteAsync(int reqno)
        {
            try
            {
                Userinfo? model = await context.Userinfos.FindAsync(reqno);
                if (model != null)
                {
                    context.Userinfos.Remove(model);
                    return await context.SaveChangesAsync() > 0 ? true : false;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> EditAsync(Userinfo model)
        {
            try
            {
                context.Userinfos.Update(model);
                return await context.SaveChangesAsync() > 0 ? true : false;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 전체 조회
        /// </summary>
        /// <returns></returns>
        public async Task<List<Userinfo>> GetAllAsync()
        {
            try
            {
                return await context.Userinfos.ToListAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// ID조회
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<Userinfo> GetByIdAsync(string userid)
        {
            try
            {
                Userinfo? model = await context.Userinfos.FindAsync(userid);
                if (model != null)
                {
                    return model;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }catch(Exception ex)
            {
                throw;
            }
        }
    }
}
