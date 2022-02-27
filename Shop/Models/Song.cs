using Shop.Data.Base;
using Shop.Models.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Song:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Lyrics { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public string FullName { get; set; }

        public SongCategory SongCategory { get; set; }

        //Relationships
        public List<Singer_Song> Singers_Songs { get; set; }

        //RecordLabel
        public int RecordLabelId { get; set; }
        [ForeignKey("RecordLabelId")]
        public RecordLabel RecordLabel { get; set; }

        //Producer
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }


    }
}


