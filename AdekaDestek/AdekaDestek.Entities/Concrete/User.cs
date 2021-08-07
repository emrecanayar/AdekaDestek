using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.Core.Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace AdekaDestek.Entities.Concrete
{
    public class User : IdentityUser<int>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SapUserName { get; set; }
        public string SapEmployeeNo { get; set; }
        public string InfiniUserName { get; set; }
        public SByte? TwoFactorType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }
        public bool IsActive { get; set; }
    }
}
