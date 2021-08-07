using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.DataAccess.Abstract;

namespace AdekaDestek.HelpDeskAPI.DataAccess.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IUserHelpDeskRepository HelpDeskUsers { get; }

        Task<int> SaveAsync();
    }
}
