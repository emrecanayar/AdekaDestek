using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Entities.Dtos;

namespace AdekaDestek.Mvc.Areas.Admin.Models
{
    public class UserAddAjaxViewModel
    {
        public UserAddDto UserAddDto { get; set; }
        public string UserAddPartial { get; set; }
        public UserDto UserDto { get; set; }
    }
}
