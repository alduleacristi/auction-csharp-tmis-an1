using DataMapper;
using DataMapper.Exceptions;
using DomainModel;
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

        public void AddRole(Role role)
        {
            logger.logInfo("Try to add a new role.");

            try
            { 
                DataMapperFactoryMethod.GetCurrentFactory().RoleFactory.AddRole(role);
                logger.logInfo("The role with name "+role.Name+" was succesfully added.");
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

        public void UpdateRole(String oldRoleName,String newRoleName)
        {
            logger.logInfo("Try to update the role with name "+oldRoleName);

            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().RoleFactory.UpdateRole(oldRoleName, newRoleName);
                logger.logInfo("Role name was succesfully changed to " + newRoleName);
            }
            catch (EntityNotFoundException entityNotFound)
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

        public void DropRole(String roleName)
        {
            logger.logInfo("Try to remove the role with name " + roleName);
            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().RoleFactory.DropRole(roleName);
                logger.logInfo("The role with name " + roleName + " was dropped succesfully");
            }
            catch (EntityNotFoundException entityNotFound)
            {
                logger.logError(entityNotFound);
                throw entityNotFound;
            }
        }
    }
}
