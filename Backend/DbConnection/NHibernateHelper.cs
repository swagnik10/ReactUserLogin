using Backend.Domain;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Driver;

namespace Backend.DbConnection;

public static class NHibernateHelper
{
    private static ISessionFactory? _sessionFactory;
    private static string? _connectionString;

    public static void Configure(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static ISessionFactory SessionFactory
    {
        get
        {
            if (_sessionFactory == null)
            {
                _sessionFactory = Fluently.Configure()
                                            .Database(
                                                PostgreSQLConfiguration.Standard
                                                    .ConnectionString(_connectionString)
                                                    .ShowSql()
                                            )
                                            .Mappings(m =>
                                                m.FluentMappings
                                                    .AddFromAssemblyOf<User>()
                                            )
                                            .BuildSessionFactory();

                //var metadata = _sessionFactory.GetClassMetadata(typeof(User));

                //Console.WriteLine($"Identifier Type: {metadata.IdentifierType.Name}");

                //var sessionFactoryImpl =
                //    (NHibernate.Impl.SessionFactoryImpl)_sessionFactory;

                //Console.WriteLine(
                //    $"Dialect: {sessionFactoryImpl.Settings.Dialect.GetType().FullName}"
                //);

                //var metadata2 =
                //    (NHibernate.Persister.Entity.AbstractEntityPersister)
                //    _sessionFactory.GetClassMetadata(typeof(User));

                //Console.WriteLine(
                //    $"Identifier Generator: {metadata2.IdentifierGenerator.GetType().FullName}"
                //);
            }

            return _sessionFactory;
        }
    }
}
