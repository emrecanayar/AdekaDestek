using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.Core.DataAccess.Concrete.EntityFramework;
using AdekaDestek.DataAccess.Abstract;
using AdekaDestek.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace AdekaDestek.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfUserRepository : EfEntityRepositoryBase<User>, IUserRepository
    {
        public EfUserRepository(DbContext context) : base(context)
        {

        }
    }
}
