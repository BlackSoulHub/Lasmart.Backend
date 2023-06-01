using System;
using System.Linq;
using System.Threading.Tasks;
using LanguageExt.Common;
using Lasmart.DAL.Repositories.Interfaces;
using Lastmart.Domain.DBModels;
using Microsoft.EntityFrameworkCore;

namespace Lasmart.DAL.Repositories.Implementation
{
    public class CommentRepository : IBaseRepository<CommentModel>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        
        public CommentRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public async Task<Result<CommentModel>> CreateAsync(CommentModel entity)
        {
            var addResult = await _applicationDbContext.Comments.AddAsync(entity);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return addResult.Entity;
            }
            catch (Exception exception)
            {
                return new Result<CommentModel>(exception);
            }
        }

        public async Task<Result<bool>> Delete(CommentModel entity)
        {
            _applicationDbContext.Comments.Remove(entity);
            
            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                return new Result<bool>(exception);
            }
        }

        public async Task<Result<CommentModel>> UpdateAsync(CommentModel entity)
        {
            var entityEntry = _applicationDbContext.Entry(entity);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return entityEntry.Entity;
            }
            catch (Exception exception)
            {
                return new Result<CommentModel>(exception);
            }
        }

        public async Task<CommentModel?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Comments
                .Include(c => c.Dot)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public IQueryable<CommentModel> GetAll()
        {
            return _applicationDbContext.Comments.AsQueryable();
        }
    }
}