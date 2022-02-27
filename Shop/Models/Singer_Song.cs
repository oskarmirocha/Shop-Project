using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Singer_Song
    {
        public int SongId { get; set; }
        public Song Song { get; set; }

        public int SingerId { get; set; }
        public Singer Singer { get; set; }
    }
}
