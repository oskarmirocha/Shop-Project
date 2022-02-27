using Shop.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class RecordLabel:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Label Logo is required")]
        public string Logo { get; set; }

        [Display(Name="Label Name")]
        [Required(ErrorMessage = "Label Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        //Relationships

        public List<Song> Songs { get; set; }
    }
}

