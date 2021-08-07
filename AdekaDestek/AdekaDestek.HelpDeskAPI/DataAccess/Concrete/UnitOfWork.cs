using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.DataAccess.Abstract;
using AdekaDestek.DataAccess.Concrete.EntityFramework.Contexts;
using AdekaDestek.DataAccess.Concrete.EntityFramework.Repositories;
using AdekaDestek.HelpDeskAPI.DataAccess.Abstract;

namespace AdekaDestek.HelpDeskAPI.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HelpDeskContext _context;
        private EfUserHelpDeskRepository _userHelpDeskRepository;

        public UnitOfWork(HelpDeskContext context)
        {
            _context = context;

        }
        public IUserHelpDeskRepository HelpDeskUsers => _userHelpDeskRepository ?? new EfUserHelpDeskRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
