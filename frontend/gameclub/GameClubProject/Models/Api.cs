using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameClubProject.Models
{
    public class Api
    {
        public string IGDB_CLIENT_ID;
        public string IGDB_CLIENT_SECRET;

        public Api()
        {
            GameclubDBContext _context;

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();
            var builder = new DbContextOptionsBuilder<GameclubDBContext>();

            builder.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GameclubDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .UseInternalServiceProvider(serviceProvider);
            _context = new GameclubDBContext(builder.Options);

            var data = _context.IgdbApis.Find(1);
            IGDB_CLIENT_ID = data.IgdbClientId.ToString();
            IGDB_CLIENT_SECRET = data.IgdbClientSecret.ToString();
            _context.Dispose();
        }
    }
}
