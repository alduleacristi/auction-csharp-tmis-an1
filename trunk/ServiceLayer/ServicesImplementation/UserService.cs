﻿using DataMapper;
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
    public class UserService:IUserService
    {
        private AuctionLogger logger;

        public UserService()
        {
            logger = AuctionLogger.GetInstance();
        }

        public User GetUserByEmail(String email)
        {
            return DataMapperFactoryMethod.GetCurrentFactory().UserFactory.GetUserByEmail(email);
        }

        public bool AddUser(User user)
        {
            logger.logInfo("Try to add a new user.");

            try
            { 
                DataMapperFactoryMethod.GetCurrentFactory().UserFactory.AddUser(user);
                logger.logInfo("The user with name " + user.FirstName +" "+user.LastName+ " was succesfully added.");

                return true;
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

        public bool UpdateFirstName(String email,String newFirstName)
        {
            logger.logInfo("Try to update the first name of the user with email " + email);

            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().UserFactory.UpdateFirstName(email, newFirstName);
                logger.logInfo("First name was succesfully changed to " + newFirstName);

                return true;
            }
            catch (EntityDoesNotExistException entityNotFound)
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

        public bool UpdateLastName(String email,String newLastName)
        {
            logger.logInfo("Try to update the last name of the user with email " + email);

            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().UserFactory.UpdateLastName(email, newLastName);
                logger.logInfo("Last name was succesfully changed to " + newLastName);

                return true;
            }
            catch (EntityDoesNotExistException entityNotFound)
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

        public bool UpdateEmail(String oldEmail,String newEmail)
        {
            logger.logInfo("Try to update the user email " + oldEmail);

            try
            {
                DataMapperFactoryMethod.GetCurrentFactory().UserFactory.UpdateEmail(oldEmail, newEmail);
                logger.logInfo("Email was succesfully changed to " + newEmail);

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

        public bool DropUser(String email)
        {
            logger.logInfo("Try to drop user with email " + email);

            try 
            {
                DataMapperFactoryMethod.GetCurrentFactory().UserFactory.DropUser(email);
                logger.logInfo("TUser with email " + email+" was succesfully removed");

                return true;
            }
            catch (EntityDoesNotExistException entityNotFound)
            {
                logger.logError(entityNotFound);
                throw entityNotFound;
            }
            catch (DependencyException dependecyException)
            {
                logger.logError(dependecyException);
                throw dependecyException;
            }
        }

        public bool AddRoleToUser(String email,Role role)
        {
            logger.logInfo("Try to add role " +role.Name+" to user with email " + email);
            DataMapperFactoryMethod.GetCurrentFactory().UserFactory.AddRoleToUser(email,role);
            logger.logInfo("Role " + role.Name + " was succesfully assigned to user with email " + email);

            return true;
        }

        public bool RemoveRoleFromUser(String email,Role role)
        {
            logger.logInfo("Try to remove role" + role.Name + " from user with email " + email);

            try 
            { 
                DataMapperFactoryMethod.GetCurrentFactory().UserFactory.RemoveRoleFromUser(email, role);
                logger.logInfo("Role" + role.Name + " was succesfully removed from user with email " + email);

                return true;
            }
            catch(EntityDoesNotExistException exc)
            {
                logger.logError(exc);
                throw exc;
            }
        }

        private void VerifyUsers(User givingNoteUser, User receivingNoteUser)
        {
            if (givingNoteUser == null)
                throw new EntityDoesNotExistException("User which give the note must not be null");
            User findedUser = this.GetUserByEmail(givingNoteUser.Email);
            if (findedUser == null)
                throw new EntityDoesNotExistException("User which give the note does not exist");
            
            if (receivingNoteUser == null)
                throw new EntityDoesNotExistException("User which receive the note must not be null");
            findedUser = this.GetUserByEmail(receivingNoteUser.Email);
            if (findedUser == null)
                throw new EntityDoesNotExistException("User which receive the note does not exist");
            
            if (givingNoteUser.Equals(receivingNoteUser))
                throw new AuctionException("User " + givingNoteUser.Email + " can not add a note because it try to add a note to him");
        }

        private bool VerifyUsersAuctions(User givingNoteUser, User receivingNoteUser)
        {
            ICollection<Auction> auctions = receivingNoteUser.Auctions;
            foreach(Auction auction in auctions)
            {
                ICollection<User> users = DataMapperFactoryMethod.GetCurrentFactory().UserFactory.GetAllUsersThatParticipateToAnAuction(auction);
                foreach (User user in users)
                {
                    if (user.Equals(givingNoteUser))
                        return true;
                }
            }

            return false;
        }

        public bool AddNoteToUser(User givingNoteUser, User receivingNoteUser, int note)
        {
            try 
            {
                VerifyUsers(givingNoteUser, receivingNoteUser);

                logger.logInfo("User " + givingNoteUser.Email + " try to add note " + note + " to user with email " + receivingNoteUser);

                if (!VerifyUsersAuctions(givingNoteUser, receivingNoteUser))
                    throw new AuctionException("User "+givingNoteUser.Email+" can not add a note to user "+receivingNoteUser.Email+" because it does not participate to any auction");

                ICollection<User> users = DataMapperFactoryMethod.GetCurrentFactory().UserFactory.GetAllUsersThatGiveARaitingToAUser(receivingNoteUser);
                foreach(User user in users)
                    if(user.Equals(givingNoteUser))
                        throw new AuctionException("User " + givingNoteUser.Email + " can not add a note to user " + receivingNoteUser.Email + " because it already give a note to that user");

                Rating persistRating = new Rating();
                persistRating.Date = DateTime.Now;
                persistRating.GivingNoteUser = givingNoteUser;
                persistRating.ReceivingNoteUser = receivingNoteUser;
                persistRating.Grade = note;

                DataMapperFactoryMethod.GetCurrentFactory().UserFactory.AddRating(persistRating);

                logger.logInfo("User " + givingNoteUser.Email + " succesfully add note " + note + " to user with email " + receivingNoteUser);

                return true;
            }
            catch (EntityDoesNotExistException exc)
            {
                logger.logError(exc);
                throw exc;
            }
            catch(ValidationException validationException)
            {
                logger.logError(validationException);
                throw validationException;
            }
            catch(AuctionException auctioException)
            {
                logger.logError(auctioException);
                throw auctioException;
            }
        }

        public bool UpdateRating(User givingRating,User receivingRating, int note)
        {
            try
            { 
                if (givingRating == null)
                    throw new EntityDoesNotExistException("User which give the note must not be null");
                User findedUser = this.GetUserByEmail(givingRating.Email);
                if (findedUser == null)
                    throw new EntityDoesNotExistException("User which give the note does not exist");

                if (receivingRating == null)
                    throw new EntityDoesNotExistException("User which receive the note must not be null");
                findedUser = this.GetUserByEmail(receivingRating.Email);
                if (findedUser == null)
                    throw new EntityDoesNotExistException("User which receive the note does not exist");

                Rating oldRating = this.GetRating(givingRating,receivingRating);

                if (oldRating == null)
                    throw new AuctionException("The rating gived by user "+givingRating.Email+" to user "+receivingRating.Email+" does not exist");

                oldRating.Grade = note;

                DataMapperFactoryMethod.GetCurrentFactory().UserFactory.UpdateRating(oldRating);

                return true;
           }
           catch (EntityDoesNotExistException exc)
           {
                logger.logError(exc);
                throw exc;
           }
           catch (ValidationException validationException)
           {
                logger.logError(validationException);
                throw validationException;
           }
           catch (AuctionException auctioException)
           {
                logger.logError(auctioException);
                throw auctioException;
           }
        }

        public Rating GetRating(User givingRating,User receivingRating)
        {
            return DataMapperFactoryMethod.GetCurrentFactory().UserFactory.GetRating(givingRating,receivingRating);
        }

        public ICollection<Rating> GetAllRatingsOfAnUser(User user)
        {
            return DataMapperFactoryMethod.GetCurrentFactory().UserFactory.GetAllRatingsOfAnUser(user);
        }
        public User GetUserById(int id)
        {
            logger.logInfo("Try to get user by id from the db.");
            return DataMapperFactoryMethod.GetCurrentFactory().UserFactory.GetUserById(id);
        }    }
}
