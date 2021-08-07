using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.Core.DataAccess.Concrete.EntityFramework;
using AdekaDestek.Core.Entities.Concrete;
using AdekaDestek.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AdekaDestek.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfUserHelpDeskRepository : EfEntityRepositoryBase<USER>, IUserHelpDeskRepository
    {
        public EfUserHelpDeskRepository(DbContext context) : base(context)
        {

        }
    }
}
