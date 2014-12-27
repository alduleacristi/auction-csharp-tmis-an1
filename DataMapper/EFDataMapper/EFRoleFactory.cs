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
            /*var validationResults = Validation.Validate<Role>(role);
            if (!validationResults.IsValid)
            {
                throw new ValidationException("Invalid role name.");
            }*/
            
            Role auxRole = this.GetRoleByName(role.Name);
            if (auxRole != null)
                throw new DuplicateException("You can not add two roles with the same name ("+ role.Name+").");
        
            using(var context = new AuctionModelContainer())
            {
                context.Roles.Add(role);
                try
                {
                    context.SaveChanges();
                }catch(DbEntityValidationException exc)
                {
                    throw new ValidationException("Invalid role name.");
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
    }
}
