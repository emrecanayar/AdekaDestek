using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Entities.Dtos
{
    public class UserUpdatePasswordForInfiniDto
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public string ModifiedByName { get; set; }
    }
}
