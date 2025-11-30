using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Foundation.Business.Repositories
{
    public class BaseRepository<T>: IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Add(T entity)
        {
            var result = await this._dbContext.Set<T>().AddAsync(entity);
            await this._dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Add(IEnumerable<T> entities)
        {
            await this._dbContext.Set<T>().AddRangeAsync(entities);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<T> Update(T entity)
        {
            var result = this._dbContext.Set<T>().Update(entity);
            await this._dbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<T> GetById(object id)
        {
            return await this._dbContext.FindAsync<T>(id);
        }
        public async void Delete(object id)
        { 

            var target = await this.GetById(id);
            if (target ==null)
            {
                return;
            }
            _dbContext.Set<T>().Remove(target);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<T> GetByWhereAsync(Expression<Func<T, bool>> predicate)
        {
            // _dbSet.Where(predicate) sẽ tạo ra câu SQL có mệnh đề WHERE
            return await this._dbContext.Set<T>().Where(predicate).FirstAsync();
        }
        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return await this._dbContext.Set<T>().Where(predicate).ToListAsync();
        }
    }
}
