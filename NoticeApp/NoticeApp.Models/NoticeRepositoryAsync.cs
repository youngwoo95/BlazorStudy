﻿using Dul.Domain.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoticeApp.Models
{
    /// <summary>
    /// [6] Repository Class: ADO.NET or Dapper or Entity Framework Core
    /// </summary>
    public class NoticeRepositoryAsync : INoticeRepositoryAsync
    {
        private readonly NoticeAppDbContext _context;
        private readonly ILogger _logger;

        // 생성자
        public NoticeRepositoryAsync(NoticeAppDbContext context, ILoggerFactory loggerFactory)
        {
            this._context = context;
            this._logger = loggerFactory.CreateLogger(nameof(NoticeRepositoryAsync));
        }

        // 입력
        public async Task<Notice> AddAsync(Notice model)
        {
            _context.Notices.Add(model);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError($"에러 발생({nameof(AddAsync)}) : {ex.Message}");
            }

            return model;
        }

        // 출력
        public Task<List<Notice>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        // 상세
        public Task<Notice> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        // 수정
        public Task<bool> EditAsync(Notice model)
        {
            throw new NotImplementedException();
        }

        // 삭제
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        // 페이징
        public Task<PagingResult<Notice>> GetAllAsync(int skip, int take)
        {
            throw new NotImplementedException();
        }

        // 부모
        public Task<PagingResult<Notice>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId)
        {
            throw new NotImplementedException();
        }

    
    }
}