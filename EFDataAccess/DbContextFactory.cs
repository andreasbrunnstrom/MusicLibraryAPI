using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace EFDataAccess
{
    public class DbContextFactory : IDesignTimeDbContextFactory<MusicLibraryContext>
    {
        public MusicLibraryContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MusicLibraryContext>();
            optionsBuilder.UseMySql("Server=94.254.22.21;Database=music;Uid=music;Pwd=music;", new MySqlServerVersion(new Version(5, 0, 12)));
            return new MusicLibraryContext(optionsBuilder.Options);
        }
    }
}
