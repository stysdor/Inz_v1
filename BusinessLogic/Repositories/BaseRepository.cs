using BusinessLogic.NHibernate;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class BaseRepository
    {
        protected ISession session;
        public BaseRepository(ISession session)
        {
            this.session = session;
            //NHibernateBase nHibernate = new NHibernateBase();
            //nHibernate.Initialize();
        }
    }
}
