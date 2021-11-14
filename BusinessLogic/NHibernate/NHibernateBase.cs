using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.NHibernate
{
    class NHibernateBase
    {
        #region fields
        private static Configuration configuration { get; set; }
        private static ISession session { get; set; }
        private static IStatelessSession statelessSession { get; set; }
        #endregion

        #region properties
        protected static ISessionFactory SessionFactory { get; set; }
        public static ISession Session => session ?? (session = SessionFactory.OpenSession());
        public static IStatelessSession StatelessSession => statelessSession ?? (statelessSession = SessionFactory.OpenStatelessSession());
        #endregion

        #region Methods
        private static Configuration ConfigureNHibernate()
        {
            configuration = new Configuration();
            configuration.Configure();
            return configuration;
        }

        public void Initialize()
        {
            configuration = ConfigureNHibernate();
            SessionFactory = configuration.BuildSessionFactory();
            new SchemaExport(configuration);
        }
        #endregion


    }
}
