using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.DataAccess.Abstract;

namespace ProgrammersBlog.DataAccess.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        IUserRepository Users { get; }
 
        Task<int> SaveAsync();
    }
}
