using EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Configuration
{
    public class SongMapping : EntityConfiguration<Song>
    {
        public override void Configure(EntityConfigurator<Song> configurator)
        {
            configurator
                .Has(x => x.HasKey(z => z.Id))
                .Has(x => x.Property(z => z.Name).HasMaxLength(50).IsRequired(true))
                .Has(x => x.Property(z => z.Length).IsRequired(true))
                .Has(x => x.HasOne(z => z.Artist))
                .Has(x => x.HasOne(z => z.Album).WithMany(x => x.songs));                
        }
    }
}
