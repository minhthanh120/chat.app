using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Business
{
    public class UnitOfWorks
    {
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWorks(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task SaveChanges()
        {
            await this._dbContext.SaveChangesAsync();
        }
    }
}
