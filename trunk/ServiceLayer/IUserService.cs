using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IUserService
    {
        void AddUser(User user);
        User GetUserByEmail(String email);
        void UpdateFirstName(String email, String newFirstName);
        void UpdateLastName(String email, String newLastName);
        void UpdateEmail(String oldEmail, String newEmail);
        void DropUser(String email);
        void AddRoleToUser(String email,Role role);
        void RemoveRoleFromUser(String email, Role role);
        void AddNoteToUser(User givingNoteUser, User receivingNoteUser, int note);
        void UpdateRating(User givingRating,User receivingRating, int note);
        Rating GetRating(User givingRating, User receivingRating);
        ICollection<Rating> GetAllRatingsOfAnUser(User user);
    }
}
