using EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Configuration
{
    public class AlbumMapping : EntityConfiguration<Album>
    {
        public override void Configure(EntityConfigurator<Album> configurator)
        {
            configurator
                .Has(x => x.HasKey(z => z.Id))
                .Has(x => x.Property(z => z.Name).HasMaxLength(50).IsRequired(true))
                .Has(x => x.Property(z => z.NumberOfSongs))
                .Has(x => x.HasOne(z => z.Artist).WithMany(p => p.Albums))
                .Has(x => x.HasMany(z => z.songs));
        }
    }
}
