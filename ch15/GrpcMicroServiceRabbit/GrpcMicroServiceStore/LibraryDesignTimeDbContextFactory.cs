using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GrpcMicroServiceStore
{
    internal class LibraryDesignTimeDbContextFactory
    : IDesignTimeDbContextFactory<MainDbContext>
    {
        private const string connectionString =
        @"Server=(localdb)\mssqllocaldb;Database=grpcmicroservice;Trusted_Connection=True;MultipleActiveResultSets=true";
        public MainDbContext CreateDbContext(params string[] args)
        {
            var builder = new DbContextOptionsBuilder<MainDbContext>();
            builder.UseSqlServer(connectionString);
            return new MainDbContext(builder.Options);
        }
    }
}
