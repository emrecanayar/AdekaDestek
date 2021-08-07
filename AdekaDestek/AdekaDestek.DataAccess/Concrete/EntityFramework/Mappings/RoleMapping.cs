using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdekaDestek.DataAccess.Concrete.EntityFramework.Mappings
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Primary key
            builder.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            builder.ToTable("Roles");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.Name).HasMaxLength(100);
            builder.Property(u => u.NormalizedName).HasMaxLength(100);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            builder.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany<RoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

            builder.HasData(
                 new Role
                 {
                     Id = 1,
                     Name = "Payroll.Create",
                     NormalizedName = "PAYROLL.CREATE",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 }
                 ,
                 new Role
                 {
                     Id = 2,
                     Name = "Payroll.Read",
                     NormalizedName = "PAYROLL.READ",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 },
                 new Role
                 {
                     Id = 3,
                     Name = "Payroll.Update",
                     NormalizedName = "PAYROLL.UPDATE",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 }
                 ,
                 new Role
                 {
                     Id = 4,
                     Name = "Payroll.Delete",
                     NormalizedName = "PAYROLL.DELETE",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 },
                new Role
                {
                    Id = 5,
                    Name = "AnnualPermit.Create",
                    NormalizedName = "ANNUALPERMIT.CREATE",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
                 ,
                new Role
                {
                    Id = 6,
                    Name = "AnnualPermit.Read",
                    NormalizedName = "ANNUALPERMIT.READ",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = 7,
                    Name = "AnnualPermit.Update",
                    NormalizedName = "ANNUALPERMIT.UPDATE",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
                 ,
                 new Role
                 {
                     Id = 8,
                     Name = "AnnualPermit.Delete",
                     NormalizedName = "ANNUALPERMIT.DELETE",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 }
                 ,
                 new Role
                 {
                     Id = 9,
                     Name = "User.Create",
                     NormalizedName = "USER.CREATE",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 }
                 ,
                 new Role
                 {
                     Id = 10,
                     Name = "User.Read",
                     NormalizedName = "USER.READ",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 },
                 new Role
                 {
                     Id = 11,
                     Name = "User.Update",
                     NormalizedName = "USER.UPDATE",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 }
                 ,
                 new Role
                 {
                     Id = 12,
                     Name = "User.Delete",
                     NormalizedName = "USER.DELETE",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 }
                 ,
                 new Role
                 {
                     Id = 13,
                     Name = "Role.Create",
                     NormalizedName = "ROLE.CREATE",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 }
                 ,
                 new Role
                 {
                     Id = 14,
                     Name = "Role.Read",
                     NormalizedName = "ROLE.READ",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 },
                 new Role
                 {
                     Id = 15,
                     Name = "Role.Update",
                     NormalizedName = "ROLE.UPDATE",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 }
                 ,
                 new Role
                 {
                     Id = 16,
                     Name = "Role.Delete",
                     NormalizedName = "ROLE.DELETE",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 }
                 ,
                 new Role
                 {
                     Id = 17,
                     Name = "AdminArea.Home.Read",
                     NormalizedName = "ADMINAREA.HOME.READ",
                     ConcurrencyStamp = Guid.NewGuid().ToString()
                 },
                new Role
                {
                    Id = 18,
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = 19,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = 20,
                    Name = "Editor",
                    NormalizedName = "EDITOR",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = 21,
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
             );

        }


    }
}
