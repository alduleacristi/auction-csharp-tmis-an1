using DataMapper.Exceptions;
using DomainModel;
using System;
using System.Collections.Generic;
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
                throw new EntityNotFoundException("The user with email " + email + " does not exist.");

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
                throw new EntityNotFoundException("The user with email " + email + " does not exist.");

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
                throw new EntityNotFoundException("The user with email " + oldEmail + " does not exist.");

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

        }

        public void AddRoleToUser(String email,Role role)
        {
            User user = GetUserByEmail(email);
            if (user == null)
                throw new EntityNotFoundException("The user with email " + email + " does not exist.");

            using(var context = new AuctionModelContainer())
            {
                user.Roles.Add(role);
                context.Users.Attach(user);

                var entry = context.Entry(user);
                entry.Property(u => u.Roles).IsModified = true;

                context.SaveChanges();
            }
        }

        public void RemoveRoleFromUser(String email,Role role)
        {

        }
    }
}
