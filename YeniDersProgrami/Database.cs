using YeniDersProgrami.Models;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YeniDersProgrami
{
    public static class Database
    {
        private static ISessionFactory _sessionFactory;
        private const string SessionKey = "YeniDersProgrami.Database.SessionKey";

        public static ISession Session
        {
            get { return (ISession)HttpContext.Current.Items[SessionKey]; }
        }

        public static void Configure()
        {
            var config = new Configuration();
            config.Configure();

            var mapper = new ModelMapper();
            mapper.AddMapping<BaslangicSaatiMap>();
            mapper.AddMapping<BitisSaatiMap>();
            mapper.AddMapping<BolumMap>();
            mapper.AddMapping<DersMap>();
            mapper.AddMapping<DersProgramiMap>();
            mapper.AddMapping<DersMap>();
            mapper.AddMapping<FakulteMap>();
            mapper.AddMapping<GunlerMap>();
            mapper.AddMapping<SinifMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            _sessionFactory = config.BuildSessionFactory();
        }
        public static void OpenSession()
        {
            HttpContext.Current.Items[SessionKey] = _sessionFactory.OpenSession();
        }
        public static void CloseSession()
        {
            var session = HttpContext.Current.Items[SessionKey] as ISession;
            if (session != null)
                session.Close();
            HttpContext.Current.Items.Remove(SessionKey);
        }
    }
}