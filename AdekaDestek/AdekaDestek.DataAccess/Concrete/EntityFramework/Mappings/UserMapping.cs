using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdekaDestek.DataAccess.Concrete.EntityFramework.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // About
            builder.Property(u => u.FirstName).HasMaxLength(30);
            builder.Property(u => u.LastName).HasMaxLength(30);
            builder.Property(u => u.SapEmployeeNo).HasMaxLength(15);
            builder.Property(u => u.SapUserName).HasMaxLength(12);
            builder.Property(u => u.InfiniUserName).HasMaxLength(20);
            builder.Property(u => u.TwoFactorType).HasMaxLength(1);
            builder.Property(u => u.IsActive).HasMaxLength(1);
            builder.Property(u => u.CreatedByName).HasMaxLength(30);
            builder.Property(u => u.ModifiedByName).HasMaxLength(30);
            builder.Property(u => u.CreatedDate).HasMaxLength(30);
            builder.Property(u => u.ModifiedDate).HasMaxLength(30);

            // Primary key
            builder.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

            // Maps to the AspNetUsers table
            builder.ToTable("Users");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.UserName).HasMaxLength(50);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(50);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(100);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            builder.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            var adminUser = new User
            {
                Id = 1,
                UserName = "adminuser",
                NormalizedUserName = "ADMINUSER",
                Email = "adminuser@adeka.com.tr",
                NormalizedEmail = "ADMINUSER@ADEKA.COM.TR",
                FirstName = "Admin",
                LastName = "User",
                SapEmployeeNo = "999",
                SapUserName = "admin.user",
                InfiniUserName = "admin.user",
                PhoneNumber = "+905555555555",
                CreatedDate = DateTime.Now,
                CreatedByName = "Emre Can Ayar",
                ModifiedDate = DateTime.Now,
                ModifiedByName = "Emre Can Ayar",
                TwoFactorType = 0,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                IsActive = true

            };
            adminUser.PasswordHash = CreatePasswordHash(adminUser, "adminuser");

            builder.HasData(adminUser);
        }

        private string CreatePasswordHash(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}
