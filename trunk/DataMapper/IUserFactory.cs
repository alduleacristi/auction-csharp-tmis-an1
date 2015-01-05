using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IUserFactory
    {
        void AddUser(User user);
        User GetUserByEmail(String email);
        void UpdateFirstName(String email, String newFirstName);
        void UpdateLastName(String email, String newLastName);
        void UpdateEmail(String oldEmail, String newEmail);
        void DropUser(String email);
        void AddRoleToUser(String email,Role role);
        void RemoveRoleFromUser(String email, Role role);
        ICollection<User> GetAllUsersThatParticipateToAnAuction(Auction auction);
        ICollection<User> GetAllUsersThatGiveARaitingToAUser(User user);
        void AddRating(Rating rating);
        ICollection<Rating> GetAllRatingsOfAnUser(User user);
        void UpdateRating(Rating rating);
        Rating GetRating(User givingRating, User receivingRating);
    }
}
