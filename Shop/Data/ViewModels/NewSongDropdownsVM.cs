using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.ViewModels
{
    public class NewSongDropdownsVM
    {
        public NewSongDropdownsVM()
        {
            Producers = new List<Producer>();
            Labels = new List<RecordLabel>();
            Singers = new List<Singer>();

        }
        public List<Producer> Producers { get; set; }
        public List<RecordLabel> Labels { get; set; }
        public List<Singer> Singers { get; set; }
    }
}
