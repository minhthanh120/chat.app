using Foundation.Business.Repositories.Abtractions;
using Foundation.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation.Business.Repositories.Implements
{
    public class UserCredentialRepository: BaseRepository<UserCredentials>, IUserCredentialRepository
    {
        public UserCredentialRepository(ApplicationDbContext context): base(context)
        {
            
        }
    }
}
