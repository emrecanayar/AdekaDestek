using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace AdekaDestek.DataAccess.Concrete.EntityFramework.Contexts
{
    public class HelpDeskContext : DbContext
    {
        public DbSet<USER> USERS { get; set; }
        public HelpDeskContext(DbContextOptions<HelpDeskContext> options) : base(options)
        {

        }
    }
}
