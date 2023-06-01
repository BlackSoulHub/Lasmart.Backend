using System;
using System.Linq;
using System.Threading.Tasks;
using LanguageExt.Common;
using Lasmart.DAL.Repositories.Interfaces;
using Lastmart.Domain.DBModels;
using Microsoft.EntityFrameworkCore;

namespace Lasmart.DAL.Repositories.Implementation
{
    public class DotRepository : IBaseRepository<DotModel>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DotRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Result<DotModel>> CreateAsync(DotModel entity)
        {
            var addResult = await _applicationDbContext.Dots.AddAsync(entity);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return addResult.Entity;
            }
            catch (Exception exception)
            {
                return new Result<DotModel>(exception);
            }
        }

        public async Task<Result<bool>> Delete(DotModel entity)
        {
            _applicationDbContext.Dots.Remove(entity);
            
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

        public async Task<Result<DotModel>> UpdateAsync(DotModel entity)
        {
            var entityEntry = _applicationDbContext.Entry(entity);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return entityEntry.Entity;
            }
            catch (Exception exception)
            {
                return new Result<DotModel>(exception);
            }
        }

        public async Task<DotModel?> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Dots
                .Include(d => d.Comments)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public IQueryable<DotModel> GetAll()
        {
            return _applicationDbContext.Dots.AsQueryable();
        }
    }
}