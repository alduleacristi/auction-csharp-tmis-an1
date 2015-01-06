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
        bool AddRole(Role role);
        Role GetRoleByName(String name);
        bool UpdateRole(String oldRoleName,String newRoleName);
        bool DropRole(String roleName);
        ICollection<Role> GetRolesFromAnUser(User user);
    }
}
