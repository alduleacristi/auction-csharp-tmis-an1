using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IRoleService
    {
        void AddRole(Role role);
        Role GetRoleByName(String name);
    }
}
