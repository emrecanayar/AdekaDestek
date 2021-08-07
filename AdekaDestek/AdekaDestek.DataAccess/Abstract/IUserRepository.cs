using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.Core.DataAccess.Abstract;
using AdekaDestek.Entities.Concrete;

namespace AdekaDestek.DataAccess.Abstract
{
    public interface IUserRepository : IEntityRepository<User>
    {

    }
}
