using AdekaDestek.DataAccess.Abstract;
using AdekaDestek.DataAccess.Concrete.EntityFramework.Contexts;
using AdekaDestek.DataAccess.Concrete.EntityFramework.Repositories;
using Microsoft.Extensions.Caching.Memory;
using ProgrammersBlog.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdekaDestekApiContext _context;
        private EfUserRepository _userRepository;

        public UnitOfWork(AdekaDestekApiContext context)
        {
            _context = context;
           
        }

        public IUserRepository Users => _userRepository ?? new EfUserRepository(_context);



        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync() //Kaydetme işlemi
        {
            return await _context.SaveChangesAsync();
        }
    }
}
