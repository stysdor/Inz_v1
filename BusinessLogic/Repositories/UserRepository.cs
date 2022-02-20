using BusinessLogic.Interfaces;
using Core.Domain;
using NHibernate;
using System;
using System.Linq;

namespace BusinessLogic.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ISession session) : base(session)
        {}

        public User GetUser(string userName)
        {

            try
            {
                using (session)
                {
                    var user = session.Query<User>()
                        .Where(userItem => userItem.UserName == userName)
                        .ToList<User>();

                     return user[0];
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GetUserNameByEmail(string email)
        {

            try
            {
                using (session)
                {
                    var user = session.Query<User>()
                        .Where(userItem => userItem.Email == email)
                        .ToList<User>();

                    return user[0].UserName;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
         }

        public bool InsertUser(User item)
        {
            int id;

            using (session)
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        {
                            session.SaveOrUpdate(item);
                        }
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new Exception(e.Message);
                    }
                }
            return true;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            bool isValidate = false;
            Console.WriteLine(userName, password);

            try
            {
                using (session)
                {
                    var user = session.Query<User>()
                        .Where(userItem => userItem.UserName == userName || userItem.Email == userName)
                        .ToList();
                    if (!(user == null) && string.Equals(user[0].Password, password))
                    {
                        isValidate = true;
                    }
                    session.Close();
                    return isValidate;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
