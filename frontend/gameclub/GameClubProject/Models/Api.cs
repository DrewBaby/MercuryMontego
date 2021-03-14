using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameClubProject.Models
{
    public class Api
    {
        private protected string IGDB_CLIENT_ID;
        private protected string IGDB_CLIENT_SECRET;

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

            //var data = _context.IGDB_API.Find(1);
            //IGDB_CLIENT_ID = data.IGDB_CLIENT_ID;
            //IGDB_CLIENT_SECRET = data.IGDB_CLIENT_SECRET;
            _context.Dispose();
        }
    }
}
