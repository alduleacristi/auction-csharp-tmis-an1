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
            using(var context = new AuctionModelContainer())
            {
                context.Roles.Add(role);
                context.SaveChanges();      
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

        public void UpdateRole(Role role)
        {
           using (var context = new AuctionModelContainer())
           {
                context.Roles.Attach(role);
                var entry = context.Entry(role);
                entry.Property(r => r.Name).IsModified = true;

                context.SaveChanges();
           }   
        }

        public void DropRole(Role role)
        {
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
                    if (!ok)
                    {
                        roleVar.Remove(roleVar.ElementAt(i));
                        i--;
                    }
                }

                return roleVar;
            }
        }
    }
}
