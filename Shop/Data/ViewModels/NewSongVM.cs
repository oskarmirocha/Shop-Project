using Shop.Data.Base;
using Shop.Models.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class NewSongVM
    {
        public int Id { get; set; }

        [Display(Name = "Song name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Song Cover Url")]
        [Required(ErrorMessage = "Cover Url is required")]
        public string ImageUrl { get; set; }

        [Display(Name = "Song full name")]
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Song category is required")]
        public SongCategory SongCategory { get; set; }

        [Display(Name = "Select a Singer(s)")]
        [Required(ErrorMessage = "Song singer(s) is required")]
        public List<int> SingerIds { get; set; }

        [Display(Name = "Select a Label")]
        [Required(ErrorMessage = "Song Label is required")]
        public int RecordLabelId { get; set; }
        [Display(Name = "Select a Producer")]
        [Required(ErrorMessage = "Song Producer is requird")]
        public int ProducerId { get; set; }



    }
}


