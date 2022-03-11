using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Models
{
    public class SongIdNameLengthArtistAlbum
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Length { get; set; }

        public CreateResponseIdName ArtistIdName { get; set; }

        public CreateResponseIdName AlbumIdName { get; set; }
    }
}
