﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Entities.Dtos
{
    public class RoleAssignDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool HasRole { get; set; }
    }
}