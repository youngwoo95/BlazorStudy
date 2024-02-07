using Dul.Domain.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoticeApp.Models
{
    /// <summary>
    /// [6] Repository Class
    /// </summary>
    public class NoticeRepositoryAsync : INoticeRepositoryAsync
    {
        public Task<Notice> AddAsync(Notice model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditAsync(Notice model)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notice>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PagingResult<Notice>> GetAllAsync(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<PagingResult<Notice>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId)
        {
            throw new NotImplementedException();
        }

        public Task<Notice> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
