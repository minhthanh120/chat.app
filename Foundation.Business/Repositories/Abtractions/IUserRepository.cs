using Foundation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation.Business.Repositories.Abtractions
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> FindByEmailorPhone(string key);
    }
}
