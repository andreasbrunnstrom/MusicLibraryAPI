using EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Configuration
{
    public class ArtistMapping : EntityConfiguration<Artist>
    {
        public override void Configure(EntityConfigurator<Artist> configurator)
        {
            configurator
                .Has(x => x.HasKey(z => z.Id))
                .Has(x => x.Property(z => z.Name).HasMaxLength(50).IsRequired(true))
                .Has(x => x.Property(z => z.NumberOfSongs))
                .Has(x => x.HasMany(z => z.Albums).WithOne(x => x.Artist));
        }
    }
}
