using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfSongs { get; set; }
        public virtual Artist Artist { get; set; }

        public virtual List<Song> songs { get; set; }
    }
}
