using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PackagesManagementDB;

namespace WWTravelClubDB
{
    public class LibraryDesignTimeDbContextFactory
        : IDesignTimeDbContextFactory<MainDbContext>
    {
        private const string  connectionString =
            @"Server=(localdb)\mssqllocaldb;Database=wwtravelclub;
                Trusted_Connection=True;MultipleActiveResultSets=true";
        public MainDbContext CreateDbContext(params string[] args)
        {
            var builder = new DbContextOptionsBuilder<MainDbContext>();
            
            builder.UseSqlServer(connectionString);

            

            return new MainDbContext(builder.Options);
        }
    }
}
