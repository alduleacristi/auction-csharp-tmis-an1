using DataMapper;
using DataMapper.Exceptions;
using DomainModel;
using ServiceLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServicesImplementation
{
    public class UserService:IUserService
    {
        private AuctionLogger logger;

        public UserService()
        {
            logger = AuctionLogger.GetInstance();
        }

        public void AddUser(User user)
        {
            logger.logInfo("Try to add a new user.");

            try
            { 
                DataMapperFactoryMethod.GetCurrentFactory().UserFactory.AddUser(user);
                logger.logInfo("The user with name " + user.FirstName +" "+user.LastName+ " was succesfully added.");
            }
            catch (ValidationException validationException)
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

        public void UpdateFirstName(String email,String newFirstName)
        {
            logger.logInfo("Try to update the first name of the user with email " + email);

            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().UserFactory.UpdateFirstName(email, newFirstName);
                logger.logInfo("First name was succesfully changed to " + newFirstName);
            }
            catch (EntityNotFoundException entityNotFound)
            {
                logger.logError(entityNotFound);
                throw entityNotFound;
            }
            catch (ValidationException validationException)
            {
                logger.logError(validationException);
                throw validationException;
            }
        }

        public void UpdateLastName(String email,String newLastName)
        {
            logger.logInfo("Try to update the last name of the user with email " + email);

            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().UserFactory.UpdateLastName(email, newLastName);
                logger.logInfo("Last name was succesfully changed to " + newLastName);
            }
            catch (EntityNotFoundException entityNotFound)
            {
                logger.logError(entityNotFound);
                throw entityNotFound;
            }
            catch (ValidationException validationException)
            {
                logger.logError(validationException);
                throw validationException;
            }
        }

        public void UpdateEmail(String oldEmail,String newEmail)
        {
            logger.logInfo("Try to update the user email " + oldEmail);

            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().UserFactory.UpdateEmail(oldEmail, newEmail);
                logger.logInfo("Email was succesfully changed to " + newEmail);
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

        public void DropUser(String email)
        {

        }

        public void AddRoleToUser(String email,Role role)
        {
            DataMapperFactoryMethod.GetCurrentFactory().UserFactory.AddRoleToUser(email,role);
        }

        public void RemoveRoleFromUser(String email,Role role)
        {

        }
    }
}
