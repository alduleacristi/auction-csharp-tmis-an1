using DataMapper.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapper.EFDataMapper;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using DomainModel;
using System.Data.Entity.Validation;


namespace DataMapper.EFDataMapper
{
    class EFRoleFactory:IRoleFactory
    {
        public void AddRole(Role role)
        {   
            Role auxRole = this.GetRoleByName(role.Name);
            if (auxRole != null)
                throw new DuplicateException("You can not add two roles with the same name ("+ role.Name+").");
        
            using(var context = new AuctionModelContainer())
            {
                context.Roles.Add(role);
                try
                {
                    context.SaveChanges();
                }
                catch(DbEntityValidationException exc)
                {
                    throw new ValidationException("Invalid role name {" + role.Name + "}");
                }
            }
        }

        public Role GetRoleByName(String name)
        {
            using(var context = new AuctionModelContainer())
            {
                var roleVar = (from role in context.Roles where role.Name.Equals(name)
                                  select role).FirstOrDefault();
                return roleVar;
            }
        }

        public void UpdateRole(String oldRoleName,String newRoleName)
        {
            Role role = this.GetRoleByName(oldRoleName);
            if (role == null)
                throw new EntityDoesNotExistException("The role with name " + oldRoleName + " does not exist.");

            if (!oldRoleName.Equals(newRoleName))
            {
                Role auxRole = this.GetRoleByName(newRoleName);
                if (auxRole != null)
                    throw new DuplicateException("The role with name {"+newRoleName+"} already exist.");
                role.Name = newRoleName;

                using (var context = new AuctionModelContainer())
                {
                    context.Roles.Attach(role);
                    var entry = context.Entry(role);
                    entry.Property(r => r.Name).IsModified = true;

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbEntityValidationException exc)
                    {
                        throw new ValidationException("Invalid role name {" + role.Name + "}");
                    }
                }
            }
        }

        public void DropRole(String roleName)
        {
            Role role = this.GetRoleByName(roleName);
            if (role == null)
                throw new EntityDoesNotExistException("The role with name " + roleName + " does not exist.");

            using (var context = new AuctionModelContainer())
            {
                context.Roles.Attach(role);
                context.Roles.Remove(role);
                context.SaveChanges();
            }
        }

        public ICollection<Role> GetAllRolesOfAnUser(User user)
        {
            using(var context = new AuctionModelContainer())
            {
                var roleVar = (from role in context.Roles
                               select role).ToList();

                for (int i = 0; i < roleVar.Count;i++ )
                {
                    ICollection<User> users = roleVar.ElementAt(i).Users;
                    bool ok = false;
                    foreach (User userFor in users)
                        if (userFor.Email.Equals(user.Email))
                            ok = true;
                    if(!ok)
                        roleVar.Remove(roleVar.ElementAt(i));
                }

                return roleVar;
            }
        }
    }
}
