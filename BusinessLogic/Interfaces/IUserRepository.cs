using Core.Domain;

namespace BusinessLogic.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Validates User credentials.
        /// </summary>
        /// <param name="userName">The userName string to ckeck</param>
        /// <param name="password">The password string to check.</param>
        /// <returns>True if user with specific <paramref name="userName"/> and <paramref name="password"/> exists in the database, or false otherwise.</returns>
        bool ValidateUser(string userName, string password);

        /// <summary>
        /// Gets instance of User class.
        /// </summary>
        /// <param name="userName">The userName string</param>
        /// <returns>Instance of User class with specific value of UserName property.</returns>
        User GetUser(string userName);

        /// <summary>
        /// Gets the UserName property of instance of User class.
        /// </summary>
        /// <param name="email">The email string</param>
        /// <returns>The UserName value of instance of User class with specific value of Email property.</returns>
        string GetUserNameByEmail(string email);

        /// <summary>
        /// Inserts instance of User class into database.
        /// </summary>
        /// <param name="item">The instance of User class.</param>
        /// <returns>The Id of the new User in database.</returns>
        bool InsertUser(User item);
    }
}
