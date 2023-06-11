using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WWTravelClubDB
{
    public class LibraryDesignTimeDbContextFactory
        : IDesignTimeDbContextFactory<MainDBContext>
    {
        private const string endpoint = "insert here your account URL";
        private const string key = "insert here your key";
        private const string databaseName = "packagesdb";
        public MainDBContext CreateDbContext(params string[] args)
        {
            var builder = new DbContextOptionsBuilder<MainDBContext>();

            builder.UseCosmos(endpoint, key, databaseName);
            return new MainDBContext(builder.Options);
        }
    }
}
