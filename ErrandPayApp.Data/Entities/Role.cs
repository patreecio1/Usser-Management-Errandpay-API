﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrandPayApp.Data.Entities
{
    public class Role : Entity<long>
    {
        public Role()
        {
        }

        public string RoleNameHash { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}
