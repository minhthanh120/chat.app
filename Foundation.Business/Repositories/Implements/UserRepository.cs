using Foundation.Business.Repositories.Abtractions;
using Foundation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Business.Repositories.Implements
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) :base(context)
        {
            
        }

        public async Task<User> FindByEmailorPhone(string key)
        {
            var user = await this.GetByWhereAsync(u => u.Email == key || u.Phone == key);
            return user;
        }
    }
}
