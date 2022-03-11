using EFDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess
{
    public class MusicLibraryContext : DbContext
    {
        public MusicLibraryContext(DbContextOptions<MusicLibraryContext> options) : base(options)
        {

        }

        public DbSet<Artist> Artist {get;set;}
        public DbSet<Album> Album { get; set; }
        public DbSet<Song> Song { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MusicLibraryContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
