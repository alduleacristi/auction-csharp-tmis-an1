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
        void UpdateFirstName(User user, String newFirstName);
        void UpdateLastName(User user, String newLastName);
        void UpdateEmail(User user, String newEmail);
        void DropUser(String email);
        void AddRoleToUser(User user,Role role);
        void RemoveRoleFromUser(User user, Role role);
        ICollection<User> GetAllUsersThatParticipateToAnAuction(Auction auction);
        ICollection<User> GetAllUsersThatGiveARaitingToAUser(User user);
        void AddRating(Rating rating);
        ICollection<Rating> GetAllRatingsOfAnUser(User user);
        void UpdateRating(Rating rating);
        Rating GetRating(User givingRating, User receivingRating);
        User GetUserById(int id);    }
}
