using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Models
{
    public class ArtistIdNameAlbums
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<CreateResponseIdName> Albums { get; set; }
    }
}
