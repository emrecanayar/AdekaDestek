using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.DataAccess.Concrete.EntityFramework.Mappings;
using AdekaDestek.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace AdekaDestek.DataAccess.Concrete.EntityFramework.Contexts
{
    public class AdekaDestekApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AdekaDestekApiContext(DbContextOptions<AdekaDestekApiContext> options) : base(options)
        {

        }
       
    }
}