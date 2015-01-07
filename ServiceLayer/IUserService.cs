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
        bool AddUser(User user);
        User GetUserByEmail(String email);
        bool UpdateFirstName(String email, String newFirstName);
        bool UpdateLastName(String email, String newLastName);
        bool UpdateEmail(String oldEmail, String newEmail);
        bool DropUser(String email);
        bool AddRoleToUser(String email,Role role);
        bool RemoveRoleFromUser(String email, Role role);
        bool AddNoteToUser(User givingNoteUser, User receivingNoteUser, int note);
        bool UpdateRating(User givingRating,User receivingRating, int note);
        Rating GetRating(User givingRating, User receivingRating);
        ICollection<Rating> GetAllRatingsOfAnUser(User user);
        User GetUserById(int id);    }
}
