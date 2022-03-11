using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Models
{
    public class AlbumIdNameArtistSongs
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual CreateResponseIdName Artist { get; set; }

        public virtual List<CreateResponseIdName> songs { get; set; }
    }
}
