﻿using Dul.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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
                _logger.LogError($"ERROR ({nameof(AddAsync)}) : {ex.Message}");
            }

            return model;
        }

        // 출력
        public async Task<List<Notice>> GetAllAsync()
        {
            return await _context.Notices.OrderByDescending(m => m.Id)
                //.Include(m => m.NoticesComments)
                .ToListAsync();
        }

        // 상세
        public async Task<Notice> GetByIdAsync(int id)
        {
            return await _context.Notices
                //.Include(m => m.NoticesComments)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        // 수정
        public async Task<bool> EditAsync(Notice model)
        {
            _context.Notices.Attach(model);
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch(Exception ex) 
            {
                _logger.LogError($"ERROR ({nameof(EditAsync)}) : {ex.Message}");
            }

            return false;
        }

        // 삭제
        public async Task<bool> DeleteAsync(int id)
        {
            var model = await _context.Notices
                .SingleOrDefaultAsync(m => m.Id == id);

            _context.Remove(model);

            try
            {
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch(Exception ex)
            {
                _logger.LogError($"ERROR ({nameof(DeleteAsync)}) : {ex.Message}");
            }
            
            return false;
        }

        // 페이징
        public async Task<PagingResult<Notice>> GetAllAsync(int pageIndex, int pageSize)
        {
            var totalRecords = await _context.Notices.CountAsync();
            var models = await _context.Notices
                .OrderByDescending(m => m.Id)
                //.Include(m => m.NoticesComments)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagingResult<Notice>(models, totalRecords);
        }

        // 부모
        public async Task<PagingResult<Notice>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId)
        {
            var totalRecords = await _context.Notices.Where(m => m.ParentId == parentId).CountAsync();
            var models = await _context.Notices
                .Where(m => m.ParentId == parentId)
                .OrderByDescending(m => m.Id)
                //.Include(m => m.NoticesComments)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            return new PagingResult<Notice>(models, totalRecords);
        }

        // 상태
        public async Task<Tuple<int, int>> GetStatus(int parentId)
        {
            var totalRecords = await _context.Notices.Where(m => m.ParentId == parentId).CountAsync();
            var pinnedRecords = await _context.Notices.Where(m => m.ParentId == parentId && m.IsPinned == true).CountAsync();

            return new Tuple<int, int>(pinnedRecords, totalRecords); //2, 10
        }

        // 삭제 by ParentID
        public async Task<bool> DeleteAllByParentId(int parentId)
        {
            try
            {
                var models = await _context.Notices
                    .Where(m => m.ParentId == parentId)
                    .ToListAsync();

                foreach(var model in models)
                {
                    _context.Notices.Remove(model);
                }

                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch(Exception ex)
            {
                _logger.LogError($"ERROR ({nameof(DeleteAllByParentId)}) : {ex.Message}");
            }

            return false;
        }
    }
}
