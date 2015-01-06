using DataMapper;
using DataMapper.Exceptions;
using DomainModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using ServiceLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class RoleService:IRoleService
    {
        private AuctionLogger logger;

        public RoleService()
        {
            logger = AuctionLogger.GetInstance();
        }

        public bool AddRole(Role role)
        {
            logger.logInfo("Try to add a new role.");

            try
            {
                Role auxRole = this.GetRoleByName(role.Name);
                if (auxRole != null)
                    throw new DuplicateException("You can not add two roles with the same name (" + role.Name + ").");

                var validationResults = Validation.Validate<Role>(role);
                if (!validationResults.IsValid)
                {
                    throw new ValidationException("Invalid role name {" + role.Name + "}");
                }

                DataMapperFactoryMethod.GetCurrentFactory().RoleFactory.AddRole(role);
                logger.logInfo("The role with name "+role.Name+" was succesfully added.");
                return true;
            }
            catch(ValidationException validationException)
            {
                logger.logError(validationException);
                throw validationException;
            }
            catch (DuplicateException duplicateException)
            {
                logger.logError(duplicateException);
                throw duplicateException;
            }
        }

        public Role GetRoleByName(String name)
        {
            return DataMapperFactoryMethod.GetCurrentFactory().RoleFactory.GetRoleByName(name);
        }

        public bool UpdateRole(String oldRoleName,String newRoleName)
        {
            logger.logInfo("Try to update the role with name "+oldRoleName);

            try
            {
                Role role = this.GetRoleByName(oldRoleName);
                if (role == null)
                    throw new EntityDoesNotExistException("The role with name " + oldRoleName + " does not exist.");

                if (!oldRoleName.Equals(newRoleName))
                {
                    Role auxRole = this.GetRoleByName(newRoleName);
                    if (auxRole != null)
                        throw new DuplicateException("The role with name {" + newRoleName + "} already exist.");
                    role.Name = newRoleName;

                    var validationResults = Validation.Validate<Role>(role);
                    if (!validationResults.IsValid)
                    {
                        throw new ValidationException("Invalid role name {" + role.Name + "}");
                    }

                    DataMapperFactoryMethod.GetCurrentFactory().RoleFactory.UpdateRole(role);
                    logger.logInfo("Role name was succesfully changed to " + newRoleName);
                }

                return true;
            }
            catch (EntityDoesNotExistException entityNotFound)
            {
                logger.logError(entityNotFound);
                throw entityNotFound;
            }
            catch (DuplicateException duplicateException)
            {
                logger.logError(duplicateException);
                throw duplicateException;
            }
            catch (ValidationException validationException)
            {
                logger.logError(validationException);
                throw validationException;
            }
        }

        public bool DropRole(String roleName)
        {
            logger.logInfo("Try to remove the role with name " + roleName);
            try
            {
                Role role = this.GetRoleByName(roleName);
                if (role == null)
                    throw new EntityDoesNotExistException("The role with name " + roleName + " does not exist.");

                DataMapperFactoryMethod.GetCurrentFactory().RoleFactory.DropRole(role);
                logger.logInfo("The role with name " + roleName + " was dropped succesfully");
                return true;
            }
            catch (EntityDoesNotExistException entityNotFound)
            {
                logger.logError(entityNotFound);
                throw entityNotFound;
            }
        }

        public ICollection<Role> GetRolesFromAnUser(User user)
        {
            return DataMapperFactoryMethod.GetCurrentFactory().RoleFactory.GetAllRolesOfAnUser(user);
        }
    }
}
