using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Core.Utilities.Encryptions;
using AdekaDestek.Entities.Concrete;
using Microsoft.AspNetCore.Identity;

namespace AdekaDestek.IdentityAPI.CustomRules
{
    //Custom olarak MD5 şifreleme algoritmasını ASP.NET Identity üzerine extend ettik.
    public class CustomPasswordHasher : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            return EncryptPassword.GetMD5Hash(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            if (hashedPassword == EncryptPassword.GetMD5Hash(providedPassword))
            {
                return PasswordVerificationResult.Success;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }
}
