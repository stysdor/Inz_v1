using Core.Domain;

namespace BusinessLogic.Interfaces
{
    public interface IUserRepository
    {
        User ValidateUser(string userName, string password);

        User GetUser(string userName);

        string GetUserNameByEmail(string email);
 
        bool InsertUser(User item);
    }
}
