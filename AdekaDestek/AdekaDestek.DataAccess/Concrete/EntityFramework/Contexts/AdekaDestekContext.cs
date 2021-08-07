using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.DataAccess.Concrete.EntityFramework.Mappings;
using AdekaDestek.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdekaDestek.DataAccess.Concrete.EntityFramework.Contexts
{
    public class AdekaDestekContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public AdekaDestekContext(DbContextOptions<AdekaDestekContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleMapping());
            builder.ApplyConfiguration(new UserMapping());
            builder.ApplyConfiguration(new RoleClaimMapping());
            builder.ApplyConfiguration(new UserClaimMapping());
            builder.ApplyConfiguration(new UserLoginMapping());
            builder.ApplyConfiguration(new UserRoleMapping());
            builder.ApplyConfiguration(new UserTokenMapping());
            builder.ApplyConfiguration(new LogMapping());
        }
    }
}
