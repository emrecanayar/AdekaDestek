using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Entities.Dtos
{
    public class UserUpdatePasswordForAdekaDto
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string ModifiedByName { get; set; }
    }
}
