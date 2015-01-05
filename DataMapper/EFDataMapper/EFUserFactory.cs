using DataMapper.Exceptions;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.EFDataMapper
{
    public class EFUserFactory:IUserFactory
    {
        public User GetUserByEmail(String email)
        {
            using (var context = new AuctionModelContainer())
            {
                var userVar = (from user in context.Users
                               where user.Email.Equals(email)
                               select user).FirstOrDefault();

                if(userVar != null)
                    context.Entry(userVar).Collection(u => u.Auctions).Load();

                return userVar;
            }
        }
        public void AddUser(User user)
        {
            User oldUser = GetUserByEmail(user.Email);
            if(oldUser != null)
                throw new DuplicateException("You can not add two users with the same email ("+ user.Email+").");
            
            using(var context = new AuctionModelContainer())
            {
                context.Users.Add(user);
                try
                {
                    context.SaveChanges();
                }
                catch(DbEntityValidationException exc)
                {
                    String message = "Invalid fields for user object.";
                    IEnumerable<DbEntityValidationResult> errors = exc.EntityValidationErrors;
                    foreach (DbEntityValidationResult error in errors)
                        foreach (var validationError in error.ValidationErrors)
                            message = message + " "+validationError.PropertyName + ". "+ validationError.ErrorMessage;

                    throw new ValidationException(message);
                }
            }
        }

        public void UpdateFirstName(String email, String newFirstName)
        {
            User user = GetUserByEmail(email);
            if(user == null)
                throw new EntityDoesNotExistException("The user with email " + email + " does not exist.");

            if(!user.FirstName.Equals(newFirstName))
            {
                user.FirstName = newFirstName;

                using (var context = new AuctionModelContainer())
                {
                    context.Users.Attach(user);
                    var entry = context.Entry(user);
                    entry.Property(u => u.FirstName).IsModified = true;

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbEntityValidationException exc)
                    {
                        String message = "Invalid fields for user object.";
                        IEnumerable<DbEntityValidationResult> errors = exc.EntityValidationErrors;
                        foreach (DbEntityValidationResult error in errors)
                            foreach (var validationError in error.ValidationErrors)
                                message = message + " " + validationError.PropertyName + ". " + validationError.ErrorMessage;
                        throw new ValidationException(message);
                    }
                }
            }
        }

        public void UpdateLastName(String email,String newLastName)
        {
            User user = GetUserByEmail(email);
            if (user == null)
                throw new EntityDoesNotExistException("The user with email " + email + " does not exist.");

            if (!user.LastName.Equals(newLastName))
            {
                user.LastName = newLastName;

                using (var context = new AuctionModelContainer())
                {
                    context.Users.Attach(user);
                    var entry = context.Entry(user);
                    entry.Property(u => u.LastName).IsModified = true;

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbEntityValidationException exc)
                    {
                        String message = "Invalid fields for user object.";
                        IEnumerable<DbEntityValidationResult> errors = exc.EntityValidationErrors;
                        foreach (DbEntityValidationResult error in errors)
                            foreach (var validationError in error.ValidationErrors)
                                message = message + " " + validationError.PropertyName + ". " + validationError.ErrorMessage;
                        throw new ValidationException(message);
                    }
                }
            }
        }

        public void UpdateEmail(String oldEmail, String newEmail)
        {
            User user = GetUserByEmail(oldEmail);
            if (user == null)
                throw new EntityDoesNotExistException("The user with email " + oldEmail + " does not exist.");

            if (!oldEmail.Equals(newEmail))
            {
                User auxUser = GetUserByEmail(newEmail);
                if (auxUser != null)
                    throw new DuplicateException("A user with email {" + newEmail + "} already exist.");
                user.Email = newEmail;

                using (var context = new AuctionModelContainer())
                {
                    context.Users.Attach(user);
                    var entry = context.Entry(user);
                    entry.Property(u => u.Email).IsModified = true;

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbEntityValidationException exc)
                    {
                        String message = "Invalid fields for user object.";
                        IEnumerable<DbEntityValidationResult> errors = exc.EntityValidationErrors;
                        foreach (DbEntityValidationResult error in errors)
                            foreach (var validationError in error.ValidationErrors)
                                message = message + " " + validationError.PropertyName + ". " + validationError.ErrorMessage;
                        throw new ValidationException(message);
                    }
                }
            }
        }

        public void DropUser(String email)
        {
            User user = GetUserByEmail(email);
            if (user == null)
                throw new EntityDoesNotExistException("The user with email " + email + " does not exist.");

            using (var context = new AuctionModelContainer())
            {
                context.setLazyFalse();
                context.Users.Attach(user);
                context.Entry(user).Collection(u => u.Roles).Load();

                if (user.Roles.Count > 0)
                    throw new DependencyException("User with email " + email + " has roles asigned so it can't be droped.");

                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        public void AddRoleToUser(String email,Role role)
        {
            User user = GetUserByEmail(email);
            if (user == null)
                throw new EntityDoesNotExistException("The user with email " + email + " does not exist.");

            using(var context = new AuctionModelContainer())
            {
                context.setLazyFalse();

                context.Users.Attach(user);
                context.Roles.Attach(role);
                context.Entry(user).Collection(u => u.Roles).Load();
                user.Roles.Add(role);

                context.SaveChanges();
            }
        }

        public void RemoveRoleFromUser(String email,Role role)
        {
            User user = GetUserByEmail(email);
            if (user == null)
                throw new EntityDoesNotExistException("The user with email " + email + " does not exist.");

            using (var context = new AuctionModelContainer())
            {
                context.setLazyFalse();
                
                context.Users.Attach(user);
                context.Roles.Attach(role);
                context.Entry(user).Collection(u => u.Roles).Load();
                context.Entry(role).Collection(r => r.Users).Load();

                if (!user.Roles.Contains(role))
                   throw new EntityDoesNotExistException("User with email "+email+" hasn't the role with name "+role.Name+" so it can't be remove.");

                user.Roles.Remove(role);
  
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public ICollection<User> GetAllUsersThatParticipateToAnAuction(Auction auction)
        {
            using (var context = new AuctionModelContainer())
            {
                var userVar = (from user in context.Users
                               join productAuction in context.ProductAuctions on user.IdUser equals productAuction.UserIdUser
                               join auxAuction in context.Auctions on productAuction.AuctionIdAuction equals auxAuction.IdAuction
                               where auxAuction.IdAuction == auction.IdAuction
                               select user).ToList();
                return userVar;
            }
        }

        public ICollection<User> GetAllUsersThatGiveARaitingToAUser(User user)
        {
            using (var context = new AuctionModelContainer())
            {
                var ratingVar = (from rating in context.Ratings
                                 join auxUser in context.Users on rating.ReceivingNoteUserId equals auxUser.IdUser
                                 where auxUser.IdUser == user.IdUser
                                 select rating.GivingNoteUser).ToList();
                return ratingVar;
            }
        }

        public void AddRating(Rating rating)
        {
            using(var context = new AuctionModelContainer())
            {
                context.Users.Attach(rating.GivingNoteUser);
                context.Users.Attach(rating.ReceivingNoteUser);
                
                context.Ratings.Add(rating);
                try
                { 
                    context.SaveChanges();
                }
                catch (DbEntityValidationException exc)
                {
                    String message = "";
                    IEnumerable<DbEntityValidationResult> errors = exc.EntityValidationErrors;
                    foreach (DbEntityValidationResult error in errors)
                        foreach (var validationError in error.ValidationErrors)
                            message = message + " " + validationError.PropertyName + ". " + validationError.ErrorMessage;
                    throw new ValidationException(message);
                }
            }
        }

        public void UpdateRating(Rating rating)
        {
            using (var context = new AuctionModelContainer())
            {
                context.Ratings.Attach(rating);
                var entry = context.Entry(rating);
                entry.Property(r => r.Grade).IsModified = true;

                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException exc)
                {
                    String message = "";
                    IEnumerable<DbEntityValidationResult> errors = exc.EntityValidationErrors;
                    foreach (DbEntityValidationResult error in errors)
                        foreach (var validationError in error.ValidationErrors)
                            message = message + " " + validationError.PropertyName + ". " + validationError.ErrorMessage;
                    throw new ValidationException(message);
                }
            }
        }

        public Rating GetRating(User givingRating,User receivingRating)
        {
            using (var context = new AuctionModelContainer())
            {
                var ratingVar = (from rating in context.Ratings
                                 where rating.ReceivingNoteUser.Email.Equals(receivingRating.Email) && rating.GivingNoteUser.Email.Equals(givingRating.Email)
                                 select rating).FirstOrDefault();
                return ratingVar;
            }
        }

        public ICollection<Rating> GetAllRatingsOfAnUser(User user)
        {
            using (var context = new AuctionModelContainer())
            {
                var ratingVar = (from rating in context.Ratings
                                 where rating.ReceivingNoteUser.Email.Equals(user.Email)
                                 select rating).ToList();
                return ratingVar;
            }
        }
        public User GetUserById(int id)
        {
            using (var context = new AuctionModelContainer())
            {
                context.setLazyFalse();
                var userVar = (from user in context.Users
                               where user.IdUser.Equals(id)
                               select user).FirstOrDefault();
                context.Users.Attach(userVar);
                context.Entry(userVar).Collection(user => user.Roles).Load();
                return userVar;
            }
        }    }
}
