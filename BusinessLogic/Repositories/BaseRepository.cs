using NHibernate;

namespace BusinessLogic.Repositories
{
    public class BaseRepository
    {
        protected ISession session;
        public BaseRepository(ISession session)
        {
            this.session = session;
        }
    }
}
