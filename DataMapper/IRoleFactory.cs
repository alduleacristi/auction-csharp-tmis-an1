﻿using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IRoleFactory
    {
        void AddRole(Role role);
        Role GetRoleByName(String name);
    }
}